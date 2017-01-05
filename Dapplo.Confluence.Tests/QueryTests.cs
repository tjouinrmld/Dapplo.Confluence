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
	public class QueryTests
	{
		public QueryTests(ITestOutputHelper testOutputHelper)
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
		public void TestClause_Type_IsPage()
		{
			var clause = Where.Type.IsPage;
			Assert.Equal("type = page", clause.ToString());
		}

		[Fact]
		public void TestClause_Type_In()
		{
			var clause = Where.Type.In(Types.Page, Types.BlogPost);
			Assert.Equal("type in (page, blogpost)", clause.ToString());
		}

		[Fact]
		public void TestClause_Type_Not_In()
		{
			var clause = Where.Type.Not.In(Types.Page, Types.BlogPost);
			Assert.Equal("type not in (page, blogpost)", clause.ToString());
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
		public void TestClause_Created_StartOfYear()
		{
			var clause = Where.Created.Before.StartOfYear();
			Assert.Equal("created < startOfYear()", clause.ToString());
		}

		[Fact]
		public void TestClause_Created_StartOfDay_WithNegativeIncrement()
		{
			// Find content created in the last 7 days
			// created > startOfDay("-7d")

			var clause = Where.Created.After.StartOfDay(TimeSpan.FromDays(-7));
			Assert.Equal("created > startOfDay(\"-7d\")", clause.ToString());
		}

		[Fact]
		public void TestClause_Created_On()
		{
			// Find content created today
			// created = yyyy-mm-dd

			var clause = Where.Created.On.DateTime(DateTime.Today);
			Assert.Equal($"created = \"{DateTime.Today:yyyy-MM-dd}\"", clause.ToString());
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

		[Fact]
		public void TestClause_Text_Contains()
		{
			var clause = Where.Text.Contains("hello");
			Assert.Equal("text ~ \"hello\"", clause.ToString());
		}

		[Fact]
		public void TestClause_OrderBy()
		{
			var clause = Where.Creator.Is("jsmith").OrderBy(Fields.Space).OrderByAscending(Fields.Title);
			Assert.Equal("creator = \"jsmith\" order by space, title asc", clause.ToString());
		}
	}
}