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
using System.IO;
using System.Threading.Tasks;
using Dapplo.Log.Facade;
using Dapplo.Log.XUnit;
using Xunit;
using Xunit.Abstractions;

#endregion

namespace Dapplo.Confluence.Tests
{
	/// <summary>
	///     Tests
	/// </summary>
	public class ConfluenceTests
	{
		public ConfluenceTests(ITestOutputHelper testOutputHelper)
		{
			LogSettings.RegisterDefaultLogger<XUnitLogger>(LogLevels.Verbose, testOutputHelper);
			_confluenceApi = new ConfluenceApi(TestConfluenceUri);

			var username = Environment.GetEnvironmentVariable("confluence_test_username");
			var password = Environment.GetEnvironmentVariable("confluence_test_password");
			if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
			{
				_confluenceApi.SetBasicAuthentication(username, password);
			}
		}

		// Test against a well known Confluence
		private static readonly Uri TestConfluenceUri = new Uri("https://greenshot.atlassian.net/wiki");

		private readonly ConfluenceApi _confluenceApi;

		/// <summary>
		///     Doesn't work yet, as deleting an attachment is not supported
		///     See <a href="https://jira.atlassian.com/browse/CONF-36015">CONF-36015</a>
		/// </summary>
		/// <returns></returns>
		//[Fact]
		public async Task TestAttach()
		{
			const string testPageId = "950274";
			var attachments = await _confluenceApi.GetAttachmentsAsync(testPageId);
			Assert.NotNull(attachments);

			// Delete all attachments
			foreach (var attachment in attachments.Results)
			{
				// Attachments are content!!
				await _confluenceApi.DeleteAttachmentAsync(attachment);
			}

			const string attachmentContent = "Testing 1 2 3";
			attachments = await _confluenceApi.AttachAsync(testPageId, attachmentContent, "test.txt", "This is a test");
			Assert.NotNull(attachments);

			attachments = await _confluenceApi.GetAttachmentsAsync(testPageId);
			Assert.NotNull(attachments);
			Assert.True(attachments.Results.Count > 0);

			// Test if the content is correct
			foreach (var attachment in attachments.Results)
			{
				var content = await _confluenceApi.GetAttachmentContentAsync<string>(attachment);
				Assert.Equal(attachmentContent, content);
			}
			// Delete all attachments
			foreach (var attachment in attachments.Results)
			{
				// Attachments are content!!
				await _confluenceApi.DeleteContentAsync(attachment.Id);
			}
			attachments = await _confluenceApi.GetAttachmentsAsync(testPageId);
			Assert.NotNull(attachments);
			Assert.True(attachments.Results.Count == 0);
		}

		/// <summary>
		///     Test GetContentAsync
		/// </summary>
		//[Fact]
		public async Task TestGetContent()
		{
			var content = await _confluenceApi.GetContentAsync("950274");
			Assert.NotNull(content);
			Assert.NotNull(content.Version);
		}

		/// <summary>
		///     Test GetContentHistoryAsync
		/// </summary>
		//[Fact]
		public async Task TestGetContentHistory()
		{
			var history = await _confluenceApi.GetContentHistoryAsync("950274");
			Assert.NotNull(history);
			Assert.NotNull(history.CreatedBy);
		}

		//[Fact]
		public async Task TestCreateContent()
		{
			var attachment = await _confluenceApi.CreateContentAsync("page", "Testing 1 2 3", "TEST", "<p>This is a test</p>");
			Assert.NotNull(attachment);
		}

		//[Fact]
		public async Task TestDeleteContent()
		{
			await _confluenceApi.DeleteContentAsync("30375945");
		}

		/// <summary>
		///     Test only works on Confluence 6.6 and later
		/// </summary>
		/// <returns></returns>
		[Fact]
		public async Task TestCurrentUserAndPicture()
		{
			var currentUser = await _confluenceApi.GetCurrentUserAsync();
			Assert.NotNull(currentUser);
			Assert.NotNull(currentUser.ProfilePicture);

			var bitmapSource = await _confluenceApi.GetPictureAsync<MemoryStream>(currentUser.ProfilePicture);
			Assert.NotNull(bitmapSource);
		}

		[Fact]
		public async Task TestGetAttachments()
		{
			var attachments = await _confluenceApi.GetAttachmentsAsync("950274");
			Assert.NotNull(attachments);
			Assert.NotNull(attachments.Results.Count > 0);
		}

		/// <summary>
		///     Test GetSpaceAsync
		/// </summary>
		[Fact]
		public async Task TestGetSpace()
		{
			var space = await _confluenceApi.GetSpaceAsync("TEST");
			Assert.NotNull(space);
			Assert.NotNull(space.Description);
		}

		/// <summary>
		///     Test GetSpacesAsync
		/// </summary>
		[Fact]
		public async Task TestGetSpaces()
		{
			var spaces = await _confluenceApi.GetSpacesAsync();
			Assert.NotNull(spaces);
			Assert.NotNull(spaces.Count > 0);
		}

		[Fact]
		public async Task TestSearch()
		{
			ConfluenceConfig.ExpandSearch = new[] {"version", "space", "space.icon", "space.description", "space.homepage", "history.lastUpdated"};

			var searchResult = await _confluenceApi.SearchAsync("text ~ \"Test Home\"");
			Assert.NotNull(searchResult);
			Assert.True(searchResult.Results.Count > 0);

			foreach (var content in searchResult.Results)
			{
				Assert.NotNull(content.Type);
			}
		}
	}
}