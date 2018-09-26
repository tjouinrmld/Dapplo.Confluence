#region Dapplo 2016-2018 - GNU Lesser General Public License

// Dapplo - building blocks for .NET applications
// Copyright (C) 2016-2018 Dapplo
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
    public class UserQueryTests
	{
		public UserQueryTests(ITestOutputHelper testOutputHelper)
		{
			LogSettings.RegisterDefaultLogger<XUnitLogger>(LogLevels.Verbose, testOutputHelper);
		}


		[Fact]
		public void TestClause_AndClause()
		{
			var clause1 = Where.Creator.IsCurrentUser;
			var clause2 = Where.Mention.Not.IsCurrentUser;
			Assert.Equal("(creator = currentUser() and mention != currentUser())", Where.And(clause1, clause2).ToString());
		}

		[Fact]
		public void TestClause_OrClause()
		{
			var clause1 = Where.Creator.IsCurrentUser;
			var clause2 = Where.Mention.Not.IsCurrentUser;
			Assert.Equal("(creator = currentUser() or mention != currentUser())", Where.Or(clause1, clause2).ToString());
		}

		[Fact]
		public void TestClause_IsCurrentUser()
		{
			var clause = Where.Creator.IsCurrentUser;
			Assert.Equal("creator = currentUser()", clause.ToString());
		}

		[Fact]
		public void TestClause_NotIsCurrentUser()
		{
			var clause = Where.Creator.Not.IsCurrentUser;
			Assert.Equal("creator != currentUser()", clause.ToString());
		}

		[Fact]
		public void TestClause_IsUser()
		{
			var clause = Where.Creator.Is("jsmith");
			Assert.Equal("creator = \"jsmith\"", clause.ToString());
		}

		[Fact]
		public void TestClause_InCurrentUserAnd()
		{
			var clause = Where.Creator.InCurrentUserAnd("jsmith");
			Assert.Equal("creator in (currentUser(), \"jsmith\")", clause.ToString());
		}

		[Fact]
		public void TestClause_IsUserIn()
		{
			var clause = Where.Creator.In("jsmith");
			Assert.Equal("creator in (\"jsmith\")", clause.ToString());
		}

		[Fact]
		public void TestClause_NotIsUser()
		{
			var clause = Where.Creator.Not.Is("jsmith");
			Assert.Equal("creator != \"jsmith\"", clause.ToString());
		}

		[Fact]
		public void TestClause_OrderBy()
		{
			var clause = Where.Creator.Is("jsmith").OrderBy(Fields.Space).OrderByAscending(Fields.Title);
			Assert.Equal("creator = \"jsmith\" order by space, title asc", clause.ToString());
		}
	}
}