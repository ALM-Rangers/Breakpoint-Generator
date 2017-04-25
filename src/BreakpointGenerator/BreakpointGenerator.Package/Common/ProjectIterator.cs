// //———————————————————————
// // <copyright file="ProjectIterator.cs">
// // This code is licensed under the MIT License.
// // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF 
// // ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// // TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
// // PARTICULAR PURPOSE AND NONINFRINGEMENT.
// // </copyright>
// // <summary>
// //  A helper class to iterate projects.
// // </summary>
// //———————————————————————

using System;
using System.Collections;
using System.Collections.Generic;
using EnvDTE;
using EnvDTE80;

namespace Microsoft.ALMRangers.BreakpointGenerator.Common
{
    public sealed class ProjectIterator : IEnumerable<Project>
    {
        Solution solution;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectIterator"/> class.
        /// </summary>
        /// <param name="solution">The solution.</param>
        public ProjectIterator(Solution solution)
        {
            if (solution == null)
            {
                throw new ArgumentNullException("solution");
            }

            this.solution = solution;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<Project> GetEnumerator()
        {
            foreach (Project project in this.solution.Projects)
            {
                if (project.Kind != ProjectKinds.vsProjectKindSolutionFolder)
                {
                    yield return project;
                }
                else
                {
                    foreach (Project subProject in Enumerate(project))
                    {
                        yield return subProject;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private IEnumerable<Project> Enumerate(Project project)
        {
            foreach (ProjectItem item in project.ProjectItems)
            {
                if (item.Object is Project)
                {
                    if (((Project) item.Object).Kind != ProjectKinds.vsProjectKindSolutionFolder)
                    {
                        yield return (Project) item.Object;
                    }
                    else
                    {
                        foreach (Project subProject in Enumerate((Project) item.Object))
                        {
                            yield return subProject;
                        }
                    }
                }
            }
        }
    }
}