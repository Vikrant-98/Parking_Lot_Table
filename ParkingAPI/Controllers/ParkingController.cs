using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingCommonLayer.Services;
using ParkingBusinessLayer.Interface;
using Microsoft.Extensions.Configuration;
using ParkingReposLayer.Services;

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
        
        [Route("")]
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
        public IActionResult DeleteEmployee(ParkingID Data)
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
    }
}