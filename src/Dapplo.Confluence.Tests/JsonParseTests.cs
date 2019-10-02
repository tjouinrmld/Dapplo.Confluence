// Dapplo - building blocks for .NET applications
// Copyright (C) 2016-2019 Dapplo
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

            var location  = Path.GetDirectoryName(GetType().Assembly.Location) ?? throw new NotSupportedException();
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