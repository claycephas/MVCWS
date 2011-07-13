using System;

namespace MLAPI.Documentation
{
	public class ErrorCaseDescription
	{
	// Fields
		private ActionDescription _declaringAction;
		private string _description;
		private string _errorCode;
		private int _httpStatusCode;
		private string _url;

	// Constructors
		public ErrorCaseDescription(string description, string url, string errorCode, int httpStatusCode, ActionDescription declaringAction)
		{
			if (string.IsNullOrEmpty(description))
			{
				throw new ArgumentNullException("code");
			}
			if (declaringAction == null)
			{
				throw new ArgumentNullException("declaringAction");
			}
			this._description = description;
			this._url = url;
			this._errorCode = errorCode;
			this._httpStatusCode = httpStatusCode;
			this._declaringAction = declaringAction;
		}

	// Properties
		public ActionDescription DeclaringAction
		{
			get { return this._declaringAction; }
		}

		public string Description
		{
			get { return this._description; }
		}

		public string ErrorCode
		{
			get { return this._errorCode; }
		}

		public int HttpStatusCode
		{
			get { return this._httpStatusCode; }
		}

		public string Url
		{
			get { return this._url; }
		}
	}
}
