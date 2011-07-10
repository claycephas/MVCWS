using System.Collections.Specialized;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace MLAPI
{
	/// <summary>
	/// Handles a result of a service call.
	/// </summary>
	/// <typeparam name="T">The type of object that is to be returned.</typeparam>
	public class ServiceResult<T> : ActionResult
	{
	// Constructors
		/// <summary>
		/// Constructs a new result that represents <paramref name="data"/>.
		/// </summary>
		/// <param name="data">The data that is to be returned in the result.</param>
		public ServiceResult(T data)
		{
			this._data = data;
		}

	// Fields
		private T _data;

	// Properties
		/// <summary>
		/// Gets the data that is to be returned in the result.
		/// </summary>
		public T Data
		{
			get
			{
				return this._data;
			}
		}

	// Methods
		/// <summary>
		/// Uses the requested format to serialize the data to the response.
		/// </summary>
		/// <param name="context">The context of the controller that constructed <see cref="P:Data"/>.</param>
		public override void ExecuteResult(ControllerContext context)
		{
			NameValueCollection query = context.HttpContext.Request.QueryString;
			object result = this._data;

			string contentType = query["contentType"];
			if (contentType == "json")
			{
				new JsonResult()
				{
					Data = result,
					JsonRequestBehavior = JsonRequestBehavior.AllowGet,
				}.ExecuteResult(context);
			}
			else if (contentType == "xml")
			{
				context.HttpContext.Response.ContentType = "application/xml";
				XmlSerializer s = new XmlSerializer(result.GetType());
				s.Serialize(context.HttpContext.Response.OutputStream, result);
			}
			else
			{
				new ViewResult()
				{
					ViewData = new ViewDataDictionary(result),
				}.ExecuteResult(context);
			}
		}
	}
}
