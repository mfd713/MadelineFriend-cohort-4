using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolarFarm.Core;

namespace SolarFarm.BLL
{ 
    public class SolarPanelService
    {
        private ISolarPanelRepository _repo;

        public SolarPanelService(ISolarPanelRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Helper function to check that a given panel follows expected business logic.
        /// Doesn't check datatypes; those are validated at user input stage
        /// </summary>
        /// <param name="panel">Solar panel that the service is called to act on</param>
        /// <returns>SolarPanelResult that contains the given panel, tells whether action was unsuccessful, and why</returns>
        private SolarPanelResult IsValid(SolarPanel panel)
        {
            Dictionary<string, SolarPanel> panelsList = _repo.ReadAll();
            //Section can't be empty
            if (string.IsNullOrEmpty(panel.Section))
            {
                return new SolarPanelResult { Success = false, Message = "Section cannot be null or empty", Data = panel };
            }
            // 0 < Row <= 250
            if(panel.Row < 0|| panel.Row > 250)
            {
                return new SolarPanelResult { Success = false, Message = "Row must be an int between 0 and 250", Data = panel };

            }
            // 0 < Column <= 250
            if (panel.Column < 0 || panel.Column > 250)
            {
                return new SolarPanelResult { Success = false, Message = "Column must be an int between 0 and 250", Data = panel };
            }
            //DateInstalled must be past
            if (panel.DateInstalled > DateTime.Now)
            {
                return new SolarPanelResult { Success = false, Message = "Date Installed cannot be in the future", Data = panel };
            }
            //IsTracking can't be empty. This section probably won't ever complete because boolean will be validated at input
            if (panel.IsTracking != true)
            {
                if(panel.IsTracking != false)
                {
                    return new SolarPanelResult { Success = false, Message = "IsTracking must be true or false", Data = panel };
                }
            }
            //Section + Row + Column must be unique
            if (panelsList.ContainsKey(panel.GetKey()))
            {
                return new SolarPanelResult { Success = false, Message = "Section key must be uniuqe", Data = panel };
            }

            return new SolarPanelResult { Success = true, Message = "Success.", Data = panel };
        }


        /// <summary>
        /// Calls the repository Create method if the given panel is valid
        /// </summary>
        /// <param name="panel">Solar panel the service is acting on</param>
        /// <returns>SolarPanelResult that contains the given panel, tells if creation was unsuccessful, and why</returns>
        public SolarPanelResult Create(SolarPanel panel)
        {
            SolarPanelResult result = IsValid(panel);
            if (!result.Success)
            {
                return result;
            }

            _repo.Create(panel);
            result.Message += $" {panel.GetKey()} added.";
            return result;
        }

        /// <summary>
        /// Finds all panels in a given section and returns them
        /// </summary>
        /// <param name="section">The section of desired panels</param>
        /// <returns>ListOfPanelsResult indicating success, Data is a list of panels if found and null if not</returns>
        public ListOfPanelsResult ReadBySection(string section)
        {
            Dictionary<string,SolarPanel> panelsList = _repo.ReadAll();
            Dictionary<string, SolarPanel> resultList = new Dictionary<string, SolarPanel>();

            foreach (var panel in panelsList)
            {
                if (panel.Value.Section == section)
                {
                    resultList.Add(panel.Value.GetKey(),panel.Value);
                }
            }

            if (resultList.Count == 0)
            {
                return new ListOfPanelsResult { Data = null, Success = false, Message = "No panels found in that section" };
            }
            else
            {
                return new ListOfPanelsResult { Data = resultList, Success = true, Message = "Success" };
            }
        }

        /// <summary>
        /// Searches the list of solar panels for a single panel
        /// </summary>
        /// <param name="section">Section of target panel</param>
        /// <param name="row">Row of target panel</param>
        /// <param name="column">Column of target panel</param>
        /// <returns>SolarPanelResult indicating success, Data is the panel if found and null if not</returns>
        public SolarPanelResult ReadSinglePanel(string section, int row, int column)
        {
            Dictionary<string, SolarPanel> panelsList = _repo.ReadAll();
            if (!panelsList.ContainsKey($"{section}-{row}-{column}"))
            {
                return new SolarPanelResult { Data = null, Success = false, Message = "Panel not found" };
            }

            return new SolarPanelResult { Data = panelsList[$"{section}-{row}-{column}"], Success = true, Message = "Success" };
        }
        /// <summary>
        /// Updates a panel with a certain Section-row-column identifier. Cannot update section/row/column for now
        /// </summary>
        /// <param name="panel">The panel containing the new information</param>
        /// <returns>SolarPanelResult indicating success, Data is the new panel if changes are valid null if not</returns>
        public SolarPanelResult Update(SolarPanel panel)
        {

            //DateInstalled must be past
            if (panel.DateInstalled > DateTime.Now)
            {
                return new SolarPanelResult { Success = false, Message = "Date Installed cannot be in the future", Data = null };
            }
            //IsTracking can't be empty
            if (panel.IsTracking != true)
            {
                if (panel.IsTracking != false)
                {
                    return new SolarPanelResult { Success = false, Message = "IsTracking must be true or false", Data = null };
                }
            }
           //Doesn't check material type because that will be validated at input point (chosen from list of valid material types)

                _repo.Update(panel.GetKey(), panel);
                return new SolarPanelResult { Success = true, Message = "Success", Data = panel };
        }

        /// <summary>
        /// Deletes panel by passing it through to the DAL level. Does not contain validation because that will be handled
        /// by viewing the panel first, caught by ReadBySection/ReadSinglePanel first.
        /// </summary>
        /// <param name="panel">The solar panel to be deleted</param>
        /// <returns>SolarPanelResult indicating success and containing the deleted solar panel in Data</returns>
        public SolarPanelResult Delete(SolarPanel panel)
        {
            SolarPanelResult result = new SolarPanelResult { Data = panel, Success = true, Message = "Panel deleted" };
            _repo.Delete(panel);

            return result;
        }
    }
}
