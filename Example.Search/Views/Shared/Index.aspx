<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<ServiceAreaDescription>" %>
<%@ Import Namespace="MLAPI.Documentation" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title><%= Model.Name %> Service Area</title>
</head>
<body>
    <div>
		<h1><%= Model.Name %> Service Area</h1>
		<p><%= Model.Summary %></p>
		<h2>Objects</h2>
		<ul>
			<% foreach (ObjectDescription obj in Model.Objects) %>
			<% { %>
				<li><a href="<%= obj.Name.ToLower() %>/?mode=help"><%= obj.Name %></a> - <%= obj.Summary %></li>
			<% } %>
		</ul>
    </div>
</body>
</html>
