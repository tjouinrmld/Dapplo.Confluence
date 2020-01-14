// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


using System;
using System.IO;
using Dapplo.Confluence.Entities;
using Dapplo.Log;
using Dapplo.Log.XUnit;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace Dapplo.Confluence.Tests
{
    public class JsonParseTests
    {
        private const string FilesDir = "JsonTestFiles";
        private readonly string _testFileLocation;

        public JsonParseTests(ITestOutputHelper testOutputHelper)
        {
            LogSettings.RegisterDefaultLogger<XUnitLogger>(LogLevels.Verbose, testOutputHelper);

            _testFileLocation = FilesDir;
            if (Directory.Exists(FilesDir))
            {
                return;
            }

            var location = Path.GetDirectoryName(GetType().Assembly.Location) ?? throw new NotSupportedException();
            _testFileLocation = Path.Combine(location, FilesDir);
        }

        [Fact]
        public void TestParseContent()
        {
            var json = File.ReadAllText(Path.Combine(_testFileLocation, "content.json"));
            var content = JsonConvert.DeserializeObject<Content>(json);
            Assert.NotNull(content);
            Assert.Equal("http://myhost:8080/confluence/rest/api/content/1234", content.Links.Self.AbsoluteUri);
        }
    }
}