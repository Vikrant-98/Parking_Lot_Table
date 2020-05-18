using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ParkingCommonLayer.Services;

namespace ParkingReposLayer.Interface
{
    public interface IParkingRL
    {
        bool ParkingDatails(ParkingCL data);

        int DeleteEmployee(ParkingID Data);
    }
}
