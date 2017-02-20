using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SearchEngine.Models
{
    public class RecordsRepository
    {
        private EntityDatabaseContext context;

        public RecordsRepository()
        {
            string connectionString =
                "Data Source=PC;Initial Catalog=SearchEngineDB;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            context = new EntityDatabaseContext(connectionString);
        }

        public List<Record> GetAllRecords()
        {
            return context.Records.ToList();
        }
    }
}