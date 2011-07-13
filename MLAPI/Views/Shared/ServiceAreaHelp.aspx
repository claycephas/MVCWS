<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<ServiceAreaDescription>" %>
<%@ Import Namespace="MLAPI.Documentation" %>

<!DOCTYPE html>
<html>
<head>
    <title><%= Model.Name %> Service Area Help</title>
</head>
<body>
	<h1><%= Model.Name %> Service Area</h1>
	<h2>Objects</h2>
	<ul>
		<% foreach (ObjectDescription obj in Model.Objects) %>
		<% { %>
			<li><a href="<%= obj.Name.ToLower() %>/?mode=help"><%= obj.Name %></a> - <%= obj.Summary %></li>
		<% } %>
	</ul>
</body>
</html>
