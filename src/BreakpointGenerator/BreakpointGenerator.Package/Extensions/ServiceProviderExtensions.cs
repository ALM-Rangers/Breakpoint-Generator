// //———————————————————————
// // <copyright file="ServiceProviderExtensions.cs">
// // This code is licensed under the MIT License.
// // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF 
// // ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// // TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
// // PARTICULAR PURPOSE AND NONINFRINGEMENT.
// // </copyright>
// // <summary>
// //  IVSServiceProvider extension methods.
// // </summary>
// //———————————————————————

using System;

namespace Microsoft.ALMRangers.BreakpointGenerator.Extensions
{
    /// <summary>
    /// Extension methods for IServiceProvider
    /// </summary>
    public static class ServiceProviderExtensions
    {
        /// <summary>
        /// Gets the service.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="serviceProvider">The service provider.</param>
        /// <returns></returns>
        public static TService GetService<TService>(this IServiceProvider serviceProvider)
        {
            return (TService) serviceProvider.GetService(typeof(TService));
        }

        /// <summary>
        /// Gets the service.
        /// </summary>
        /// <typeparam name="SInterface">The type of the interface.</typeparam>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <param name="serviceProvider">The service provider.</param>
        /// <returns></returns>
        public static TInterface GetService<SInterface, TInterface>(this IServiceProvider serviceProvider)
        {
            return (TInterface) serviceProvider.GetService(typeof(SInterface));
        }

        /// <summary>
        /// Tries to the get service.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="serviceProvider">The service provider.</param>
        /// <returns></returns>
        public static TService TryGetService<TService>(this IServiceProvider serviceProvider)
        {
            object service = serviceProvider.GetService(typeof(TService));

            if (service == null)
            {
                return default(TService);
            }

            return (TService) service;
        }

        /// <summary>
        /// Tries to the get service.
        /// </summary>
        /// <typeparam name="SInterface">The type of the interface.</typeparam>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <param name="serviceProvider">The service provider.</param>
        /// <returns></returns>
        public static TInterface TryGetService<SInterface, TInterface>(this IServiceProvider serviceProvider)
        {
            object service = serviceProvider.GetService(typeof(SInterface));

            if (service == null)
            {
                return default(TInterface);
            }

            return (TInterface) service;
        }
    }
}