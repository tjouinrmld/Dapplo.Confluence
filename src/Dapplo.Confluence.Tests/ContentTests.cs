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
using Dapplo.Confluence.Entities;
using Dapplo.Confluence.Query;
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
    public class ContentTests
    {
        private static readonly LogSource Log = new LogSource();
        public ContentTests(ITestOutputHelper testOutputHelper)
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
        public async Task Test_ContentVersion()
        {
            var query = Where.And(Where.Space.Is("TEST"), Where.Type.IsPage, Where.Title.Contains("Test Home"));
            var searchResults = await _confluenceClient.Content.SearchAsync(query);
            var searchResult = searchResults.First();
            Log.Info().WriteLine("Version = {0}", searchResult.Version.Number);
                // => searchResult.Version.Number = 11 // !!! why 11 ?

            query = Where.Title.Contains("Test Home");
            searchResults = await _confluenceClient.Content.SearchAsync(query);
            searchResult = searchResults.First();
            Log.Info().WriteLine("Version = {0}", searchResult.Version.Number);
                // => searchResult.Version.Number = 8 // !!! why 10 ?
            var id = searchResult.Id;
            var content = await _confluenceClient.Content.GetAsync(id);
            Log.Info().WriteLine("Version = {0}", content.Version.Number);
        }

        /// <summary>
        ///     Test GetAsync
        /// </summary>
        //[Fact]
        public async Task TestGetContent()
        {
            var content = await _confluenceClient.Content.GetAsync(950274);
            Assert.NotNull(content);
            Assert.NotNull(content.Version);
            Assert.NotNull(content.Ancestors);
            Assert.True(content.Ancestors.Count > 0);
        }

        /// <summary>
        ///     Test GetHistoryAsync
        /// </summary>
        //[Fact]
        public async Task TestGetContentHistory()
        {
            var history = await _confluenceClient.Content.GetHistoryAsync(950274);
            Assert.NotNull(history);
            Assert.NotNull(history.CreatedBy);
        }

        //[Fact]
        public async Task TestCreateContent()
        {
            var attachment = await _confluenceClient.Content.CreateAsync("page", "Testing 1 2 3", "TEST", "<p>This is a test</p>");
            Assert.NotNull(attachment);
        }

        //[Fact]
        public async Task TestDeleteContent()
        {
            await _confluenceClient.Content.DeleteAsync(30375945);
        }

        [Fact]
        public async Task TestSearch()
        {
            ConfluenceClientConfig.ExpandSearch = new[] {"version", "space", "space.icon", "space.description", "space.homepage", "history.lastUpdated"};

            var searchResult = await _confluenceClient.Content.SearchAsync(Where.And(Where.Type.IsPage, Where.Text.Contains("Test Home")), limit:1);
            Assert.Equal("page", searchResult.First().Type);
            var uri = _confluenceClient.CreateWebUiUri(searchResult.FirstOrDefault()?.Links);
            Assert.NotNull(uri);
        }

        //[Fact]
        public async Task TestLabels()
        {
            var searchResult = await _confluenceClient.Content.SearchAsync(Where.And(Where.Type.IsPage, Where.Text.Contains("Test Home")), limit: 1);
            var contentId = searchResult.First().Id;

            var labels = new[] {"test1", "test2"};
            await _confluenceClient.Content.AddLabelsAsync(contentId, labels.Select(s => new Label{Name = s}));
            var labelsForContent = await _confluenceClient.Content.GetLabelsAsync(contentId);
            Assert.Equal(labels.Length, labelsForContent.Count(label => labels.Contains(label.Name)));

            // Delete all
            foreach (var label in labelsForContent)
            {
                await _confluenceClient.Content.DeleteLabelAsync(contentId, label.Name);
            }
        }
    }
}