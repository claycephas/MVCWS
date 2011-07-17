<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="MLAPI.Documentation" %>

<%
	ServiceAreaDescription serviceArea = null;
	string relativePath = "./";
	if (Model is ServiceAreaDescription)
	{
		serviceArea = (ServiceAreaDescription)Model;
	}
	else if (Model is ObjectDescription)
	{
		serviceArea = ((ObjectDescription)Model).DeclaringServiceArea;
		relativePath = "../";
	}
	else if (Model is ActionDescription)
	{
		serviceArea = ((ActionDescription)Model).DeclaringObject.DeclaringServiceArea;
		relativePath = "../../";
	}
%>

<ul>
	<li><a href="<%= relativePath %>?mode=help"><%= serviceArea.Name %></a>
	<ul>
	<% foreach (ObjectDescription obj in serviceArea.Objects) %>
	<% { %>
		<li><a href="<%= relativePath + obj.Name.ToLower() %>/?mode=help"><%= obj.Name%></a> - <%= obj.Summary%>
		<ul>
		<% foreach (ActionDescription action in obj.Actions) %>
		<% { %>
			<li><a href="<%= relativePath + obj.Name.ToLower() + "/" + action.Name.ToLower() %>/?mode=help"><%= action.Name%></a> - <%= action.Summary%></li>
		<% } %>
		</li>
		</ul>
	<% } %>
	</li>
	</ul>
</ul>