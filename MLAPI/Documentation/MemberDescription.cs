using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace MLAPI.Documentation
{
	public class MemberDescription
	{
	// Fields
		private ObjectDescription _declaringObject;
		private MemberInfo _member;

	// Constructors
		public MemberDescription(FieldInfo field, ObjectDescription declaringObject)
		{
			this._declaringObject = declaringObject;
			this._member = field;
		}

		public MemberDescription(PropertyInfo property, ObjectDescription declaringObject)
		{
			this._declaringObject = declaringObject;
			this._member = property;
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
				XElement xml = null;
				if (this._member is FieldInfo)
				{
					xml = MemberDescription.GetFieldDocumentation((FieldInfo)this._member);
				}
				else
				{
					xml = MemberDescription.GetPropertyDocumentation((PropertyInfo)this._member);
				}
				if (xml != null)
				{
					XElement summaryXml = xml.Element("summary");
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
				return this._member.Name;
			}
		}

		public ObjectDescription Type
		{
			get
			{
				System.Type type = this._member is FieldInfo ? ((FieldInfo)this._member).FieldType : ((PropertyInfo)this._member).PropertyType;
				return new ObjectDescription(type, this._declaringObject.DeclaringServiceArea);
			}
		}

	// Methods
		static public XElement GetFieldDocumentation(FieldInfo field)
		{
			XElement fieldXml = null;
			XDocument xml = ServiceAreaDescription.GetServiceAreaDocumentation(field.DeclaringType.Assembly);
			if (xml != null)
			{
				XElement members = xml.Root.Element("members");
				if (members != null)
				{
					fieldXml = members.Elements().FirstOrDefault(e => e.Attribute("name").Value == "F:" + field.DeclaringType.FullName + "." + field.Name);
				}
			}
			return fieldXml;
		}

		static public XElement GetPropertyDocumentation(PropertyInfo property)
		{
			XElement propertyXml = null;
			XDocument xml = ServiceAreaDescription.GetServiceAreaDocumentation(property.DeclaringType.Assembly);
			if (xml != null)
			{
				XElement members = xml.Root.Element("members");
				if (members != null)
				{
					propertyXml = members.Elements().FirstOrDefault(e => e.Attribute("name").Value == "P:" + property.DeclaringType.FullName + "." + property.Name);
				}
			}
			return propertyXml;
		}
	}
}
