using AP.Business;
using AP.Data;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System.Net;

namespace AP.MVC.Controllers
{
    public class FoodItemsController : Controller
    {
        private readonly FoodItemsBusiness _foodItemsBusiness = new FoodItemsBusiness();

        public ActionResult Index(string filter)
        {
            ViewBag.CurrentFilter = filter;

            ViewBag.Filters = new List<SelectListItem>
            {
                new SelectListItem { Value = "All",               Text = "Todos los productos" },
                new SelectListItem { Value = "Fruit",             Text = "Categoría: Fruit" },
                new SelectListItem { Value = "LowStockPerishable",Text = "Perecederos < 10 unidades" },
                new SelectListItem { Value = "Premium",           Text = "Productos Premium" },
                new SelectListItem { Value = "Top10Caros",        Text = "Top 10 más caros" },
                new SelectListItem { Value = "MenorStock",        Text = "Ordenar por menor stock" },
            };

            if (string.IsNullOrEmpty(filter) || filter == "All")
                return View(_foodItemsBusiness.GetFoodItems());

            return View(_foodItemsBusiness.SearchProducts(null, filter));
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var item = _foodItemsBusiness.GetFoodItems(id.Value).FirstOrDefault();
            if (item == null)
                return HttpNotFound();

            return View(item);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Category,Brand,Description,Price,Unit,QuantityInStock,ExpirationDate,IsPerishable,CaloriesPerServing,Ingredients,Barcode,Supplier,IsActive")] FoodItem foodItem)
        {
            if (ModelState.IsValid)
            {
                _foodItemsBusiness.SaveOrUpdate(foodItem);
                return RedirectToAction("Index");
            }
            return View(foodItem);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var item = _foodItemsBusiness.GetFoodItems(id.Value).FirstOrDefault();
            if (item == null)
                return HttpNotFound();

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FoodItemID,Name,Category,Brand,Description,Price,Unit,QuantityInStock,ExpirationDate,IsPerishable,CaloriesPerServing,Ingredients,Barcode,Supplier,DateAdded,IsActive,RoleId")] FoodItem foodItem)
        {
            if (ModelState.IsValid)
            {
                _foodItemsBusiness.SaveOrUpdate(foodItem);
                return RedirectToAction("Index");
            }
            return View(foodItem);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var item = _foodItemsBusiness.GetFoodItems(id.Value).FirstOrDefault();
            if (item == null)
                return HttpNotFound();

            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _foodItemsBusiness.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
