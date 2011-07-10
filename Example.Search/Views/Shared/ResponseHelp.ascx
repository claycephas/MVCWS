<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ObjectDescription>" %>
<%@ Import Namespace="MLAPI.Documentation" %>

<% if (Model.Members != null) %>
<% { %>
	<ul>
		<% foreach (MemberDescription member in Model.Members) %>
		<% { %>
			<li><strong><%= member.Name %></strong> : <em><%= member.Type.Name %></em> - <%= member.Summary %></li>
			<% Html.RenderPartial("ResponseHelp", member.Type); %>
		<% } %>
	</ul>
<% } %>
