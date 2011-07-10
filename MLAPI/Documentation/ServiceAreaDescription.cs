using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace MLAPI.Documentation
{
	public class ServiceAreaDescription
	{
	// Constructors
		public ServiceAreaDescription(Assembly assembly)
		{
			this._assembly = assembly;
		}

	// Fields
		private Assembly _assembly;

	// Properties
		public string Summary
		{
			get
			{
				return "TODO: Service Area";
			}
		}

		public string Name
		{
			get
			{
				string name = new FileInfo(this._assembly.Location).Name;
				return name.Substring(0, name.Length - 4);
			}
		}

		public ObjectDescription[] Objects
		{
			get
			{
				return this._assembly
					.GetTypes()
					.Where(t => t.Name.EndsWith("Controller") && t.IsSubclassOf(typeof(Controller)))
					.Select(t => new ObjectDescription(t, this))
					.ToArray();
			}
		}

	// Methods
		public static XDocument GetServiceDocumentation(Assembly assembly)
		{
			string xmlDocPath = HttpContext.Current.Server.MapPath("~/bin/" + new ServiceAreaDescription(assembly).Name + ".xml");
			XDocument xml = null;
			if (File.Exists(xmlDocPath))
			{
				xml = XDocument.Load(xmlDocPath);
			}
			return xml;
		}
	}
}
