<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<ObjectDescription>" MasterPageFile="HelpMasterPage.Master" %>
<%@ Import Namespace="MLAPI.Documentation" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
	<%= Model.Name %> Object Help
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
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
</asp:Content>
