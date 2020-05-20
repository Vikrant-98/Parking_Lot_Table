using ParkingReposLayer.Interface;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkingCommonLayer.Services;

namespace ParkingReposLayer.Services
{
    public class ParkingRL : IParkingRL
    {
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-MQSNJSU;Initial Catalog=MyDatabase;Integrated Security=True");

        public static readonly int CHARGE_PER_HR = 10;
        public bool ParkingDatails(ParkingCL data)
        {
            try
            {

                SqlCommand com = StoreProcedureConnection("spAddParkingDetail", connection);
                string Password = EncryptedPassword.EncodePasswordToBase64(data.Password);
                com.Parameters.AddWithValue("VehicalNo", data.VehicalNo);
                com.Parameters.AddWithValue("VehicalBrand", data.VehicalBrand);
                com.Parameters.AddWithValue("VehicalColor", data.VehicalColor);
                if (data.ExitTime > data.EntryTime && data.ParkingType != "Own")
                {
                    data.ChargePerHr = CHARGE_PER_HR;
                }
                com.Parameters.AddWithValue("ChargePerHr", data.ChargePerHr);
                com.Parameters.AddWithValue("EntryTime", data.EntryTime);
                com.Parameters.AddWithValue("DriverCategory", data.DriverCategory);
                com.Parameters.AddWithValue("ParkingType", data.ParkingType);
                if (data.ExitTime < data.EntryTime)
                {
                    data.ExitTime = data.EntryTime;
                }
                com.Parameters.AddWithValue("ExitTime", data.ExitTime);
                com.Parameters.AddWithValue("@Password", Password);

                connection.Open();
                int Response = com.ExecuteNonQuery();
                connection.Close();
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
        public bool ParkingLogin(ParkingCL data)
        {
            try
            {

                SqlCommand command = StoreProcedureConnection("spParkingLogin", connection);
                string Password = EncryptedPassword.EncodePasswordToBase64(data.Password);
                command.Parameters.AddWithValue("@ParkingId", data.ParkingId);
                command.Parameters.AddWithValue("@Password", Password);
                connection.Open();
                int Response = command.ExecuteNonQuery();
                connection.Close();
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
        }
        public int DeleteEmployee(ParkingCL data)
        {

            try
            {
                SqlCommand com = StoreProcedureConnection("spDeleteParkingRcords", connection);
                com.Parameters.AddWithValue("@ParkingId", data.ParkingId);
                connection.Open();
                int Response = com.ExecuteNonQuery();
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
            finally
            {
                connection.Close();
            }
        }
        public int UpdateEmployee(ParkingCL data)
        {
            try
            {

                SqlCommand com = StoreProcedureConnection("spUpdateParkingDetails", connection);
                string Password = EncryptedPassword.EncodePasswordToBase64(data.Password);
                com.Parameters.AddWithValue("@ParkingId", data.ParkingId);
                com.Parameters.AddWithValue("VehicalNo", data.VehicalNo);
                com.Parameters.AddWithValue("VehicalBrand", data.VehicalBrand);
                com.Parameters.AddWithValue("VehicalColor", data.VehicalColor);
                if (data.ExitTime > data.EntryTime && data.ParkingType != "Own")
                {
                    data.ChargePerHr = CHARGE_PER_HR;
                }
                com.Parameters.AddWithValue("ChargePerHr", data.ChargePerHr);
                com.Parameters.AddWithValue("EntryTime", data.EntryTime);
                com.Parameters.AddWithValue("DriverCategory", data.DriverCategory);
                com.Parameters.AddWithValue("ParkingType", data.ParkingType);
                if (data.ExitTime < data.EntryTime)
                {
                    data.ExitTime = data.EntryTime;
                }
                com.Parameters.AddWithValue("ExitTime", data.ExitTime);
                com.Parameters.AddWithValue("@Password", Password);

                connection.Open();
                int Response = com.ExecuteNonQuery();
                connection.Close();
                if (Response == 0)
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
            finally
            {
                connection.Close();
            }
        }
        public IEnumerable<ParkingCL> GetAllParkingDetails()
        {
            try
            {
                List<ParkingCL> listParking = new List<ParkingCL>();
                SqlCommand com = StoreProcedureConnection("spAllParkingDetails", connection);
                connection.Open();
                SqlDataReader Response = com.ExecuteReader();
                while (Response.Read())
                {
                    ParkingCL park = new ParkingCL();
                    park.ParkingId = Convert.ToInt32(Response["ParkingSlotNo"]);
                    park.VehicalNo = Convert.ToInt32(Response["VehicalNo"]);
                    park.VehicalBrand = Response["VehicalBrand"].ToString();
                    park.VehicalColor = Response["VehicalColor"].ToString();
                    park.ChargePerHr = Convert.ToInt32(Response["ChargePerHr"]);
                    park.EntryTime = DateTime.Parse(Response["EntryTime"].ToString());
                    park.DriverCategory = Convert.ToInt32(Response["DriverCategory"]);
                    park.ParkingType = Response["ParkingType"].ToString();
                    park.ExitTime = DateTime.Parse(Response["ExitTime"].ToString());
                    park.Password = Response["Password"].ToString();
                    listParking.Add(park);
                }
                connection.Close();
                return listParking;
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
        public ParkingCL GetspecifiParkingDetails(int ID)
        {
            try
            {
                ParkingCL park = new ParkingCL();
                SqlCommand com = StoreProcedureConnection("spSpecificParkingDetails", connection);
                com.Parameters.Add("@ParkingId", SqlDbType.Int).Value = ID;
                connection.Open();
                SqlDataReader Response = com.ExecuteReader();
                while (Response.Read())
                {
                    park.ParkingId = Convert.ToInt32(Response["ParkingSlotNo"]);
                    park.VehicalNo = Convert.ToInt32(Response["VehicalNo"]);
                    park.VehicalBrand = Response["VehicalBrand"].ToString();
                    park.VehicalColor = Response["VehicalColor"].ToString();
                    park.ChargePerHr = Convert.ToInt32(Response["ChargePerHr"]);
                    park.EntryTime = DateTime.Parse(Response["EntryTime"].ToString());
                    park.DriverCategory = Convert.ToInt32(Response["DriverCategory"]);
                    park.ParkingType = Response["ParkingType"].ToString();
                    park.ExitTime = DateTime.Parse(Response["ExitTime"].ToString());
                    park.Password= Response["Password"].ToString();
                }
                connection.Close();
                return park;
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
        public SqlCommand StoreProcedureConnection(string Procedurename, SqlConnection connection)
        {
            using (SqlCommand com = new SqlCommand(Procedurename, connection))
            {
                com.CommandType = CommandType.StoredProcedure;
                return com;
            }
        }
    }
}
