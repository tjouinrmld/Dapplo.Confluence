#region Dapplo 2016 - GNU Lesser General Public License

// Dapplo - building blocks for .NET applications
// Copyright (C) 2016 Dapplo
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
using System.Linq;
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
	public class SpaceTests
	{
		public SpaceTests(ITestOutputHelper testOutputHelper)
		{
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
		///     Test GetAsync
		/// </summary>
		[Fact]
		public async Task TestGetSpace()
		{
			var space = await _confluenceClient.Space.GetAsync("TEST");
			Assert.NotNull(space);
			Assert.NotNull(space.Description);
		}

		/// <summary>
		///     Test Space.GetAllAsync
		/// </summary>
		[Fact]
		public async Task TestGetSpaces()
		{
			var spaces = await _confluenceClient.Space.GetAllAsync();
			Assert.NotNull(spaces);
			Assert.True(spaces.Count > 0);
		}


        /// <summary>
        ///     Test GetContentsAsync
        /// </summary>
        [Fact]
        public async Task TestGetContentsAsync()
	    {
	        var spaceContents = await _confluenceClient.Space.GetContentsAsync("TEST");
	        Assert.NotNull(spaceContents);
	        Assert.NotNull(spaceContents.Pages);
	        Assert.True(spaceContents.Pages.Any());
	    }

        /// <summary>
        ///     Test Space.CreateAsync
        /// </summary>
        [Fact]
		public async Task TestCreateAsync()
		{
			const string key = "TESTTMP";
			var createdSpace = await _confluenceClient.Space.CreatePrivateAsync(key, "Dummy for test", "Created and deleted during test");
			Assert.NotNull(createdSpace);
			Assert.Equal(key, createdSpace.Key);

			try
			{
				var space = await _confluenceClient.Space.GetAsync(key);
				Assert.NotNull(space);
				Assert.Equal(key, space.Key);
			}
			finally
			{
				await _confluenceClient.Space.DeleteAsync(key);
			}
		}
	}
}