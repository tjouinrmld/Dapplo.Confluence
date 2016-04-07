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

using System;
using System.Threading.Tasks;
using Dapplo.LogFacade;
using Xunit;
using Xunit.Abstractions;

#endregion

namespace Dapplo.Confluence.Tests
{
	public class ConfluenceTests
	{
		// Test against a well known Confluence
		private static readonly Uri TestConfluenceUri = new Uri("https://confluence.cip4.org");

		private readonly ConfluenceApi _confluenceApi;

		public ConfluenceTests(ITestOutputHelper testOutputHelper)
		{
			XUnitLogger.RegisterLogger(testOutputHelper, LogLevel.Verbose);
			_confluenceApi = new ConfluenceApi(TestConfluenceUri);
			//_confluenceApi.SetBasicAuthentication("username", "password");
		}

		[Fact]
		public async Task TestSearch()
		{
			var searchResult = await _confluenceApi.SearchAsync("text ~ \"CIP4\"");
			Assert.NotNull(searchResult);
			Assert.True(searchResult.Results.Count > 0);

			foreach (var content in searchResult.Results)
			{
				Assert.NotNull(content.Type);
			}
		}

		//[Fact]
		public async Task TestContent()
		{
			var content = await _confluenceApi.GetContentAsync("2721");
			Assert.NotNull(content);
			Assert.NotNull(content.Version);
		}

		//[Fact]
		public async Task TestAttachments()
		{
			var attachments = await _confluenceApi.GetAttachmentsAsync("37298618");
			Assert.NotNull(attachments);
			Assert.NotNull(attachments.Results.Count > 0);
		}


		//[Fact]
		public async Task TestAttach()
		{
			var attachment = await _confluenceApi.AttachAsync("37298618", "Testing 1 2 3", "test.txt", "This is a test");
			Assert.NotNull(attachment);
		}
	}
}