// //———————————————————————
// // <copyright file="Extensions.cs">
// // This code is licensed under the MIT License.
// // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF 
// // ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// // TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
// // PARTICULAR PURPOSE AND NONINFRINGEMENT.
// // </copyright>
// // <summary>
// // Utility extension methods
// // </summary>
// //———————————————————————

using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.ALMRangers.BreakpointGenerator.Analyzer
{
    public class EntryPoint
    {
        public EntryPoint(MethodDeclarationSyntax method)
        {
            Name = method.ToDisplayName();
            LineNo = method.GetLocation().GetLineSpan().StartLinePosition.Line;
        }

        public EntryPoint(ConstructorDeclarationSyntax constructor)
        {
            Name = constructor.ToDisplayName();
            LineNo = constructor.GetLocation().GetLineSpan().StartLinePosition.Line;
        }

        public EntryPoint(AccessorDeclarationSyntax constructor, string propertyType, string propertyName)
        {
            Name = propertyType + " " + propertyName + " " + constructor.Keyword.Text;
            LineNo = constructor.GetLocation().GetLineSpan().StartLinePosition.Line;
        }

        public string Name { get; set; }
        public int LineNo { get; set; }
    }

    public static class Extensions
    {
        public static IEnumerable<EntryPoint> GetPublicEntryPoints(this ClassDeclarationSyntax c)
        {
            List<EntryPoint> entryPoints = new List<EntryPoint>();
            foreach (var m in c.DescendantNodes()
                .OfType<MethodDeclarationSyntax>()
                .Where(m => m.Modifiers.Any(mod => mod.Text.Equals("public"))))
            {
                entryPoints.Add(new EntryPoint(m));
            }
            foreach (var m in c.DescendantNodes()
                .OfType<ConstructorDeclarationSyntax>()
                .Where(m => m.Modifiers.Any(mod => mod.Text.Equals("public"))))
            {
                entryPoints.Add(new EntryPoint(m));
            }
            foreach (var m in c.DescendantNodes()
                .OfType<PropertyDeclarationSyntax>()
                .Where(m => m.Modifiers.Any(mod => mod.Text.Equals("public"))))
            {
                foreach (var a in m.AccessorList.Accessors)
                {
                    entryPoints.Add(new EntryPoint(a, m.Type.ToString(), m.Identifier.Text));
                }
            }
            return entryPoints;
        }

        public static string ToDisplayName(this MethodDeclarationSyntax m)
        {
            var displayName = m.Identifier.Text + "(";

            foreach (var p in m.ParameterList.Parameters)
            {
                displayName += p.Type + " " + p.Identifier.Text;
            }
            return displayName.TrimEnd(',') + ")";
        }

        public static string ToDisplayName(this ConstructorDeclarationSyntax m)
        {
            var displayName = m.Identifier.Text + "(";

            foreach (var p in m.ParameterList.Parameters)
            {
                displayName += p.Type + " " + p.Identifier.Text;
            }
            return displayName.TrimEnd(',') + ")";
        }
    }
}