using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingCommonLayer.Services
{
    public class ParkingCL
    {
        public string ParkingId { get; set; }

        public int VehicalNo { get; set; }

        public string VehicalBrand { get; set; }

        public string VehicalColor { get; set; }

        public int ChargePerHr { get; set; }

        public DateTime EntryTime { get; set; }

        public DateTime ExitTime { get; set; }

        public int DriverCategory { get; set; }

        public string ParkingType { get; set; }
       
    }
}
