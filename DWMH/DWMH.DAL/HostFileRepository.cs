﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DWMH.Core;
using DWMH.Core.Repos;

namespace DWMH.DAL
{
    public class HostFileRepository : IHostRepository
    {
        public List<Host> ReadAll()
        {
            throw new NotImplementedException();
        }

        public Host ReadByEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}