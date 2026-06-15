using AP.Business;
using AP.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace AP.MVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly UsersBusiness _usersBusiness = new UsersBusiness();

        public ActionResult Index()
        {
            return View(_usersBusiness.GetUsers());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var user = _usersBusiness.GetUsers(id.Value).FirstOrDefault();
            if (user == null)
                return HttpNotFound();

            return View(user);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Username,Email,FullName,IsActive")] User user)
        {
            if (ModelState.IsValid)
            {
                _usersBusiness.SaveOrUpdate(user);
                return RedirectToAction("Index");
            }

            return View(user);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var user = _usersBusiness.GetUsers(id.Value).FirstOrDefault();
            if (user == null)
                return HttpNotFound();

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,Username,Email,FullName,IsActive,CreatedAt")] User user)
        {
            if (ModelState.IsValid)
            {
                _usersBusiness.SaveOrUpdate(user);
                return RedirectToAction("Index");
            }

            return View(user);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var user = _usersBusiness.GetUsers(id.Value).FirstOrDefault();
            if (user == null)
                return HttpNotFound();

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _usersBusiness.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
