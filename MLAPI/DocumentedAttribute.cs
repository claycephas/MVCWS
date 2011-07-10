﻿using System;
using System.Linq;
using System.Web.Mvc;
using MLAPI.Documentation;

namespace MLAPI
{
	/// <summary>
	/// Handles requests for documentation and automatically loads the appropriate view.
	/// </summary>
	public class DocumentedAttribute : ActionFilterAttribute
	{
		/// <summary>
		/// Handle the action executing event and set the result to a view if documentation is requested.
		/// </summary>
		/// <param name="filterContext">The context of the event.</param>
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			base.OnActionExecuting(filterContext);

			ServiceAreaDescription serviceArea = new ServiceAreaDescription(filterContext.Controller.GetType().Assembly);
			if (filterContext.HttpContext.Request["mode"] == "help")
			{
				ObjectDescription type = new ObjectDescription(filterContext.Controller.GetType(), serviceArea);
				if (filterContext.ActionDescriptor.ActionName.Equals("Index", StringComparison.InvariantCultureIgnoreCase))
				{
					filterContext.Result = new ViewResult()
					{
						ViewName = "ObjectHelp",
						ViewData = new ViewDataDictionary<ObjectDescription>(type),
					};
				}
				else
				{
					ActionDescription action = type.Actions.First(a => a.Name.ToLower() == filterContext.ActionDescriptor.ActionName.ToLower());
					filterContext.Result = new ViewResult()
					{
						ViewName = "ActionHelp",
						ViewData = new ViewDataDictionary<ActionDescription>(action),
					};
				}
			}
		}
	}
}
