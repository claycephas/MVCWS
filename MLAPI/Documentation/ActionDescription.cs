using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace MLAPI.Documentation
{
	public class ActionDescription
	{
		public ObjectDescription DeclaringObject { get; protected set; }
		public string Summary { get; protected set; }
		public string Name { get; protected set; }
		public ParameterDescription[] Parameters { get; protected set; }
		public ObjectDescription Response { get; protected set; }

		public ActionDescription(MethodInfo method, ObjectDescription declaringObject)
			: this(method, declaringObject, new Stack<Type>()) { }

		public ActionDescription(MethodInfo method, ObjectDescription declaringObject, Stack<Type> visitedTypes)
		{
			this.DeclaringObject = declaringObject;
			this.Name = method.Name;
			FillResponse(method, visitedTypes);
			FillDocumentation(method);
			FillParameters(method, visitedTypes);
		}

		private void FillDocumentation(MethodInfo method)
		{
			// Fill in the information from the XML documentation file
			XElement methodXml = ActionDescription.GetActionDocumentation(method);
			if (methodXml != null)
			{
				XElement summaryXml = methodXml.Element("summary");
				if (summaryXml != null)
				{
					this.Summary = summaryXml.Value;
				}
			}
		}

		private void FillParameters(MethodInfo method, Stack<Type> visitedTypes)
		{
			List<ParameterDescription> parameterList = new List<ParameterDescription>();
			foreach (ParameterInfo parameter in method.GetParameters())
			{
				parameterList.Add(new ParameterDescription(parameter, this, visitedTypes));
			}
			this.Parameters = parameterList.ToArray();
		}

		private void FillResponse(MethodInfo method, Stack<Type> visitedTypes)
		{
			this.Response = new ObjectDescription(method.ReturnType, this.DeclaringObject.DeclaringServiceArea);
		}

		public static XElement GetActionDocumentation(MethodInfo method)
		{
			XElement methodXml = null;
			XDocument xml = ServiceAreaDescription.GetServiceDocumentation(method.DeclaringType.Assembly);
			if (xml != null)
			{
				XElement members = xml.Root.Element("members");
				if (members != null)
				{
					string parameterString = String.Empty;
					if (method.GetParameters().Length > 0)
					{
						parameterString = "(" + string.Join(",", method.GetParameters().Select(x => x.ParameterType.FullName).ToArray()) + ")";
					}
					string fullMethodName = method.DeclaringType.FullName + "." + method.Name + parameterString;
					methodXml = members.Elements().FirstOrDefault(e => e.Attribute("name").Value == "M:" + fullMethodName);
				}
			}
			return methodXml;
		}
	}
}
