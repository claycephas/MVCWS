using System;
using System.Web.Mvc;
using MLAPI.Example.Content.Models;
using MLAPI;

namespace MLAPI.Example.Content.Controllers
{
	/// <summary>
	/// Provides ubiquitous access to content for use around the web.
	/// </summary>
	[Documented]
    public class StringController : Controller
    {
		/// <summary>
		/// TODO
		/// </summary>
		/// <returns></returns>
		public ActionResult Index()
		{
			return View();
		}

		/// <summary>
		/// Finds content related to a topic.
		/// </summary>
		/// <param name="topic">A topic or category.</param>
		/// <returns>Content related to a topic.</returns>
        public ServiceResult<StringContent> Find(string topic)
        {
			StringContent content = new StringContent()
			{
				Id = Guid.NewGuid().ToString(),
				Content = "This is some content about " + topic + "."
			};
			return new ServiceResult<StringContent>(content);
        }
    }
}
