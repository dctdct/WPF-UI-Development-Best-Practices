// /define:GLOBAL_USE_SHAREDRESOURCEDICTIONARY
#define GLOBAL_USE_SHAREDRESOURCEDICTIONARY

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Markup;

/*
 * This file has been created by ERGOSIGN GmbH - All rights reserved - http://www.ergosign.de
 * DO NOT ALTER OR REMOVE THIS COPYRIGHT NOTICE OR THIS FILE HEADER.
 * 
 * This file and the code contained in it are subject to the agreed contractual terms and conditions, 
 * in particular with regard to resale and publication.
 */
namespace SharedResourceDictionary.Shared
{
    /// <summary>
    /// <para>
    /// Shared dictionary, load resources only once :-).
    /// </para>
    /// 
    /// <para>
    /// Class history:
    /// <list type="bullet">
    ///     <item>
    ///         <description>0.1: First release, working (David C. Thoemmes, André Lanninger).</description>
    ///     </item>
    /// </list>
    /// </para>
    /// 
    /// <para>Author: David C. Thoemmes</para>
    /// <para>Date: 11.05.2011</para>
    /// </summary>
	public class SharedResourceDictionary : ResourceDictionary
	{
		#if GLOBAL_USE_SHAREDRESOURCEDICTIONARY
		/// <summary>
		/// Dictionary for caching.
		/// </summary>
		private static Dictionary<Uri, ResourceDictionary> SharedDictionaries = new Dictionary<Uri, ResourceDictionary>();

		/// <summary>
		/// Our shared uri.
		/// </summary>
		private Uri sharedSourceURI;

		/// <summary>
		/// Define new implementation of source property.
		/// </summary>
		public new Uri Source
		{
			get { return sharedSourceURI; }

			set
			{
				// Maybe implement lock
				if (sharedSourceURI == value)
					return;

				sharedSourceURI = value;

				if (!SharedDictionaries.ContainsKey(value))
				{
					// Set as source of this rd
					base.Source = value;
					SharedDictionaries.Add(value, this);
				}
				else
				{
					MergedDictionaries.Add(SharedDictionaries[value]);
				}
			}
		}
		#endif
	}
}
