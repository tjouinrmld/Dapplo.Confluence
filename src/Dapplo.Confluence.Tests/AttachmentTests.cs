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
	public class AttachmentTests
	{
		public AttachmentTests(ITestOutputHelper testOutputHelper)
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


		[Fact]
		public async Task TestGetAttachments()
		{
			var attachments = await _confluenceClient.Attachment.GetAttachmentsAsync("950274");
			Assert.NotNull(attachments);
			Assert.NotNull(attachments.Results.Count > 0);
			using (var attachmentMemoryStream = await _confluenceClient.Attachment.GetContentAsync<MemoryStream>(attachments.FirstOrDefault()))
			{
				Assert.True(attachmentMemoryStream.Length > 0);
			}

		}

		/// <summary>
		///     Doesn't work yet, as deleting an attachment is not supported
		///     See <a href="https://jira.atlassian.com/browse/CONF-36015">CONF-36015</a>
		/// </summary>
		/// <returns></returns>
		//[Fact]
		public async Task TestAttach()
		{
			const string testPageId = "950274";
			var attachments = await _confluenceClient.Attachment.GetAttachmentsAsync(testPageId);
			Assert.NotNull(attachments);

			// Delete all attachments
			foreach (var attachment in attachments.Results)
			{
				// Attachments are content!!
				await _confluenceClient.Attachment.DeleteAsync(attachment);
			}

			const string attachmentContent = "Testing 1 2 3";
			attachments = await _confluenceClient.Attachment.AttachAsync(testPageId, attachmentContent, "test.txt", "This is a test");
			Assert.NotNull(attachments);

			attachments = await _confluenceClient.Attachment.GetAttachmentsAsync(testPageId);
			Assert.NotNull(attachments);
			Assert.True(attachments.Results.Count > 0);

			// Test if the content is correct
			foreach (var attachment in attachments.Results)
			{
				var content = await _confluenceClient.Attachment.GetContentAsync<string>(attachment);
				Assert.Equal(attachmentContent, content);
			}
			// Delete all attachments
			foreach (var attachment in attachments.Results)
			{
				// Attachments are content!!
				await _confluenceClient.Content.DeleteAsync(attachment.Id);
			}
			attachments = await _confluenceClient.Attachment.GetAttachmentsAsync(testPageId);
			Assert.NotNull(attachments);
			Assert.True(attachments.Results.Count == 0);
		}
	}
}