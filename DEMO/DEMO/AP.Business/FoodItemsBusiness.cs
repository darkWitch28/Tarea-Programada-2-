using AP.Data;
using AP.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.Business
{
    public class FoodItemsBusiness
    {
        private readonly FoodItemRepository _foodItemRepository;

        public FoodItemsBusiness()
        {
            _foodItemRepository = new FoodItemRepository();
        }

        public IEnumerable<FoodItem> GetFoodItems(int id = 0)
        {
            if (id == 0)
                return _foodItemRepository.GetAll();
            else
                return new List<FoodItem> { _foodItemRepository.GetById(id) };
        }

        public bool SaveOrUpdate(FoodItem foodItem)
        {
            if (foodItem.FoodItemID == 0)
                _foodItemRepository.Add(foodItem);
                //if (ProductValidations.IsWithinTimeRange())
                //  _repositoryProduct.Add(foodItem);
                //else
                //throw new System.Exception("Products cannot be added outside of the allowed time range.");
            else
                _foodItemRepository.Update(foodItem);
            return true;
        }

        public void Delete(int id)
        {
            _foodItemRepository.Delete(id);
        }

        public IEnumerable<FoodItem> SearchProducts(string criteria, string field)
        {
            var products = GetFoodItems(id: 0);
            switch (field)
            {
                case "Fruit":
                    return products.Where(p => p.Category == "Fruit");
                case "LowStockPerishable":
                    return products.Where(p =>
                        p.IsPerishable == true &&
                        (p.QuantityInStock ?? 0) < 10);
                case "Premium":
                    return products.Where(p =>
                        p.Price > 20 &&
                        (p.CaloriesPerServing ?? 0) > 300);
                case "Name":
                    return products.Where(p => p.Name.ToLower().Contains(criteria.ToLower()));
                case "Description":
                    return products.Where(p => p.Description.ToLower().Contains(criteria.ToLower()));
                case "Category":
                    return products.Where(p => p.Category.ToLower() != criteria.ToLower());
                case "Brand":
                    return products.Where(p => p.Brand.ToLower() != criteria.ToLower());
                case "Top10Caros":
                    return GetTop10MostExpensive();

                case "MenorStock":
                    return GetOrderedByLowestStock();
                default:
                    return new List<FoodItem>();
            }
        }

        public IEnumerable<FoodItem> GetTop10MostExpensive()
        {
            return _foodItemRepository.GetAll()
                .OrderByDescending(f => f.Price)
                .Take(10);
        }

        public IEnumerable<FoodItem> GetOrderedByLowestStock()
        {
            return _foodItemRepository.GetAll()
                .OrderBy(f => f.QuantityInStock);
        }
    }
}
