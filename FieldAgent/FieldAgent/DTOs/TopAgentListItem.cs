﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldAgent.DTOs
{
    public class TopAgentListItem
    {
        public string NameLastFirst { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int CompletedMissionCount { get; set; }
    }
}
