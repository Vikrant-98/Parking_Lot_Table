using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ParkingCommonLayer.Services;
using ParkingBusinessLayer.Interface;
using Microsoft.Extensions.Configuration;

namespace ParkingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingController : ControllerBase
    {
        private readonly IConfiguration configuration;
        IParkingBL BusinessLayer;

        public ParkingController(IParkingBL BusinessDependencyInjection, IConfiguration configuration)
        {
            BusinessLayer = BusinessDependencyInjection;
            this.configuration = configuration;
        }
        
        [Route("adddetails")]
        [HttpPost]
        public IActionResult ParkingLOTDetails([FromBody]ParkingCL Info)
        {
            try
            {
                bool data = BusinessLayer.ParkingDatails(Info);
                if (!data.Equals(null))
                {
                    var status = true;
                    var Message = "Parking Details Entered Succesfully";
                    return this.Ok(new { status, Message, data });
                }
                else
                {
                    var status = false;
                    var Message = "Parking Details Entered Failed";
                    return this.BadRequest(new { status, Message });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }
        [HttpDelete]
        [Route("delete")]
        public IActionResult DeleteEmployee(ParkingCL Data)
        {
            try
            {
                int result = BusinessLayer.DeleteEmployee(Data);
                
                if (result != 0)
                {
                    var Status = "True";
                    var Message = "Parking Data deleted Sucessfully";
                    return this.Ok(new { Status, Message, Data });
                }
                else                                           
                {
                    var Status = "False";
                    var Message = "Parking Data is not deleted Sucessfully";
                    return this.BadRequest(new { Status, Message, Data });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpPost]
        [Route("login")]
        public IActionResult ParkingLogin([FromBody] ParkingCL Info)
        {
            try
            {
                var Result = BusinessLayer.ParkingLogin(Info);
                
                if (!Result.Equals(null))
                {
                    var status = "True";
                    var Message = "Login Successful";
                    return Ok(new { status, Message, Result });
                }
                else                                        
                {
                    var status = "False";
                    var Message = "Invaid Username Or Password";
                    return BadRequest(new { status, Message, Result });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpPatch]
        [Route("update")]
        public IActionResult UpdateEmployee([FromBody] ParkingCL data)
        {
            try
            {
                int Result = BusinessLayer.UpdateEmployee(data);
                if (Result == 0)
                {
                    var Status = "False";
                    var Message = "wrong input";
                    return this.BadRequest(new { Status, Message, data });
                }
                else                                             
                {
                    var Status = "True";
                    var Message = "Parking Data Updated Sucessfully";
                    return this.Ok(new { Status, Message, data });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpGet]
        public IEnumerable<ParkingCL> GetAllParkingDetails()
        {
            try
            {
                return BusinessLayer.GetAllParkingDetails();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpGet("{ID}")]
        public ParkingCL GetAllParkingDetails(int ID)
        {
            try
            {
                return BusinessLayer.GetspecifiParkingDetails(ID);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}