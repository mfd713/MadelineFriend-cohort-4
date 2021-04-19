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
                        ReadBySection();
                        ConsoleIO.AnyKeyToContinue();
                        break;
                    case 2:
                        Create();
                        ConsoleIO.AnyKeyToContinue();
                        break;
                    case 3:
                        Update();
                        ConsoleIO.AnyKeyToContinue();
                        break;
                    case 4:
                        Remove();
                        ConsoleIO.AnyKeyToContinue();
                        break;
                    default:
                        shouldContinue = !ShouldLeave();
                        break;
                }
            } while (shouldContinue);

            ConsoleIO.Display("Good Bye!");
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

        private void ReadBySection()
        {
            string section = ConsoleIO.PromptString("Section name");
            ListOfPanelsResult result = _service.ReadBySection(section);
            ConsoleIO.Display($"Panels in {section}");

            if (result.Success)
            {
                foreach (var panel in result.Data)
                {
                    ConsoleIO.PrintPanel(panel.Value);
                }
            }
            else
            {
                ConsoleIO.Display(result.Message);
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

        private void Update()
        {
            //get the panel to change and make sure it exists
            string section = ConsoleIO.PromptString("Section name");
            int row = ConsoleIO.PromptInt("Row [1,250]", 1, 250);
            int column = ConsoleIO.PromptInt("Column [1,250]", 1, 250);

            SolarPanel toAdd = new SolarPanel();
            
            SolarPanelResult result = _service.ReadSinglePanel(section, row, column);
            if (result.Success == false)
            {
                ConsoleIO.Display(result.Message);
                return;
            }
            else
            {
                //can't currently change the section/row/column, so these stay the same
                toAdd.Section = result.Data.Section;
                toAdd.Row = result.Data.Row;
                toAdd.Column = result.Data.Column;

                ConsoleIO.Display($"Editing {result.Data.GetKey()}\nPress [Enter] to keep original value. Press any other key to begin editing");

                //get new material
                toAdd.Material = ConsoleIO.PromptMaterialUpdate(result.Data);

                //get new DateInstalled
                toAdd.DateInstalled = ConsoleIO.PromptDateTimeUpdate(result.Data);

                //get new IsTracking
                toAdd.IsTracking = ConsoleIO.PromptTrackingUpdate(result.Data);
            }

            result = _service.Update(toAdd);

            ConsoleIO.Display($"{result.Message} {toAdd.GetKey()} was updated");
        }

        private void Remove()
        {
            //get the panel to change and make sure it exists
            string section = ConsoleIO.PromptString("Section name");
            int row = ConsoleIO.PromptInt("Row [1,250]", 1, 250);
            int column = ConsoleIO.PromptInt("Column [1,250]", 1, 250);

            SolarPanel toRemove = new SolarPanel();
            

            SolarPanelResult result1 = _service.ReadSinglePanel(section, row, column);
            if (result1.Success == false)
            {
                ConsoleIO.Display(result1.Message);
                return;
            }
            else
            {
                SolarPanelResult result2 = _service.Delete(result1.Data);
                ConsoleIO.Display(result2.Message);
            }
        }
    }
}
