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

        //set up from file
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
                        if (!string.IsNullOrEmpty(line))//lines may be blank after deleting in-program
                        {
                            string[] fields = line.Split(',');
                            SolarPanel panel = new SolarPanel();
                            panel.Section = fields[0];
                            panel.Row = int.Parse(fields[1]);
                            panel.Column = int.Parse(fields[2]);
                            panel.DateInstalled = DateTime.Parse(fields[3]);
                            panel.Material = Enum.Parse<MaterialType>(fields[4]);
                            panel.IsTracking = bool.Parse(fields[5]);
                            _panelList.Add(panel.GetKey(), panel);
                        }
                        else
                        {
                            continue;
                        }
                        
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
                    sw.WriteLine("Section,Row,Column,DateInstalled,Material,IsTracking");//standard for data is to have these headers
                    foreach (var panel in _panelList) 
                    {
                        sw.WriteLine($"{panel.Value.Section},{panel.Value.Row},{panel.Value.Column}," +
                            $"{panel.Value.DateInstalled},{panel.Value.Material},{panel.Value.IsTracking}");
                    }
                }
            }
        }

        public SolarPanel Create(SolarPanel panel)
        {
            _panelList.Add(panel.GetKey(), panel);
            SavePanels();
            return panel;
        }

        //Delete will remove the Key & Value from the dictionary
        public void Delete(SolarPanel panel)
        {
            _panelList.Remove(panel.GetKey());
            SavePanels();
        }

        public Dictionary<string, SolarPanel> ReadAll()
        {
            return _panelList;
        }


        //Not currently able to change a panel's section/row/column
        public void Update(string section, SolarPanel panel)
        {
            _panelList[section].DateInstalled = panel.DateInstalled;
            _panelList[section].Material = panel.Material;
            _panelList[section].IsTracking = panel.IsTracking;
            SavePanels();

        }
    }
}
