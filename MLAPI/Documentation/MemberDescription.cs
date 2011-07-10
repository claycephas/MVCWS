using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace MLAPI.Documentation
{
	public class MemberDescription
	{
		public ObjectDescription DeclaringObject { get; protected set; }
		public string Summary { get; protected set; }
		public string Name { get; protected set; }
		public ObjectDescription Type { get; protected set; }

		public MemberDescription(FieldInfo field, ObjectDescription declaringObject)
		{
			this.Name = field.Name;
			this.DeclaringObject = declaringObject;
			this.Type = new ObjectDescription(field.FieldType, declaringObject.DeclaringServiceArea);

			XDocument xml = ServiceAreaDescription.GetServiceDocumentation(field.DeclaringType.Assembly);
			if (xml != null)
			{
				XElement members = xml.Root.Element("members");
				if (members != null)
				{
					XElement fieldXml = members.Elements().FirstOrDefault(e => e.Attribute("name").Value == "F:" + field.DeclaringType.FullName + "." + field.Name);
					if (fieldXml != null)
					{
						XElement summary = fieldXml.Element("summary");
						if (summary != null)
						{
							this.Summary = summary.Value;
						}
					}
				}
			}
		}

		public MemberDescription(PropertyInfo property, ObjectDescription declaringObject)
		{
			this.Name = property.Name;
			this.DeclaringObject = declaringObject;
			this.Type = new ObjectDescription(property.PropertyType, declaringObject.DeclaringServiceArea);

			XDocument xml = ServiceAreaDescription.GetServiceDocumentation(property.DeclaringType.Assembly);
			if (xml != null)
			{
				XElement members = xml.Root.Element("members");
				if (members != null)
				{
					XElement propertyXml = members.Elements().FirstOrDefault(e => e.Attribute("name").Value == "P:" + property.DeclaringType.FullName + "." + property.Name);
					if (propertyXml != null)
					{
						XElement summary = propertyXml.Element("summary");
						if (summary != null)
						{
							this.Summary = summary.Value;
						}
					}
				}
			}
		}
	}
}
