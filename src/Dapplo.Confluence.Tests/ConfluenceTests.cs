// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


using System;
using System.Diagnostics;
using System.IO;
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

        // Test against a "well known" Confluence
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
            Assert.DoesNotContain("Anonymous", currentUser.DisplayName);

            var bitmapSource = await _confluenceClient.Misc.GetPictureAsync<MemoryStream>(currentUser.ProfilePicture);
            Assert.NotNull(bitmapSource);
        }
    }
}