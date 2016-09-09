﻿using System.Linq;
using System.Data.Entity;

namespace MY_MVC_APPLICATION.Controllers
{
	public partial class RolesController : Infrastructure.BaseController
	{
		public RolesController()
			: base()
		{
		}

		[System.Web.Mvc.HttpGet]
		public virtual System.Web.Mvc.ViewResult Index()
		{
			var varRoles =
				MyDatabaseContext.Roles
				.OrderBy(current => current.Name)
				.ToList()
				;

			return (View(model: varRoles));
		}

		[System.Web.Mvc.HttpGet]
		//public virtual System.Web.Mvc.ActionResult Details(System.Guid id)
		public virtual System.Web.Mvc.ActionResult Details(System.Guid? id)
		{
			//if (id == null)
			if (id.HasValue == false)
			{
				return (new System.Web.Mvc.HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest));
			}

			//Models.Role oRole =
			//	MyDatabaseContext.Roles.Find(id.Value);

			Models.Role oRole =
				MyDatabaseContext.Roles
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oRole == null)
			{
				return (HttpNotFound());
			}

			return (View(model: oRole));
		}

		//[System.Web.Mvc.HttpGet]
		//public virtual System.Web.Mvc.ViewResult Create()
		//{
		//	return (View());
		//}

		[System.Web.Mvc.HttpGet]
		public virtual System.Web.Mvc.ViewResult Create()
		{
			Models.Role oRole = new Models.Role();

			oRole.IsActive = true;

			return (View(model: oRole));
		}

		//[System.Web.Mvc.HttpPost]
		//[System.Web.Mvc.ValidateAntiForgeryToken]
		//public virtual System.Web.Mvc.ActionResult Create(System.Web.Mvc.FormCollection formCollection)
		//{
		//	Models.Role oRole = new Models.Role();

		//	string strIsActive = formCollection["IsActive"];

		//	if (string.Compare(strIsActive, "TRUE", ignoreCase: true) == 0)
		//	{
		//		oRole.IsActive = true;
		//	}
		//}

		//[System.Web.Mvc.HttpPost]
		//[System.Web.Mvc.ValidateAntiForgeryToken]
		//public virtual System.Web.Mvc.ActionResult Create(Models.Role role)
		//{
		//}

		//[System.Web.Mvc.HttpPost]
		//[System.Web.Mvc.ValidateAntiForgeryToken]
		//public virtual System.Web.Mvc.ActionResult Create([System.Web.Mvc.Bind(Include = "Name")] Models.Role role)
		//{
		//}

		//[System.Web.Mvc.HttpPost]
		//[System.Web.Mvc.ValidateAntiForgeryToken]
		//public virtual System.Web.Mvc.ActionResult Create([System.Web.Mvc.Bind(Exclude = "Id,IsActive")] Models.Role role)
		//{
		//}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		public virtual System.Web.Mvc.ActionResult Create([System.Web.Mvc.Bind(Include = "IsActive,Name")] Models.Role role)
		{
			// **************************************************
			// Business Validation

			if ((role.Name != null) && (role.Name.Length < 4) && (role.IsActive))
			{
				//ModelState.AddModelError
				//	(key: "Name", errorMessage: "Some business error message!");

				ModelState.AddModelError
					(key: string.Empty, errorMessage: "Some business error message!");
			}
			// **************************************************

			if (ModelState.IsValid)
			{
				MyDatabaseContext.Roles.Add(role);

				MyDatabaseContext.SaveChanges();

				return (RedirectToAction(MVC.Roles.Index()));
			}

			//return (View());
			return (View(model: role));
		}

		[System.Web.Mvc.HttpGet]
		public virtual System.Web.Mvc.ActionResult Edit(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (new System.Web.Mvc.HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest));
			}

			Models.Role oRole =
				MyDatabaseContext.Roles
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oRole == null)
			{
				return (HttpNotFound());
			}

			return (View(model: oRole));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		public virtual System.Web.Mvc.ActionResult Edit([System.Web.Mvc.Bind(Include = "Id,IsActive,Name")] Models.Role role)
		{
			// **************************************************
			Models.Role oOriginalRole =
				MyDatabaseContext.Roles.Find(role.Id);

			if (oOriginalRole == null)
			{
				return (HttpNotFound());
			}
			// **************************************************

			// **************************************************
			// Business Validation

			if ((role.Name != null) && (role.Name.Length < 4) && (role.IsActive))
			{
				//ModelState.AddModelError
				//	(key: "Name", errorMessage: "Some business error message!");

				ModelState.AddModelError
					(key: string.Empty, errorMessage: "Some business error message!");
			}
			// **************************************************

			if (ModelState.IsValid)
			{
				oOriginalRole.Name = role.Name;
				oOriginalRole.IsActive = role.IsActive;

				MyDatabaseContext.SaveChanges();

				return (RedirectToAction(MVC.Roles.Index()));
			}

			return (View(model: role));
		}

		[System.Web.Mvc.HttpGet]
		public virtual System.Web.Mvc.ActionResult Delete(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (new System.Web.Mvc.HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest));
			}

			Models.Role oRole =
				MyDatabaseContext.Roles
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oRole == null)
			{
				return (HttpNotFound());
			}

			return (View(model: oRole));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ActionName("Delete")]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		public virtual System.Web.Mvc.ActionResult DeleteConfirmed(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (new System.Web.Mvc.HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest));
			}

			Models.Role oRole =
				MyDatabaseContext.Roles
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oRole == null)
			{
				return (HttpNotFound());
			}

			MyDatabaseContext.Roles.Remove(oRole);

			MyDatabaseContext.SaveChanges();

			return (RedirectToAction(MVC.Roles.Index()));
		}
	}
}
