using AP.Business;
using System.Web.Mvc;

namespace AP.MVC.Controllers
{
    public class FoodItemsController : Controller
    {
        private readonly FoodItemsBusiness _foodItemsBusiness = new FoodItemsBusiness();

        public ActionResult Index(string filter)
        {
            ViewBag.CurrentFilter = filter;

            if (string.IsNullOrEmpty(filter) || filter == "All")
                return View(_foodItemsBusiness.GetFoodItems());

            return View(_foodItemsBusiness.SearchProducts(null, filter));
        }
    }
}
