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
                        _panelList.Add(GenerateKey(panel), panel);
                    }
                }
            }
        }

        private void SavePanels()
        {
            if (File.Exists(_fileName))
            {
                using(StreamWriter sw = new StreamWriter(_fileName))
                {
                    foreach (var panel in _panelList) 
                    {
                        sw.WriteLine($"{panel.Value.Section},{panel.Value.Row},{panel.Value.Column}," +
                            $"{panel.Value.Material},{panel.Value.IsTracking}");
                    }
                }
            }
        }

        private string GenerateKey(SolarPanel panel)
        {
            return $"{panel.Section}-{panel.Row}-{panel.Column}";
        }
        public SolarPanel Create(SolarPanel panel)
        {
            _panelList.Add($"{panel.Section}-{panel.Row}-{panel.Column}", panel);
            SavePanels();
            return panel;
        }

        public void Delete(SolarPanel panel)
        {
            _panelList.Remove(GenerateKey(panel));
            SavePanels();
        }

        public Dictionary<string, SolarPanel> ReadAll()
        {
            return _panelList;
        }

        public void Update(string section, SolarPanel panel)
        {
            _panelList[section].Material = panel.Material;
            _panelList[section].IsTracking = panel.IsTracking;

        }
    }
}
