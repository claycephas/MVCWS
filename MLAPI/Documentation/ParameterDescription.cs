using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace MLAPI.Documentation
{
	public class ParameterDescription
	{
		public ActionDescription DeclaringAction { get; protected set; }
		public string Summary { get; protected set; }
		public string Name { get; protected set; }
		public ObjectDescription Type { get; protected set; }

		public ParameterDescription(ParameterInfo parameter, ActionDescription declaringAction)
			: this(parameter, declaringAction, new Stack<Type>()) { }

		public ParameterDescription(ParameterInfo parameter, ActionDescription declaringAction, Stack<Type> visitedTypes)
		{
			this.Name = parameter.Name;
			this.DeclaringAction = declaringAction;
			this.Type = new ObjectDescription(parameter.ParameterType, declaringAction.DeclaringObject.DeclaringServiceArea);

			XElement methodXml = ActionDescription.GetActionDocumentation((MethodInfo)parameter.Member);
			if (methodXml != null)
			{
				XElement parameterXml = methodXml.Elements().FirstOrDefault(e => e.Name == "param" && e.Attribute("name").Value == parameter.Name);
				if (parameterXml != null)
				{
					this.Summary = parameterXml.Value;
				}
			}
		}
	}
}
