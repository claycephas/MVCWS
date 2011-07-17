<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<ActionDescription>" MasterPageFile="HelpMasterPage.Master" %>
<%@ Import Namespace="MLAPI.Documentation" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
	<%= Model.DeclaringObject.Name %>.<%= Model.Name %> Action Help
</asp:Content>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript">
		$(document).ready(function () {
			$("a.xmlLink").each(function () {
				this.href += this.href.indexOf("?") > -1 ? "&" : "?";
				this.href += "contentType=xml";
			});
			$("a.jsonLink").each(function () {
				this.href += this.href.indexOf("?") > -1 ? "&" : "?";
				this.href += "contentType=json";
			});
		});
	</script>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
	<h1><%= Model.DeclaringObject.Name %>.<%= Model.Name %> Action</h1>
	<p><%= Model.Summary %></p>
	<% if (!string.IsNullOrEmpty(Model.Remarks)) %>
	<% { %>
		<h3>Remarks</h3>
		<p><%= Model.Remarks %></p>
	<% } %>
	<h2>Parameters</h2>
	<ul>
		<% foreach (ParameterDescription parameter in Model.Parameters) %>
		<% { %>
		<li>
			<strong><%= parameter.Name %></strong>
			<% if (!string.IsNullOrEmpty(parameter.DefaultValue)) %>
			<% { %>
				- (Default value: <%= parameter.DefaultValue %>)
			<% } %>
			- <%= parameter.Summary %>
		</li>
		<% } %>
	</ul>
	<h2>Response</h2>
	<p><%= Model.ResponseSummary %></p>
	<ul>
		<li><strong><%= Model.Response.Name %></strong> - <em><%= Model.Response.Name %></em> - <%= Model.Response.Summary %></li>
		<% Html.RenderPartial("~/bin/Views/Shared/ResponseHelp.ascx", Model.Response); %>
	</ul>
	<h2>Use Cases</h2>
	<h3>Examples</h3>
	<% if (Model.Examples != null && Model.Examples.Length > 0) %>
	<% { %>
		<table>
		<thead>
			<tr>
			<th>Description</th>
			<th>HTML</th>
			<th>XML</th>
			<th>JSON</th>
			</tr>
		</thead>
		<tbody>
		<% foreach (UseCaseDescription example in Model.Examples) %>
		<% { %>
			<tr>
			<td><%= example.Description %></td>
			<td><a href="<%= example.Url %>">HTML</a></td>
			<td><a href="<%= example.Url %>" class="xmlLink">XML</a></td>
			<td><a href="<%= example.Url %>" class="jsonLink">JSON</a></td>
			</tr>
		<% } %>
		</tbody>
		</table>
	<% } %>
	<% else %>
	<% { %>
		<em>None</em> (use the &lt;example&gt; documentation node on the action to document examples)
	<% } %>
	<h3>Errors</h3>
	<% if (Model.Errors != null && Model.Errors.Length > 0) %>
	<% { %>
		<table>
		<thead>
			<tr>
			<th>Description</th>
			<th>Error Code</th>
			<th>Http Status Code</th>
			<th>HTML</th>
			<th>XML</th>
			<th>JSON</th>
			</tr>
		</thead>
		<tbody>
		<% foreach (ErrorCaseDescription error in Model.Errors) %>
		<% { %>
			<tr>
			<td><%= error.Description%></td>
			<td><%= error.ErrorCode %></td>
			<td><%= error.HttpStatusCode %></td>
			<td><a href="<%= error.Url %>">HTML</a></td>
			<td><a href="<%= error.Url %>" class="xmlLink">XML</a></td>
			<td><a href="<%= error.Url %>" class="jsonLink">JSON</a></td>
			</tr>
		<% } %>
		</tbody>
		</table>
	<% } %>
	<% else %>
	<% { %>
		<em>None</em> (use the &lt;exception&gt; documentation node on the action to document errors)
	<% } %>
	<p><a href="../?mode=help">Back</a></p>
</asp:Content>