using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ParkingCommonLayer.Services;
using ParkingReposLayer.Services;

namespace ParkingBusinessLayer.Interface
{
    public interface IParkingBL
    {
        bool ParkingDatails(ParkingCL data);

        int DeleteEmployee(ParkingCL Data);

        int UpdateEmployee(ParkingCL data);

    }
}
