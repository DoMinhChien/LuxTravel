using System;
using System.Collections.Generic;
using System.Text;

namespace LuxTravel.Models
{
    public class LuxTravelDBConfig
    {
        private string connectionString;
        public LuxTravelDBConfig(string _connectionString)
        {
            connectionString = _connectionString;
        }
    }
}
