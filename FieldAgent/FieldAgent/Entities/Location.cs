using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldAgent.Entities
{
    public class Location
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string CountryCode { get; set; }

        public int AgencyId { get; set; }
        public Agency Agency { get; set; }
    }
}
