using System;
using SolarFarm.UI;
using SolarFarm.BLL;
using SolarFarm.Core;
using SolarFarm.DAL;


namespace SolarFarm
{
    class Program
    {
        static void Main(string[] args)
        {
            ISolarPanelRepository repo = new FileSolarPanelRepository("FullTestData.csv");
            SolarPanelService service = new SolarPanelService(repo);
            SolarPanelController solarPanelController = new SolarPanelController(service);

            solarPanelController.Run();
        }
    }
}
