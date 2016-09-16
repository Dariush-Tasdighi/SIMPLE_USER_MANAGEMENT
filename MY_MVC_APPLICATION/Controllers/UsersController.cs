using System.Linq;
using System.Data.Entity;

namespace MY_MVC_APPLICATION.Controllers
{
	public partial class UsersController : Infrastructure.BaseController
	{
		public UsersController()
		{
		}

		[System.Web.Mvc.HttpGet]
		public virtual System.Web.Mvc.ViewResult Index()
		{
			var varUsers =
				MyDatabaseContext.Users
				.OrderBy(current => current.FullName)
				.ToList()
				;

			return (View(model: varUsers));
		}

		[System.Web.Mvc.HttpGet]
		public virtual System.Web.Mvc.ActionResult Details(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (new System.Web.Mvc.HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest));
			}

			Models.User oUser =
				MyDatabaseContext.Users
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oUser == null)
			{
				return (HttpNotFound());
			}

			return (View(model: oUser));
		}

		[System.Web.Mvc.HttpGet]
		public virtual System.Web.Mvc.ViewResult Create()
		{
			Models.User oDefaultUser = new Models.User();

			oDefaultUser.Age = 20;

			// **************************************************
			var varRoles =
				MyDatabaseContext.Roles
				.Where(current => current.IsActive)
				.OrderBy(current => current.Name)
				.ToList()
				;

			ViewBag.RoleId =
				new System.Web.Mvc.SelectList
					(items: varRoles, dataValueField: "Id", dataTextField: "Name", selectedValue: null);
			// **************************************************

			return (View(model: oDefaultUser));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		public virtual System.Web.Mvc.ActionResult Create
			([System.Web.Mvc.Bind(Exclude = "Id")] Models.User user)
		{
			if (ModelState.IsValid)
			{
				MyDatabaseContext.Users.Add(user);

				MyDatabaseContext.SaveChanges();

				return RedirectToAction(MVC.Users.Index());
			}

			// **************************************************
			var varRoles =
				MyDatabaseContext.Roles
				.Where(current => current.IsActive)
				.OrderBy(current => current.Name)
				.ToList()
				;

			//ViewBag.RoleId =
			//	new System.Web.Mvc.SelectList
			//		(items: varRoles, dataValueField: "Id", dataTextField: "Name", selectedValue: null);

			ViewBag.RoleId =
				new System.Web.Mvc.SelectList
					(items: varRoles, dataValueField: "Id", dataTextField: "Name", selectedValue: user.RoleId);
			// **************************************************

			return (View(model: user));
		}

		[System.Web.Mvc.HttpGet]
		public virtual System.Web.Mvc.ActionResult Edit(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (new System.Web.Mvc.HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest));
			}

			Models.User oUser =
				MyDatabaseContext.Users
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oUser == null)
			{
				return (HttpNotFound());
			}

			// **************************************************
			var varRoles =
				MyDatabaseContext.Roles
				.Where(current => current.IsActive)
				.OrderBy(current => current.Name)
				.ToList()
				;

			ViewBag.RoleId =
				new System.Web.Mvc.SelectList
					(items: varRoles, dataValueField: "Id", dataTextField: "Name", selectedValue: oUser.RoleId);
			// **************************************************

			return (View(model: oUser));
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.ValidateAntiForgeryToken]
		public virtual System.Web.Mvc.ActionResult Edit
			([System.Web.Mvc.Bind(Exclude = "")] Models.User user)
		{
			// **************************************************
			Models.User oOriginalUser =
				MyDatabaseContext.Users
				.Where(current => current.Id == user.Id)
				.FirstOrDefault();

			if (oOriginalUser == null)
			{
				return (HttpNotFound());
			}
			// **************************************************

			if (ModelState.IsValid)
			{
				oOriginalUser.Age = user.Age;
				oOriginalUser.RoleId = user.RoleId;
				oOriginalUser.FullName = user.FullName;
				oOriginalUser.IsActive = user.IsActive;
				oOriginalUser.Password = user.Password;
				oOriginalUser.Username = user.Username;
				oOriginalUser.Description = user.Description;

				MyDatabaseContext.SaveChanges();

				return (RedirectToAction(MVC.Users.Index()));
			}

			// **************************************************
			var varRoles =
				MyDatabaseContext.Roles
				.Where(current => current.IsActive)
				.OrderBy(current => current.Name)
				.ToList()
				;

			ViewBag.RoleId =
				new System.Web.Mvc.SelectList
					(items: varRoles, dataValueField: "Id", dataTextField: "Name", selectedValue: user.RoleId);
			// **************************************************

			return (View(model: user));
		}

		[System.Web.Mvc.HttpGet]
		public virtual System.Web.Mvc.ActionResult Delete(System.Guid? id)
		{
			if (id.HasValue == false)
			{
				return (new System.Web.Mvc.HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest));
			}

			Models.User oUser =
				MyDatabaseContext.Users
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oUser == null)
			{
				return (HttpNotFound());
			}

			return (View(model: oUser));
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

			Models.User oUser =
				MyDatabaseContext.Users
				.Where(current => current.Id == id.Value)
				.FirstOrDefault();

			if (oUser == null)
			{
				return (HttpNotFound());
			}

			MyDatabaseContext.Users.Remove(oUser);

			MyDatabaseContext.SaveChanges();

			return (RedirectToAction(MVC.Users.Index()));
		}
	}
}
