<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html />
<html>
<head>
	<title>Article</title>
</head>
<body>
	<div>
		<h1>
			Article
		</h1>
		<ul>
			<li>
			<a href="find/">Find</a>
			<ul>
				<li>Examples:</li>
				<li><a href="find/?topic=Parks&contentType=json">JSON Format</a></li>
				<li><a href="find/?topic=Parsk&contentType=xml">XML Format</a></li>
				<li><a href="find/?topic=Parks">Find articles about Parks</a></li>
				<li><a href="find/?topic=Health">Find articles about Health</a></li>
				<li><a href="find/?magicWord=now">Find Unauthorized</a></li>
			</ul>
			</li>
		</ul>
	</div>
</body>
</html>
