using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MLAPI.Example.Search.Models;
using System.Net;

namespace MLAPI.Example.Search.Controllers
{
	/// <summary>
	/// Provides access to article data.
	/// </summary>
	[Documented]
	public class ArticleController : Controller
	{
	// Methods
		/// <summary>
		/// Finds a list of articles related to a topic.
		/// </summary>
		/// <param name="topic">The name of a topic.</param>
		/// <returns>A list of articles related to a topic.</returns>
		/// <example url="?topic=Astronomy">Articles about Astronomy</example>
		/// <example url=".">Articles about nothing</example>
		/// <exception errorCode="NastyTopic" statusCode="400" url="?topic=Catastrophes">Articles about Catastrophes</exception>
		public ServiceResult<ArticleList> Find(string topic)
		{
			if (topic == "Catastrophes")
			{
				return new ServiceResult<ArticleList>(new Error()
				{
					Code = "NastyTopic",
					Message = "Think happy thoughts instead.",
					StatusCode = (int)HttpStatusCode.BadRequest,
				});
			}


			ArticleList page = new ArticleList();
			List<Article> articles = new List<Article>();
			for (int i = 0; i < 10; ++i)
			{
				articles.Add(new Article()
				{
					Id = Guid.NewGuid().ToString(),
					Headline = "Headline " + (i + 1).ToString(),
					Body = "Body " + (i + 1).ToString(),
					Topics = new Topic[]
					{
						new Topic() { Id = Guid.NewGuid().ToString(), Name = topic },
						new Topic() { Id = Guid.NewGuid().ToString(), Name = "Local Events" },
					}
				});
			}
			// Occasionally swap the last topic
			if (new Random().Next(3) == 0)
				articles[5].Topics[1].Name = "Remote Events";

			page.Articles = articles.ToArray();

			return new ServiceResult<ArticleList>(page);
		}

		/// <summary>
		/// Displays the homepage for the Article object.
		/// </summary>
		/// <returns>The associated view.</returns>
		public ActionResult Index()
		{
			return View();
		}
	}
}
