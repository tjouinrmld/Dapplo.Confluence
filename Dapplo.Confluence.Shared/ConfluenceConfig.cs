//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2015-2016 Dapplo
// 
//  For more information see: http://dapplo.net/
//  Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
//  This file is part of Dapplo.Confluence
// 
//  Dapplo.Confluence is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  Dapplo.Confluence is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
// 
//  You should have a copy of the GNU Lesser General Public License
//  along with Dapplo.Confluence. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#region using
using System.Collections.Generic;
#endregion

namespace Dapplo.Confluence
{
	/// <summary>
	/// Use this class to configure some of the behaviour
	/// </summary>
	public static class ConfluenceConfig
	{
		/// <summary>
		/// The values that are expanded in the Search results
		/// </summary>
		public static IList<string> ExpandSearch
		{
			get;
			set;
		} = new List<string> { "version", "space", "space.icon", "space.description", "space.homepage" };

		/// <summary>
		/// The values that are expanded in the GetChildren results
		/// </summary>
		public static IList<string> ExpandGetChildren
		{
			get;
			set;
		} = new List<string> { "page" };

		/// <summary>
		/// The values that are expanded in the GetContent result
		/// </summary>
		public static IList<string> ExpandGetContent
		{
			get;
			set;
		} = new List<string>();

		/// <summary>
		/// The values that are expanded in the GetContentByTitle results
		/// </summary>
		public static IList<string> ExpandGetContentByTitle
		{
			get;
			set;
		} = new List<string>();

		/// <summary>
		/// The values that are expanded in the GetSpace result
		/// </summary>
		public static IList<string> ExpandGetSpace
		{
			get;
			set;
		} = new List<string>();

		/// <summary>
		/// The values that are expanded in the GetCurrentUser result
		/// </summary>
		public static IList<string> ExpandGetCurrentUser
		{
			get;
			set;
		} = new List<string>();

		/// <summary>
		/// The values that are expanded in the GetUser result
		/// </summary>
		public static IList<string> ExpandGetUser
		{
			get;
			set;
		} = new List<string>();

		/// <summary>
		/// The values that are expanded in the GetAttachments result
		/// </summary>
		public static IList<string> ExpandGetAttachments
		{
			get;
			set;
		} = new List<string> { "version" };


		/// <summary>
		/// The values that are expanded in the GetSpaces results
		/// </summary>
		public static IList<string> ExpandGetSpaces
		{
			get;
			set;
		} = new List<string> { "icon","description.plain" };
	}
}
