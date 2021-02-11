using CrudWithMongoDBWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Linq;

namespace CrudWithMongoDBWebAPI.Controllers
{
    [Route("api/Employee")]
    [ApiController]
    public class EmpController : Controller
    {
        private readonly IConfiguration _configuration;
        public EmpController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [Route("InsertEmployee")]
        [HttpPost]
        public object Addemployee(Employee objVM)
        {
            try
            {   ///Insert Emoloyeee  
                #region InsertDetails  
                if (objVM.Id == null)
                {
                    string constr = _configuration.GetConnectionString("myDb1");
                    var Client = new MongoClient(constr);
                    var DB = Client.GetDatabase("Employee");
                    var collection = DB.GetCollection<Employee>("EmployeeDetails");
                    collection.InsertOne(objVM);
                    return new Status
                    { Result = "Success", Message = "Employee Details Insert Successfully" };
                }
                #endregion
                ///Update Emoloyeee  
                #region updateDetails  
                else
                {
                    //string constr = ConfigurationManager.AppSettings["connectionString"];
                    string constr = _configuration.GetConnectionString("myDb1");
                    var Client = new MongoClient(constr);
                    var Db = Client.GetDatabase("Employee");
                    var collection = Db.GetCollection<Employee>("EmployeeDetails");

                    var update = collection.FindOneAndUpdateAsync(Builders<Employee>.Filter.Eq("Id", objVM.Id), Builders<Employee>.Update
                        .Set("Name", objVM.Name)
                        .Set("Department", objVM.Department)
                        .Set("Age", objVM.Age)
                        .Set("City", objVM.City)
                        .Set("Technology", objVM.Technology));

                    return new Status
                    { Result = "Success", Message = "Employee Details Update Successfully" };
                }
                #endregion
            }

            catch (Exception ex)
            {
                return new Status
                { Result = "Error", Message = ex.Message.ToString() };
            }

        }

        #region Getemployeedetails  
        [Route("GetAllEmployee")]
        [HttpGet]
        public object GetAllEmployee()
        {
            //string constr = ConfigurationManager.AppSettings["connectionString"];
            string constr = _configuration.GetConnectionString("myDb1");
            var Client = new MongoClient(constr);
            var db = Client.GetDatabase("Employee");
            var collection = db.GetCollection<Employee>("EmployeeDetails")
                .Find(new BsonDocument()).ToList();
            return Json(collection);

        }
        #endregion
        #region EmpdetaisById  
        [Route("GetEmployeeById")]
        [HttpGet]
        public object GetEmployeeById(String id)
        {
            //string constr = ConfigurationManager.AppSettings["connectionString"];
            string constr = _configuration.GetConnectionString("myDb1");
            var Client = new MongoClient(constr);
            var DB = Client.GetDatabase("Employee");
            var collection = DB.GetCollection<Employee>("EmployeeDetails");
            var plant = collection.Find(Builders<Employee>.Filter.Where(s => s.Id == id)).FirstOrDefault();
            return Json(plant);

        }
        #endregion
        #region DeleteEmployee  
        [Route("Delete")]
        [HttpGet]
        public object Delete(string id)
        {
            try
            {
                //string constr = ConfigurationManager.AppSettings["connectionString"];
                string constr = _configuration.GetConnectionString("myDb1");
                var Client = new MongoClient(constr);
                var DB = Client.GetDatabase("Employee");
                var collection = DB.GetCollection<Employee>("EmployeeDetails");
                var DeleteRecored = collection.DeleteOneAsync(
                               Builders<Employee>.Filter.Eq("Id", id));
                return new Status
                { Result = "Success", Message = "Employee Details Delete  Successfully" };

            }
            catch (Exception ex)
            {
                return new Status
                { Result = "Error", Message = ex.Message.ToString() };
            }

        }
        #endregion
    }
}