<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<ServiceAreaDescription>" MasterPageFile="HelpMasterPage.Master" %>
<%@ Import Namespace="MLAPI.Documentation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= Model.Name %> Service Area Help</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<h1><%= Model.Name %> Service Area</h1>
	<h2>Objects</h2>
	<ul>
		<% foreach (ObjectDescription obj in Model.Objects) %>
		<% { %>
			<li><a href="<%= obj.Name.ToLower() %>/?mode=help"><%= obj.Name %></a> - <%= obj.Summary %></li>
		<% } %>
	</ul>
</asp:Content>