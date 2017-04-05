// //———————————————————————
// // <copyright file="CodeParser.cs">
// // This code is licensed under the MIT License.
// // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF 
// // ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// // TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
// // PARTICULAR PURPOSE AND NONINFRINGEMENT.
// // </copyright>
// // <summary>
// //  A code parsers - parses the code and identifies various class constructs.
// // </summary>
// //———————————————————————

using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ALMRangers.BreakpointGenerator.Common;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.MSBuild;

namespace Microsoft.ALMRangers.BreakpointGenerator.Analyzer
{
    public static class CodeParser
    {
        public static async Task<Tree<TreeNode>> GetPublicMethodsFromSolution(string slnPath)
        {
            MSBuildWorkspace workspace = MSBuildWorkspace.Create();
            Solution solution = await workspace.OpenSolutionAsync(slnPath);
            Tree<TreeNode> sol = new Tree<TreeNode>(new SolutionNode(Path.GetFileName(solution.FilePath)),
                ItemType.Solution);
            foreach (ProjectId projectId in solution.ProjectIds)
            {
                Project project = solution.GetProject(projectId);
                var proj = sol.AddChild(new ProjectNode(project.Name), ItemType.Project);

                await TraverseProject(project, proj);
            }
            return sol;
        }

        private static async Task TraverseProject(Project project, Tree<TreeNode> proj)
        {
            foreach (DocumentId documentId in project.DocumentIds)
            {
                Document document = project.GetDocument(documentId);
                var file = proj.CreateNode(new FileNode(document.Name), ItemType.File);

                await TraverseDocument(proj, document, file);
            }
        }

        private static async Task TraverseDocument(Tree<TreeNode> proj, Document document, Tree<TreeNode> file)
        {
            bool foundPublicMethods = false;
            var root = await document.GetSyntaxRootAsync();
            foreach (var c in root.DescendantNodes().OfType<ClassDeclarationSyntax>())
            {
                foreach (var m in c.GetPublicEntryPoints())
                {
                    foundPublicMethods = true;
                    file.AddChild(new PublicMethodNode(m, document.FilePath), ItemType.Method);
                }
            }
            if (foundPublicMethods)
            {
                proj.AppendChildNode(file);
            }
        }

        public static async Task<Tree<TreeNode>> GetPublicMethodsFromProject(string projectPath)
        {
            MSBuildWorkspace workspace = MSBuildWorkspace.Create();
            Project project = await workspace.OpenProjectAsync(projectPath);
            Tree<TreeNode> projectNode = new Tree<TreeNode>(new ProjectNode(project.Name), ItemType.Project);

            await TraverseProject(project, projectNode);
            return projectNode;
        }

        public static async Task<Tree<TreeNode>> GetPublicMethodsFromFile(string projectPath, string filePath)
        {
            MSBuildWorkspace workspace = MSBuildWorkspace.Create();
            Project project = await workspace.OpenProjectAsync(projectPath);
            Tree<TreeNode> projectNode = new Tree<TreeNode>(new FileNode(projectPath), ItemType.Project);
            Tree<TreeNode> fileNode = new Tree<TreeNode>(new FileNode(filePath), ItemType.Project);

            var doc = project.Documents.FirstOrDefault(x => x.FilePath == filePath);

            if (doc == null)
                return null;

            Document document = project.GetDocument(doc.Id);

            await TraverseDocument(projectNode, document, fileNode);

            return fileNode;
        }
    }
}