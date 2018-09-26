#region Dapplo 2016-2018 - GNU Lesser General Public License

// Dapplo - building blocks for .NET applications
// Copyright (C) 2016-2018 Dapplo
// 
// For more information see: http://dapplo.net/
// Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
// This file is part of Dapplo.Confluence
// 
// Dapplo.Confluence is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Dapplo.Confluence is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have a copy of the GNU Lesser General Public License
// along with Dapplo.Confluence. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#endregion

#region Usings

using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Dapplo.Log;
using Dapplo.Log.XUnit;
using Xunit;
using Xunit.Abstractions;

#endregion

namespace Dapplo.Confluence.Tests
{
    /// <summary>
    ///     Tests
    /// </summary>
    [CollectionDefinition("Dapplo.Confluence")]
    public class ConfluenceTests
	{
		public ConfluenceTests(ITestOutputHelper testOutputHelper)
		{
			LogSettings.ExceptionToStacktrace = exception => exception.ToStringDemystified();

			LogSettings.RegisterDefaultLogger<XUnitLogger>(LogLevels.Verbose, testOutputHelper);
			_confluenceClient = ConfluenceClient.Create(TestConfluenceUri);

			var username = Environment.GetEnvironmentVariable("confluence_test_username");
			var password = Environment.GetEnvironmentVariable("confluence_test_password");
			if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
			{
				_confluenceClient.SetBasicAuthentication(username, password);
			}
		}

		// Test against a well known Confluence
		private static readonly Uri TestConfluenceUri = new Uri("https://greenshot.atlassian.net/wiki");


		private readonly IConfluenceClient _confluenceClient;

		/// <summary>
		///     Test only works on Confluence 6.6 and later
		/// </summary>
		/// <returns></returns>
		[Fact]
		public async Task TestCurrentUserAndPicture()
		{
			var currentUser = await _confluenceClient.User.GetCurrentUserAsync();
			Assert.NotNull(currentUser);
			Assert.NotNull(currentUser.ProfilePicture);

			var bitmapSource = await _confluenceClient.Misc.GetPictureAsync<MemoryStream>(currentUser.ProfilePicture);
			Assert.NotNull(bitmapSource);
		}
	}
}