using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarFarm.Core
{
    interface ISolarPanelRepository
    {
        //create
        public SolarPanel Create(SolarPanel panel);
        //read all
        public Dictionary<string, SolarPanel> ReadAll();
        //update
        public void Update(string section, SolarPanel panel);
        //delete
        public void Delete(string section);
    }
}
