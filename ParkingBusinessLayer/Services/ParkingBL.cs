using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ParkingReposLayer.Interface;
using ParkingReposLayer.Services;
using ParkingCommonLayer.Services;
using ParkingBusinessLayer.Interface;

namespace ParkingBusinessLayer.Services
{
    public class ParkingBL : IParkingBL
    {
        private IParkingRL Parking;
        public ParkingBL(IParkingRL data)
        {
            Parking = data;
        }
        public bool ParkingDatails(ParkingCL data)
        {
            try
            {
                var Result = Parking.ParkingDatails(data);
                if (!Result.Equals(null))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public int DeleteEmployee(ParkingCL Data)
        {
            try
            {
                int Result = Parking.DeleteEmployee(Data);
                if (Result == 0)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public int UpdateEmployee(ParkingCL data)
        {
            try
            {
                int Result =  Parking.UpdateEmployee(data);
                
                if (Result == 0)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
