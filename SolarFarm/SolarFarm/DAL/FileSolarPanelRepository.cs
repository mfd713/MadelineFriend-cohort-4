using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolarFarm.Core;
using System.IO;

namespace SolarFarm.DAL
{
    public class FileSolarPanelRepository : ISolarPanelRepository
    {
        private Dictionary<string, SolarPanel> _panelList;
        private string _fileName;

        public FileSolarPanelRepository(string fileName)
        {
            _fileName = fileName;
            _panelList = new Dictionary<string, SolarPanel>();
            LoadPanels();
        }

        private void LoadPanels()
        {
            if (!File.Exists(_fileName))
            {
                File.Create(_fileName).Close();
            }
            else
            {
                using(StreamReader sr = new StreamReader(_fileName))
                {
                    sr.ReadLine(); //first row is just headers
                    for (string line = sr.ReadLine(); line != null; line = sr.ReadLine())
                    {
                        string[] fields = line.Split(',');
                        SolarPanel panel = new SolarPanel();
                        panel.Section = fields[0];
                        panel.Row = int.Parse(fields[1]);
                        panel.Column = int.Parse(fields[2]);
                        panel.Material = Enum.Parse<MaterialType>(fields[3]);
                        panel.IsTracking = bool.Parse(fields[4]);
                        string key = $"{panel.Section}-{panel.Row}-{panel.Column}";
                        _panelList.Add(key, panel);
                    }
                }
            }
        }
        public SolarPanel Create(SolarPanel panel)
        {
            return new SolarPanel();
        }

        public void Delete(string section)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, SolarPanel> ReadAll()
        {
            return _panelList;
        }

        public void Update(string section, SolarPanel panel)
        {
            throw new NotImplementedException();
        }
    }
}
