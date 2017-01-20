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
	public class SpaceQueryTests
	{
		public SpaceQueryTests(ITestOutputHelper testOutputHelper)
		{
			LogSettings.RegisterDefaultLogger<XUnitLogger>(LogLevels.Verbose, testOutputHelper);
		}


		[Fact]
		public void TestClause_Space_Is()
		{
			var clause = Where.Space.Is("DEV");
			Assert.Equal("space = \"DEV\"", clause.ToString());
		}

		[Fact]
		public void TestClause_Space_Not_Is()
		{
			var clause = Where.Space.Not.Is("DEV");
			Assert.Equal("space != \"DEV\"", clause.ToString());
		}

		[Fact]
		public void TestClause_Space_In()
		{
			var clause = Where.Space.In("DEV", "PRODUCTION");
			Assert.Equal("space in (\"DEV\", \"PRODUCTION\")", clause.ToString());
		}

		[Fact]
		public void TestClause_Space_Not_In()
		{
			var clause = Where.Space.Not.In("DEV", "PRODUCTION");
			Assert.Equal("space not in (\"DEV\", \"PRODUCTION\")", clause.ToString());
		}

		[Fact]
		public void TestClause_Space_InFavouriteSpacesAnd()
		{
			var clause = Where.Space.InFavouriteSpacesAnd("DEV", "PRODUCTION");
			Assert.Equal("space in (favouriteSpaces(), \"DEV\", \"PRODUCTION\")", clause.ToString());
		}

		[Fact]
		public void TestClause_Space_Not_InFavouriteSpacesAnd()
		{
			var clause = Where.Space.Not.InFavouriteSpacesAnd("DEV", "PRODUCTION");
			Assert.Equal("space not in (favouriteSpaces(), \"DEV\", \"PRODUCTION\")", clause.ToString());
		}
	}
}