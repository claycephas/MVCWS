using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace MLAPI.Documentation
{
	public class ParameterDescription
	{
	// Fields
		private ActionDescription _declaringAction;
		private ParameterInfo _parameter;

	// Constructors
		public ParameterDescription(ParameterInfo parameter, ActionDescription declaringAction)
		{
			this._declaringAction = declaringAction;
			this._parameter = parameter;
		}

	// Properties
		public ActionDescription DeclaringAction
		{
			get
			{
				return this._declaringAction;
			}
		}

		public string Summary
		{
			get
			{
				string summary = null;
				XElement methodXml = ActionDescription.GetActionDocumentation((MethodInfo)this._parameter.Member);
				if (methodXml != null)
				{
					XElement parameterXml = methodXml.Elements().FirstOrDefault(e => e.Name == "param" && e.Attribute("name").Value == this._parameter.Name);
					if (parameterXml != null)
					{
						summary = parameterXml.Value;
					}
				}
				return summary;
			}
		}

		public string Name
		{
			get
			{
				return this._parameter.Name;
			}
		}

		public ObjectDescription Type
		{
			get
			{
				return new ObjectDescription(this._parameter.ParameterType, this._declaringAction.DeclaringObject.DeclaringServiceArea);
			}
		}

	// Methods
		static public XElement GetParameterDocumentation(ParameterInfo parameter)
		{
			XElement parameterXml = null;
			XElement methodXml = ActionDescription.GetActionDocumentation((MethodInfo)parameter.Member);
			if (methodXml != null)
			{
				parameterXml = methodXml.Elements().FirstOrDefault(e => e.Name == "param" && e.Attribute("name").Value == parameter.Name);
			}
			return parameterXml;
		}
	}
}
