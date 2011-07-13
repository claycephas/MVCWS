using System;

namespace MLAPI.Documentation
{
	public class UseCaseDescription
	{
	// Fields
		private ActionDescription _declaringAction;
		private string _description;
		private string _url;

	// Constructors
		public UseCaseDescription(string description, string url, ActionDescription declaringAction)
		{
			if (string.IsNullOrEmpty(description))
			{
				throw new ArgumentNullException("code");
			}
			if (declaringAction == null)
			{
				throw new ArgumentNullException("declaringAction");
			}
			this._url = url;
			this._description = description;
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

		public string Url
		{
			get { return this._url; }
		}
	}
}
