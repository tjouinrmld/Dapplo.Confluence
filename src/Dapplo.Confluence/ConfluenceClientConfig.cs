//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2016-2018 Dapplo
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

namespace Dapplo.Confluence
{
	/// <summary>
	///     Use this class to configure some of the behaviour.
	///     This stores the "expand" settings for the different REST calls, and defines what additional information is
	///     requested.
	/// </summary>
	public static class ConfluenceClientConfig
	{
	    /// <summary>
	    ///     These expand values can be used when getting the content with storage, instead of view
	    /// </summary>
	    public static string[] ExpandGetContentWithStorage { get; set; } = { "body", "body.storage", "version" };

        /// <summary>
        ///     The values that are expanded in the GetAttachments result
        /// </summary>
        public static string[] ExpandGetAttachments { get; set; } = {"version", "container"};

		/// <summary>
		///     The values that are expanded in the GetChildren results
		/// </summary>
		public static string[] ExpandGetChildren { get; set; } = {"page"};

		/// <summary>
		///     The values that are expanded in the GetContent result
		/// </summary>
		public static string[] ExpandGetContent { get; set; } = {"body", "body.view", "version"};

		/// <summary>
		///     The values that are expanded in the GetContentByTitle results
		/// </summary>
		public static string[] ExpandGetContentByTitle { get; set; }

	    /// <summary>
	    ///     The values that are expanded in the Space.GetContentsAsync results
	    /// </summary>
	    public static string[] ExpandSpaceGetContents { get; set; }

        /// <summary>
        ///     The values that are expanded in the GetSpace result
        /// </summary>
        public static string[] ExpandGetSpace { get; set; } = {"icon", "description.plain", "homepage"};


		/// <summary>
		///     The values that are expanded in the GetSpaces results
		/// </summary>
		public static string[] ExpandGetSpaces { get; set; } = {"icon", "description.plain", "homepage"};

		/// <summary>
		///     The values that are expanded in the Search results
		/// </summary>
		public static string[] ExpandSearch { get; set; } = {"version", "space", "space.icon", "space.description", "space.homepage"};
	}
}