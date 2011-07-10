<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<ArticleList>" %>
<%@ Import Namespace="MLAPI.Example.Search.Models" %>
<!DOCTYPE html>
<html>
<head>
	<title>Article.Find</title>
	<script src="../../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
	<script src="../../Scripts/jquery.query-2.1.7.js" type="text/javascript"></script>
	<script>
		$(document).ready(function () {
			$("#topic").val($.query.get("topic"));
			var locationToAppend = document.location;
			if (locationToAppend.href.indexOf("?") < 0) {
				locationToAppend += "?";
			}
			else {
				locationToAppend += "&";
			}
			$("#jsonLink").attr("href", locationToAppend + "contentType=json");
			$("#xmlLink").attr("href", locationToAppend + "contentType=xml");
		});
	</script>
	<style>
		h3
		{
			padding: 0px;
			margin: 0px;
		}
		p
		{
			padding: 0px;
			margin: 0px;
		}
		ul
		{
			margin: 0px;
		}
	</style>
</head>
<body>
	<div>
		<h1>
			Find
		</h1>
		<h2>Input</h2>
		<form>
		<label>Topic: </label><input type="text" name="topic" id="topic" /><br />
		<input type="submit" value="Find" />
		</form>
		<h2>Output</h2>
		<% foreach (Article article in Model.Articles) %>
		<% { %>
			<h3><%= article.Headline %></h3>
			<p><em><%= article.Id %></em></p>
			<ul>
			<% foreach (Topic topic in article.Topics) %>
			<% { %>
				<li><%= topic.Name %></li>
			<% } %>
			</ul>
		<% } %>
		<h2>Examples</h2>
		<ul>
			<li><a href="" id="jsonLink">JSON Format</a></li>
			<li><a href="" id="xmlLink">XML Format</a></li>
			<li><a href="?topic=Parks">Find articles about Parks</a></li>
			<li><a href="?topic=Health">Find articles about Health</a></li>
			<li><a href="?magicWord=now">Find Unauthorized</a></li>
		</ul>
	</div>
</body>
</html>
