// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Dapplo.Log;
using Dapplo.Log.XUnit;
using Xunit;
using Xunit.Abstractions;

namespace Dapplo.Confluence.Tests
{
    /// <summary>
    ///     Tests
    /// </summary>
    [CollectionDefinition("Dapplo.Confluence")]
    public class SpaceTests
    {
        public SpaceTests(ITestOutputHelper testOutputHelper)
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