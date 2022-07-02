﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace DataAccess.DataProvider
{
    public class BaseDAL
    {
        public PRN_Ass2DataProvider dataProvider { get; set; } = null;
        public SqlConnection connection = null;
        //----------------------------------
        public BaseDAL()
        {
            var connectionString = GetConnectionString();
            dataProvider = new PRN_Ass2DataProvider(GetConnectionString());
        }

        public String GetConnectionString()
        {
            String connectionString;
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            connectionString = config["ConnectionString:MyStockDB"];
            return connectionString;
        }


        public void CloseConnection() => dataProvider.CloseConnection(connection);

    }
}
