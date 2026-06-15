using System.Linq;
using System.Net;
using System.Web.Mvc;
using AP.Business;
using AP.Data;

namespace AP.MVC.Controllers
{
    public class UserRolesController : Controller
    {
        private readonly UserRoleBusiness _userRoleBusiness = new UserRoleBusiness();
        private readonly UsersBusiness _usersBusiness = new UsersBusiness();
        private readonly RoleBusiness _rolesBusiness = new RoleBusiness();

        public ActionResult Index()
        {
            return View(_userRoleBusiness.GetUserRoles());
        }

        public ActionResult Details(int? userId, int? roleId)
        {
            if (userId == null || roleId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var userRole = _userRoleBusiness.GetUserRoles()
                .FirstOrDefault(ur => ur.UserId == userId && ur.RoleId == roleId);

            if (userRole == null) return HttpNotFound();
            return View(userRole);
        }

        public ActionResult Create()
        {
            ViewBag.Users = new SelectList(_usersBusiness.GetUsers(), "UserId", "Username");
            ViewBag.Roles = new SelectList(_rolesBusiness.GetRoles(), "RoleId", "RoleName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,RoleId,Description")] UserRole userRole)
        {
            if (ModelState.IsValid)
            {
                _userRoleBusiness.SaveOrUpdate(userRole);
                return RedirectToAction("Index");
            }
            ViewBag.Users = new SelectList(_usersBusiness.GetUsers(), "UserId", "Username", userRole.UserId);
            ViewBag.Roles = new SelectList(_rolesBusiness.GetRoles(), "RoleId", "RoleName", userRole.RoleId);
            return View(userRole);
        }

        public ActionResult Edit(int? userId, int? roleId)
        {
            if (userId == null || roleId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var userRole = _userRoleBusiness.GetUserRoles()
                .FirstOrDefault(ur => ur.UserId == userId && ur.RoleId == roleId);

            if (userRole == null) return HttpNotFound();
            ViewBag.Users = new SelectList(_usersBusiness.GetUsers(), "UserId", "Username", userRole.UserId);
            ViewBag.Roles = new SelectList(_rolesBusiness.GetRoles(), "RoleId", "RoleName", userRole.RoleId);
            return View(userRole);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,RoleId,Description")] UserRole userRole)
        {
            if (ModelState.IsValid)
            {
                _userRoleBusiness.SaveOrUpdate(userRole);
                return RedirectToAction("Index");
            }
            ViewBag.Users = new SelectList(_usersBusiness.GetUsers(), "UserId", "Username", userRole.UserId);
            ViewBag.Roles = new SelectList(_rolesBusiness.GetRoles(), "RoleId", "RoleName", userRole.RoleId);
            return View(userRole);
        }

        public ActionResult Delete(int? userId, int? roleId)
        {
            if (userId == null || roleId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var userRole = _userRoleBusiness.GetUserRoles()
                .FirstOrDefault(ur => ur.UserId == userId && ur.RoleId == roleId);

            if (userRole == null) return HttpNotFound();
            return View(userRole);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int userId, int roleId)
        {
            _userRoleBusiness.Delete(userId, roleId);
            return RedirectToAction("Index");
        }
    }
}