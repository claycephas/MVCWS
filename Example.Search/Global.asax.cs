using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MLAPI.Example.Search
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	/// <summary>
	/// The main MVC application.
	/// </summary>
	public class MvcApplication : HttpApplication
	{
	// Methods
		/// <summary>
		/// Handle the application start event.
		/// </summary>
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			RegisterRoutes(RouteTable.Routes);
		}

		/// <summary>
		/// Registers all the MVC routes for the application.
		/// </summary>
		/// <param name="routes">The collection to populate.</param>
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}", // URL with parameters
				new { controller = "Home", action = "Index" } // Parameter defaults
			);

		}
	}
}