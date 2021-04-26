using SustainableForaging.BLL;
using SustainableForaging.Core.Exceptions;
using SustainableForaging.Core.Models;
using System;
using System.Collections.Generic;

namespace SustainableForaging.UI
{
    public class Controller
    {
        private readonly ForagerService foragerService;
        private readonly ForageService forageService;
        private readonly ItemService itemService;
        private readonly View view;
        private readonly Report reports;

        public Controller(ForagerService foragerService, ForageService forageService, ItemService itemService, View view, Report reports)
        {
            this.foragerService = foragerService;
            this.forageService = forageService;
            this.itemService = itemService;
            this.view = view;
            this.reports = reports;
        }

        public void Run()
        {
            view.DisplayHeader("Welcome to Sustainable Foraging");
            try
            {
                RunAppLoop();
            }
            catch(RepositoryException ex)
            {
                view.DisplayException(ex);
            }
            view.DisplayHeader("Goodbye.");
        }

        private void RunAppLoop()
        {
            MainMenuOption option;
            do
            {
                option = view.SelectMainMenuOption();
                switch(option)
                {
                    case MainMenuOption.ViewForagesByDate:
                        ViewByDate();
                        break;
                    case MainMenuOption.ViewItems:
                        ViewItems();
                        break;
                    case MainMenuOption.AddForage:
                        AddForage();
                        break;
                    case MainMenuOption.AddForager:
                        AddForager();
                        view.EnterToContinue();
                        break;
                    case MainMenuOption.AddItem:
                        AddItem();
                        break;
                    case MainMenuOption.ReportKgPerItem:
                        ReportKgPerItem();
                        view.EnterToContinue();
                        break;
                    case MainMenuOption.ReportCategoryValue:
                        ReportValuePerCategory();
                        view.EnterToContinue();
                        break;
                    case MainMenuOption.ViewForagers:
                        ViewForagers();
                        break;
                    case MainMenuOption.Generate:
                        Generate();
                        break;
                }
            } while(option != MainMenuOption.Exit);
        }

        // top level menu
        private void ViewByDate()
        {
            DateTime date = view.GetForageDate();
            List<Forage> forages = forageService.FindByDate(date);
            view.DisplayForages(forages);
            view.EnterToContinue();
        }

        private void ViewItems()
        {
            view.DisplayHeader(MainMenuOption.ViewItems.ToLabel());
            Category category = view.GetItemCategory();
            List<Item> items = itemService.FindByCategory(category);
            view.DisplayHeader("Items");
            view.DisplayItems(items);
            view.EnterToContinue();
        }

        //add a view foragaers method
        private void ViewForagers()
        {
            view.DisplayHeader(MainMenuOption.ViewForagers.ToLabel());
            string lastNamePrefix = view.GetForagerNamePrefix();
            List<Forager> foragers = foragerService.FindByLastName(lastNamePrefix);

            view.DisplayHeader("Foragers");
            view.DisplayForagers(foragers);
            view.EnterToContinue();
        }

        private void AddForage()
        {
            view.DisplayHeader(MainMenuOption.AddForage.ToLabel());
            Forager forager = GetForager();
            if(forager == null)
            {
                return;
            }
            Item item = GetItem();
            if(item == null)
            {
                return;
            }
            Forage forage = view.MakeForage(forager, item);
            Result<Forage> result = forageService.Add(forage);
            if(!result.Success)
            {
                view.DisplayStatus(false, result.Messages);
            }
            else
            {
                string successMessage = $"Forage {result.Value.Id} created.";
                view.DisplayStatus(true, successMessage);
            }
        }

        //Add forager method
        private void AddForager()
        {
            Forager toAdd = view.MakeForager();
            Result<Forager> result = foragerService.Add(toAdd);
            if (!result.Success)
            {
                view.DisplayStatus(false, result.Messages);
            }
            else
            {
                string successMessage = $"Forager {result.Value.Id} created";
                view.DisplayStatus(true, successMessage);
            }
        }


        //KG/Item report
        private void ReportKgPerItem()
        {
            view.DisplayHeader(MainMenuOption.ReportKgPerItem.ToLabel());
            DateTime date = view.GetForageDate();

            var result = reports.GenerateDailyKGReport(date);
            if (!result.Success)
            {
                view.DisplayStatus(false, result.Messages);
            }
            else
            {
                view.DisplayKgItemReport(result.Value);
            }
        }

        //Value/Category report
        private void ReportValuePerCategory()
        {
            view.DisplayHeader(MainMenuOption.ReportCategoryValue.ToLabel());
            DateTime date = view.GetForageDate();

            var result = reports.GenerateDailyValuePerCategoryReport(date);
            if (!result.Success)
            {
                view.DisplayStatus(false, result.Messages);
            }
            else
            {
                view.DisplayValCategoryReport(result.Value);
            }
        }
        private void AddItem()
        {
            Item item = view.MakeItem();
            Result<Item> result = itemService.Add(item);
            if(!result.Success)
            {
                view.DisplayStatus(false, result.Messages);
            }
            else
            {
                string successMessage = $"Item {result.Value.Id} created.";
                view.DisplayStatus(true, successMessage);
            }
        }

        private void Generate()
        {
            ForageGenerationRequest request = view.GetForageGenerationRequest();
            if(request != null)
            {
                int count = forageService.Generate(request.Start, request.End, request.Count);
                view.DisplayStatus(true, $"{count} forages generated.");
            }
        }

        // support methods
        private Forager GetForager()
        {
            string lastNamePrefix = view.GetForagerNamePrefix();
            List<Forager> foragers = foragerService.FindByLastName(lastNamePrefix);
            return view.ChooseForager(foragers);
        }

        private Item GetItem()
        {
            Category category = view.GetItemCategory();
            List<Item> items = itemService.FindByCategory(category);
            return view.ChooseItem(items);
        }
    }
}
