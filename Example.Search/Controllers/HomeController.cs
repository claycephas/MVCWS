using System.Reflection;
using System.Web.Mvc;
using MLAPI.Documentation;

namespace MLAPI.Example.Search.Controllers
{
	/// <summary>
	/// Provides resources for the root of this service area.
	/// </summary>
	[Documented]
	public class HomeController : Controller
    {
	// Methods
		/// <summary>
		/// Displays the homepage for this service area.
		/// </summary>
		/// <returns>A view for the homepage for this service area.</returns>
        public ActionResult Index()
        {
			ServiceAreaDescription serviceArea = new ServiceAreaDescription(Assembly.GetExecutingAssembly());
            return View(serviceArea);
        }

    }
}
