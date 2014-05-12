GitHubby
========

A small project that showcases some of my web developer skills.

This tiny single-page app allows you to search for GitHub users and see some 
cool public data about them.

###Requirements

1. Visual Studio (2012)
2. ASP.NET MVC 4
3. Enable Package Restore!
  1. [Octokit](https://www.nuget.org/packages/Octokit)
  2. [Json.NET](http://www.nuget.org/packages/newtonsoft.json)

###Current Issues

####Getting latest commits for repositories

I really wanted to display latest commits for repositories, but it turns 
out the library I'm using for the GitHub API does not support this quite yet.
* [octokit/octokit.net Issue #333](https://github.com/octokit/octokit.net/issues/333)
* [octokit/octokit.net Issue #469](https://github.com/octokit/octokit.net/issues/469)

Possible solutions are use a different library (Ruby :heart:) or just do the HTTP calls myself.