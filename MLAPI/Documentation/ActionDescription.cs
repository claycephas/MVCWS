using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
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

		public ErrorCaseDescription[] Errors
		{
			get
			{
				ErrorCaseDescription[] errors = null;
				XElement methodXml = ActionDescription.GetActionDocumentation(this._method);
				if (methodXml != null)
				{
					errors = methodXml
						.Elements("exception")
						.Select(e => new ErrorCaseDescription(
							e.Value,
							e.Attribute("url") != null ? e.Attribute("url").Value : String.Empty,
							e.Attribute("errorCode").Value,
							Convert.ToInt32(e.Attribute("statusCode").Value),
							this)).ToArray();
				}
				return errors;
			}
		}

		public UseCaseDescription[] Examples
		{
			get
			{
				List<UseCaseDescription> examples = new List<UseCaseDescription>();
				// Add default example
				examples.Add(new UseCaseDescription("Default", ".", this));
				XElement methodXml = ActionDescription.GetActionDocumentation(this._method);
				if (methodXml != null)
				{
					examples.AddRange(methodXml
						.Elements("example")
						.Select(e => (UseCaseDescription)new UseCaseDescription(
							e.Value,
							e.Attribute("url") != null ? e.Attribute("url").Value : String.Empty,
							this)));
				}
				return examples.ToArray();
			}
		}

		public string Remarks
		{
			get
			{
				string summary = null;
				XElement methodXml = ActionDescription.GetActionDocumentation(this._method);
				if (methodXml != null)
				{
					XElement summaryXml = methodXml.Element("remarks");
					if (summaryXml != null)
					{
						summary = summaryXml.InnerText();
					}
				}
				return summary;
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

		public string ResponseSummary
		{
			get
			{
				string summary = null;
				XElement actionXml = ActionDescription.GetActionDocumentation(this._method);
				if (actionXml != null)
				{
					XElement returns = actionXml.Element("returns");
					if (returns != null)
					{
						summary = returns.Value;
					}
				}
				return summary;
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
						parameterString = "(" + string.Join(",", method.GetParameters().Select(x => GetTypeDocumentationName(x.ParameterType)).ToArray()) + ")";
					}
					string fullMethodName = method.DeclaringType.FullName + "." + method.Name + parameterString;
					methodXml = members.Elements().FirstOrDefault(e => e.Attribute("name").Value == "M:" + fullMethodName);
				}
			}
			return methodXml;
		}

		static private string GetTypeDocumentationName(Type type)
		{
			StringBuilder name = new StringBuilder(type.FullName);
			if (type.IsGenericType)
			{
				name = new StringBuilder(type.Namespace);
				name.Append(".");
				name.Append(type.Name.Substring(0, type.Name.IndexOf("`")));
				name.Append("{");
				name.Append(string.Join(",", type.GetGenericArguments().Select(t => GetTypeDocumentationName(t)).ToArray()));
				name.Append("}");
			}
			return name.ToString();
		}
	}
}
