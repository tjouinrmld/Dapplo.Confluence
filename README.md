# Dapplo.Confluence
This is a simple REST based Confluence client, written for Greenshot, by using Dapplo.HttpExtension

- Current build status: [![Build status](https://ci.appveyor.com/api/projects/status/3vp7h9n40n4v680n?svg=true)](https://ci.appveyor.com/project/dapplo/dapplo-confluence)
- Coverage Status: [![Coverage Status](https://coveralls.io/repos/github/dapplo/Dapplo.Confluence/badge.svg?branch=master)](https://coveralls.io/github/dapplo/Dapplo.Confluence?branch=master)
- NuGet package: [![NuGet package](https://badge.fury.io/nu/Dapplo.Confluence.svg)](https://badge.fury.io/nu/Dapplo.Confluence)

If you want to extend the API, for example to add logic for a *plugin*, you can write an extension method to extend the IConfluenceClientPlugins.
Your "plugin" extension will now be available, if the developer has a using statement of your namespace, on the .Plugins property of the IConfluenceClient

An example can be found in the ConfluenceAttachmentExtensions, this is directly on the IConfluenceClient but the principle is the same.