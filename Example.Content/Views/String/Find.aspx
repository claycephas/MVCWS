<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<StringContent>" %>
<%@ Import Namespace="MLAPI.Example.Content.Models" %>

<!DOCTYPE html />

<html>
<head>
    <title>Find</title>
</head>
<body>
	<%= Model.Content %>
</body>
</html>
