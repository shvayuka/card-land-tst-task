using CardLand.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLand.Models
{
    public class Office
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string CityCode { get; set; }
        public string? Uuid { get; set; }
        public OfficeType? Type { get; set; }
        public string CountryCode { get; set; }
        public Coordinates Coordinates { get; set; }
        public string? AddressRegion { get; set; }
        public string? AddressCity { get; set; }
        public string? AddressStreet { get; set; }
        public string? AddressHouseNumber { get; set; }
        public int? AddressApartment { get; set; }
        public string WorkTime { get; set; }
        public ICollection<Phone> Phones { get; set; } = new List<Phone>();

        public Office() 
        { 
        
        }
    }
}
