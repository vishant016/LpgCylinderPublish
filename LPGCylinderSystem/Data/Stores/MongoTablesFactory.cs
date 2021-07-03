using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LPGCylinderSystem.Data.Stores
{
    public  class MongoTablesFactory
    {
        private readonly string _connectionString;
        public const string TABLE_USERS = "Users";
        public const string TABLE_ROLES = "Roles";
        public const string TABLE_AGENCY = "Agencies";
        public const string TABLE_CYLINDER = "Cylinders";
        public const string TABLE_ADMIN = "Admins";
        public const string TABLE_NETBANKING = "Netbankings";
        public const string TABLE_CARD = "Cards";
        public const string TABLE_COMPLAINT = "Complaints";


        public MongoTablesFactory(IConfiguration configuration)
        {
            _connectionString = configuration["MongodbConnectionString"];
        }

        public MongoTablesFactory()
        {
        }

        public IMongoCollection<T> GetCollection<T>(string tableName)
        {
            MongoClient client = new MongoClient(_connectionString);
            var db = client.GetDatabase("IdentityDb");
            return db.GetCollection<T>(tableName);
        }







    }
}
