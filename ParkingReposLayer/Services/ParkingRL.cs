using ParkingReposLayer.Interface;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading.Tasks;
using ParkingCommonLayer.Services;

namespace ParkingReposLayer.Services
{
    public class ParkingRL : IParkingRL
    {
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-MQSNJSU;Initial Catalog=MyDatabase;Integrated Security=True");

        public static readonly int CHARGE_PER_HR = 2;
        public bool ParkingDatails(ParkingCL data)
        {
             try
            {

                SqlCommand command = StoreProcedureConnection("spAddParkingDetail", connection);

                command.Parameters.AddWithValue("VehicalNo", data.VehicalNo);
                command.Parameters.AddWithValue("VehicalBrand", data.VehicalBrand);
                command.Parameters.AddWithValue("VehicalColor", data.VehicalColor);
                if (data.ExitTime > data.EntryTime && data.ParkingType != "Own")
                {
                    TimeSpan time = data.ExitTime - data.EntryTime;
                    data.ChargePerHr = Convert.ToInt32(time) * CHARGE_PER_HR;
                }
                command.Parameters.AddWithValue("ChargePerHr", data.ChargePerHr);
                command.Parameters.AddWithValue("EntryTime", data.EntryTime);
                command.Parameters.AddWithValue("DriverCategory", data.DriverCategory);
                command.Parameters.AddWithValue("ParkingType", data.ParkingType);
                if (data.ExitTime < data.EntryTime)
                {
                    data.ExitTime = data.EntryTime;
                }
                command.Parameters.AddWithValue("ExitTime", data.ExitTime);

                connection.Open();
                int Response = command.ExecuteNonQuery();
                if (Response != 0)
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
            finally
            {
                connection.Close();
            }
        }
        public int DeleteEmployee(ParkingID Data)
        {

            try
            {
                SqlCommand command = StoreProcedureConnection("spDeleteParkingRcords", connection);
                command.Parameters.AddWithValue("@ParkingId", Data.ParkingId);
                connection.Open();
                int Response = command.ExecuteNonQuery();
                connection.Close();
                if (Response != 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public SqlCommand StoreProcedureConnection(string Procedurename, SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand(Procedurename, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                return command;
            }
        }
    }
}
