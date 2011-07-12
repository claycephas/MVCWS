<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<ActionDescription>" %>
<%@ Import Namespace="MLAPI.Documentation" %>

<!DOCTYPE html>
<html>
<head runat="server">
	<title><%= Model.DeclaringObject.Name %>.<%= Model.Name %> Action Help</title>
</head>
<body>
	<div>
		<h1><%= Model.DeclaringObject.Name %>.<%= Model.Name %> Action</h1>
		<p><%= Model.Summary %></p>
		<h2>Parameters</h2>
		<ul>
			<% foreach (ParameterDescription parameter in Model.Parameters) %>
			<% { %>
			<li>
				<strong><%= parameter.Name %></strong> - <%= parameter.Summary %>
			</li>
			<% } %>
		</ul>
		<h2>Response</h2>
		<ul>
			<li><strong><%= Model.Response.Name %></strong> : <em><%= Model.Response.Name %></em> - <%= Model.Response.Summary %></li>
			<% Html.RenderPartial("~/bin/Views/Shared/ResponseHelp.ascx", Model.Response); %>
		</ul>
		<p><a href="../?mode=help">Back</a></p>
	</div>
</body>
</html>
	