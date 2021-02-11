using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using MongoDB.Bson;
using MongoDB.Driver;

namespace InsertOperation
{
    class Program
    {
        static void Main(string[] args)
        {
            var Mongodbconnection = "mongodb://localhost";
            var Client = new MongoClient(Mongodbconnection);
            var DB = Client.GetDatabase("Employee");
            var collection = DB.GetCollection<BsonDocument>("EmployeeDetails ");
            var Emp = new BsonDocument
            {
                {"Name","Sanwar"},
                {"City","Jaipur"},
                {"Age","23"},
                {"Department","Software Development"},
                {"Technology","Dot Net"}
            };
            collection.InsertOneAsync(Emp);
            Console.WriteLine("Press Enter");
            Console.ReadLine();
        }
    }
}
