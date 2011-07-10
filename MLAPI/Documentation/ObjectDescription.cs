using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace MLAPI.Documentation
{
	public class ObjectDescription
	{
	// Constants
		private static readonly string[] FundamentalTypes = new string[]
		{
			"System."
		};

	// Constructors
		public ObjectDescription(Type type, ServiceAreaDescription declaringServiceArea)
		{
			this._type = this._innerType = type;
			if (type.IsGenericType && type.Name.StartsWith("ServiceResult"))
			{
				this._innerType = type.GetGenericArguments()[0];
			}
			// If the type is an array, get the element type
			if (type.IsArray)
			{
				this._innerType = type.GetElementType();
			}

			this._declaringServiceArea = declaringServiceArea;
		}

	// Fields
		private ServiceAreaDescription _declaringServiceArea;
		private Type _innerType;
		private Type _type;

	// Properties
		public ActionDescription[] Actions
		{
			get
			{
				ActionDescription[] actions = null;
				if (!ObjectDescription.FundamentalTypes.Any(t => this._innerType.FullName.Contains(t)))
				{
					actions = this._innerType
						.GetMethods()
						.Where(m => m.DeclaringType == this._innerType && m.IsPublic)
						.Select(m => new ActionDescription(m, this))
						.OrderBy(a => a.Name)
						.ToArray();
				}
				return actions;
			}
		}

		public ServiceAreaDescription DeclaringServiceArea
		{
			get
			{
				return this._declaringServiceArea;
			}
		}

		public MemberDescription[] Members
		{
			get
			{
				MemberDescription[] members = null;
				if (!ObjectDescription.FundamentalTypes.Any(t => this._innerType.FullName.Contains(t)))
				{
					IEnumerable<MemberDescription> fields = this._innerType
						.GetFields()
						.Where(f => f.DeclaringType == this._innerType && f.IsPublic && !f.IsStatic)
						.Select(f => new MemberDescription(f, this));
					IEnumerable<MemberDescription> properties = this._innerType
						.GetProperties()
						.Where(p => p.DeclaringType == this._innerType && p.GetGetMethod().IsPublic && !p.GetGetMethod().IsStatic)
						.Select(p => new MemberDescription(p, this));
					members = fields.Union(properties).OrderBy(f => f.Name).ToArray();
				}
				return members;
			}
		}

		public string Name
		{
			get
			{
				// Include the [] for array types
				if (this._type.IsArray)
				{
					return this._type.Name;
				}
				else
				{
					return this._innerType.Name.Replace("Controller", String.Empty);
				}
			}
		}

		public string Summary
		{
			get
			{
				string summary = null;
				XElement typeXml = ObjectDescription.GetTypeDocumentation(this._innerType);
				if (typeXml != null)
				{
					XElement summaryXml = typeXml.Element("summary");
					if (summaryXml != null)
					{
						summary = summaryXml.Value;
					}
				}
				return summary;
			}
		}

		public static XElement GetTypeDocumentation(Type type)
		{
			XElement typeXml = null;
			XDocument xml = ServiceAreaDescription.GetServiceDocumentation(type.Assembly);
			if (xml != null)
			{
				XElement members = xml.Root.Element("members");
				if (members != null)
				{
					typeXml = members.Elements().FirstOrDefault(e => e.Attribute("name").Value == "T:" + type.FullName);
				}
			}
			return typeXml;
		}
	}
}
