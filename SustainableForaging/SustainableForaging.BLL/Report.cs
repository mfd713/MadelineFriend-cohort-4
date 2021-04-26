using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SustainableForaging.Core.Repositories;
using SustainableForaging.Core.Models;


namespace SustainableForaging.BLL
{
    public class Report
    {
        private IForageRepository _forageRepo;
        private IItemRepository _items;
        private IForagerRepository _foragerRepo;
        private ForageService _forageService;

        public Report(IForageRepository forageRepo, IItemRepository items, IForagerRepository foragerRepo)
        {
            _forageRepo = forageRepo;
            _items = items;
            _forageService = new ForageService(forageRepo, foragerRepo, items);
        }

        public Result<Dictionary<Item,decimal>> GenerateDailyKGReport(DateTime date)
        {
            List<Forage> forages = _forageService.FindByDate(date);
            Result<Dictionary<Item, decimal>> result = new Result<Dictionary<Item, decimal>>();

            if (forages.Count > 0)
            {
                result.Value = _forageService.FindByDate(date)
                 .GroupBy(f => f.Item)
                 .ToDictionary(g => g.Key, g => g.Sum(k => k.Kilograms));
            }
            else
            {
                result.AddMessage("No forages for that date.");
            }

            return result;
        }

        public Result<Dictionary<Category,decimal>> GenerateDailyValuePerCategoryReport(DateTime date)
        {
            List<Forage> forages = _forageService.FindByDate(date);
            Result<Dictionary<Category, decimal>> result = new Result<Dictionary<Category, decimal>>();

            if(forages.Count > 0)
            {
                result.Value = _forageService.FindByDate(date)
                    .GroupBy(f => f.Item.Category)
                    .ToDictionary(g => g.Key, g => g.Sum(k => k.Value));
            }
            else
            {
                result.AddMessage("No forages for that date");
            }

            return result;
        }
    }
}
