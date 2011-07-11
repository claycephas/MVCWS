using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace MLAPI.Documentation
{
	public class ActionDescription
	{
	// Fields
		private ObjectDescription _declaringObject;
		private MethodInfo _method;

	// Constructors
		public ActionDescription(MethodInfo method, ObjectDescription declaringObject)
		{
			this._declaringObject = declaringObject;
			this._method = method;
		}

	// Properties
		public ObjectDescription DeclaringObject
		{
			get
			{
				return this._declaringObject;
			}
		}

		public string Summary
		{
			get
			{
				string summary = null;
				XElement methodXml = ActionDescription.GetActionDocumentation(this._method);
				if (methodXml != null)
				{
					XElement summaryXml = methodXml.Element("summary");
					if (summaryXml != null)
					{
						summary = summaryXml.Value;
					}
				}
				return summary;
			}
		}

		public string Name
		{
			get
			{
				return this._method.Name;
			}
		}

		public ParameterDescription[] Parameters
		{
			get
			{
				List<ParameterDescription> parameterList = new List<ParameterDescription>();
				foreach (ParameterInfo parameter in this._method.GetParameters())
				{
					parameterList.Add(new ParameterDescription(parameter, this));
				}
				return parameterList.ToArray();
			}
		}

		public ObjectDescription Response
		{
			get
			{
				return new ObjectDescription(this._method.ReturnType, this.DeclaringObject.DeclaringServiceArea);
			}
		}

	// Methods
		static public XElement GetActionDocumentation(MethodInfo method)
		{
			XElement methodXml = null;
			XDocument xml = ServiceAreaDescription.GetServiceAreaDocumentation(method.DeclaringType.Assembly);
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
