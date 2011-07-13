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
	// Fields
		private Assembly _assembly;

	// Constructors
		public ServiceAreaDescription(Assembly assembly)
		{
			this._assembly = assembly;
		}

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
				return ServiceAreaDescription.GetFileName(this._assembly);
			}
		}

		public ObjectDescription[] Objects
		{
			get
			{
				return this._assembly
					.GetTypes()
					.Where(t => t.Name.EndsWith("Controller") && t.IsSubclassOf(typeof(Controller)) && t.Name != "HomeController")
					.Select(t => new ObjectDescription(t, this))
					.ToArray();
			}
		}

	// Methods
		static public XDocument GetServiceAreaDocumentation(Assembly assembly)
		{
			XDocument xml = null;
			string xmlDocPath = HttpContext.Current.Server.MapPath("~/bin/" + ServiceAreaDescription.GetFileName(assembly) + ".xml");
			if (File.Exists(xmlDocPath))
			{
				xml = XDocument.Load(xmlDocPath);
			}
			return xml;
		}

		static private string GetFileName(Assembly assembly)
		{
			string name = new FileInfo(assembly.Location).Name;
			return name.Substring(0, name.Length - 4);
		}
	}
}
