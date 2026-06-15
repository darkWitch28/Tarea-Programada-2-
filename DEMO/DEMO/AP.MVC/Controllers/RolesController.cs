using AP.Business;
using AP.Data;
using System.Linq;
using System.Web.Mvc;

namespace AP.MVC.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleBusiness _roleBusiness = new RoleBusiness();

        public ActionResult Index()
        {
            return View(_roleBusiness.GetRoles());
        }

        public ActionResult Details(int id)
        {
            var role = _roleBusiness.GetRoles(id).FirstOrDefault();

            if (role == null)
                return HttpNotFound();

            return View(role);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Role role)
        {
            if (ModelState.IsValid)
            {
                _roleBusiness.SaveOrUpdate(role);
                return RedirectToAction("Index");
            }

            return View(role);
        }

        public ActionResult Edit(int id)
        {
            var role = _roleBusiness.GetRoles(id).FirstOrDefault();

            if (role == null)
                return HttpNotFound();

            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Role role)
        {
            if (ModelState.IsValid)
            {
                _roleBusiness.SaveOrUpdate(role);
                return RedirectToAction("Index");
            }

            return View(role);
        }

        public ActionResult Delete(int id)
        {
            var role = _roleBusiness.GetRoles(id).FirstOrDefault();

            if (role == null)
                return HttpNotFound();

            return View(role);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _roleBusiness.Delete(id);
            return RedirectToAction("Index");
        }
    }
}