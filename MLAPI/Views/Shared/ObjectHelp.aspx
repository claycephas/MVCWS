<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<ObjectDescription>" %>
<%@ Import Namespace="MLAPI.Documentation" %>

<!DOCTYPE html>
<html>
<head>
	<title><%= Model.Name %> Object Help</title>
</head>
<body>
	<h1><%= Model.Name %> Object</h1>
	<p><%= Model.Summary %></p>
	<h2>Actions</h2>
	<ul>
		<% if (Model.Actions != null) %>
		<% { %>
			<% foreach (ActionDescription action in Model.Actions) %>
			<% { %>
				<li><a href="<%= action.Name.ToLower() %>/?mode=help"><%= action.Name %></a> - <%= action.Summary %></li>
			<% } %>
		<% } %>
		<% else %>
		<% { %>
			<li><em>None</em></li>
		<% } %>
	</ul>
	<p><a href="../?mode=help">Back</a></p>
</body>
</html>
