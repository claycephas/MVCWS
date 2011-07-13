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

		public string DefaultValue
		{
			get
			{
				string defaultValue = null;
				XElement parameterXml = ParameterDescription.GetParameterDocumentation(this._parameter);
				if (parameterXml != null && parameterXml.Attribute("default") != null)
				{
					defaultValue = parameterXml.Attribute("default").Value;
				}
				return defaultValue;
			}
		}

		public string Summary
		{
			get
			{
				string summary = null;
				XElement parameterXml = ParameterDescription.GetParameterDocumentation(this._parameter);
				if (parameterXml != null)
				{
					summary = parameterXml.InnerText();
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
