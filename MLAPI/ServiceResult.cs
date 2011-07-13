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

		/// <summary>
		/// Constructs a new result for an error case.
		/// </summary>
		/// <param name="error">The error that occured.</param>
		public ServiceResult(Error error)
		{
			this._error = error;
		}

	// Fields
		private T _data;
		private Error _error;

	// Properties
		/// <summary>
		/// Gets the data that is to be returned in the result or <c>null</c> if this is an error.
		/// </summary>
		public T Data
		{
			get
			{
				return this._data;
			}
		}

		/// <summary>
		/// Gets the error that occured or <c>null</c> if this is not an error.
		/// </summary>
		public Error Error
		{
			get
			{
				return this._error;
			}
		}

		/// <summary>
		/// Gets a value indicating whether or not this is an error.
		/// </summary>
		public bool IsError
		{
			get
			{
				return this._error != null;
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
			if (this.IsError)
			{
				result = this._error;
			}

			string contentType = query["contentType"];
			if (contentType == "json")
			{
				new JsonResult()
				{
					Data = result,
					JsonRequestBehavior = JsonRequestBehavior.AllowGet,
				}.ExecuteResult(context);
			}
			else if (contentType == "xml" || this.IsError)
			{
				context.HttpContext.Response.ContentType = "text/xml";
				XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
				ns.Add("", "");
				XmlSerializer xs = new XmlSerializer(result.GetType());
				xs.Serialize(context.HttpContext.Response.OutputStream, result, ns);
			}
			else
			{
				new ViewResult()
				{
					ViewData = new ViewDataDictionary(result),
				}.ExecuteResult(context);
			}

			if (this.IsError)
			{
				context.HttpContext.Response.StatusCode = this._error.StatusCode;
			}
		}
	}
}
