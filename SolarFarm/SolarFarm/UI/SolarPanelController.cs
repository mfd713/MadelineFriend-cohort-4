using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolarFarm.Core;
using SolarFarm.BLL;

namespace SolarFarm.UI
{
    public class SolarPanelController
    {
        private SolarPanelService _service;

        public SolarPanelController(SolarPanelService service)
        {
            _service = service;
        }

        public void Run()
        {

            int menuChoice;
            bool shouldContinue = true;
            //Display choice menu while user doesn't want to exit
            do
            {
                Console.Clear();
                ConsoleIO.Display("Main Menu\n**********");
                menuChoice = ConsoleIO.PromptInt("0. Exit\n1. Find Panels by Section\n2. Add a Panel\n3. Update a Panel\n4. Remove a Panel\n");

                switch (menuChoice)
                {
                    case 1:
                        string sectionName = ConsoleIO.PromptString("Section name");
                        ReadBySection(sectionName);
                        ConsoleIO.AnyKeyToContinue();
                        break;
                    case 2:
                        Create();
                        ConsoleIO.AnyKeyToContinue();
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    default:
                        shouldContinue = !ShouldLeave();
                        break;
                }
            } while (shouldContinue);

            ConsoleIO.Display("GoodBye!");
        }

        private bool ShouldLeave()
        {
            string input = ConsoleIO.PromptString("Do you want to exit? [y/n]");
            if (input == "y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ReadBySection(string section)
        {
            ListOfPanelsResult result = _service.ReadBySection(section);
            ConsoleIO.Display($"Panels in {section}");

            foreach (var panel in result.Data) 
            {
                ConsoleIO.PrintPanel(panel.Value);
            }

        }

        private void Create()
        {
            SolarPanel panel = new SolarPanel();

            panel.Section = ConsoleIO.PromptString("Section");
            panel.Row = ConsoleIO.PromptInt("Row", 1, 250);
            panel.Column = ConsoleIO.PromptInt("Column", 1, 250);
            panel.DateInstalled = ConsoleIO.PromptDateTime("Installation Date");
            panel.Material = ConsoleIO.PromptMaterialType("Material: \n1. Multicrystalline Silicon\n2. Monocrystalline Silicon" +
                "\n3. Amorphous Silicon\n4. Cadmium Telluride\n5. Copper Indium Gallium Selenide");
            panel.IsTracking = ConsoleIO.PromptIsTracking("Tracking sun [y/n]");

            SolarPanelResult result = _service.Create(panel);

            ConsoleIO.Display(result.Message);
        }
    }
}
