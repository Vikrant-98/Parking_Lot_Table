using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingCommonLayer.Services
{
    public class ParkingCL
    {
        public int ParkingId { get; set; }

        public int VehicalNo { get; set; }

        public string VehicalBrand { get; set; }

        public string Password { get; set; }

        public string VehicalColor { get; set; }

        public int ChargePerHr { get; set; }

        public DateTime EntryTime { get; set; }

        public DateTime ExitTime { get; set; }

        public int DriverCategory { get; set; }

        public string ParkingType { get; set; }
       
    }
    public class EncryptedPassword
    {
        public static string EncodePasswordToBase64(string Password)
        {
            try
            {
                byte[] encData_byte = new byte[Password.Length];
                encData_byte = Encoding.UTF8.GetBytes(Password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
    }
}
