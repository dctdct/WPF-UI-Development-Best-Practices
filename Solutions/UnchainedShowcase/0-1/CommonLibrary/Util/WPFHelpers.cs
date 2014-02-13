using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Media;

/*
* This file has been created by ERGOSIGN GmbH - All rights reserved - http://www.ergosign.de
* DO NOT ALTER OR REMOVE  THIS COPYRIGHT NOTICE OR THIS FILE HEADER.
* 
* This file and the code contained in it are subject to the agreed contractual terms and conditions,
* in particular with regard to resale and publication.
*/
namespace CommonLibrary.Util
{
    /// <summary>
    /// <para>
    /// The class WPFHelpers represents a collection of WPF helper methods
    /// </para>
    /// 
    /// <para>
    /// Class history:
    /// <list type="bullet">
    ///     <item>
    ///         <description>0.1: First release, working (André Lanninger).</description>
    ///     </item>
    /// </list>
    /// </para>
    /// 
    /// <para>Author: André Lanninger</para>
    /// <para>Date: 27.03.2013</para>
    /// </summary>
    public static class WPFHelpers
    {
        /// <summary>
        /// Returns all types in the given assembly and namespace
        /// </summary>
        /// <param name="assembly">Assembly</param>
        /// <param name="nameSpace">string</param>
        /// <returns>Type[]</returns>
        public static Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
        {
            return assembly.GetTypes().Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal)).ToArray();
        }

        /// <summary>
        /// Searches the visual tree for an ancestor specified by the type T
        /// </summary>
        /// <typeparam name="T">Searched Type</typeparam>
        /// <param name="dependencyObject">Starting Point</param>
        /// <returns>Searched Type</returns>
        public static T FindAncestor<T>(DependencyObject dependencyObject) where T : class
        {
            DependencyObject target = dependencyObject;
            do
            {
                target = VisualTreeHelper.GetParent(target);
            }
            while (target != null && !(target is T));

            return target as T;
        }
    }
}
