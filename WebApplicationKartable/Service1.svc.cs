using System;
using System.Collections.Generic;
using Cartable;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Data;
using System.Configuration;

namespace WebApplicationKartable
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        private String strConnString = ConfigurationManager.ConnectionStrings["FarsheBoom"].ConnectionString;
        private LoginInfo ubuzhi = new LoginInfo();
        private List<Customer> lst_customer = new List<Customer>();
        public List<Customer> GetAllCustomer()
        {
            DataTable dt = new DataTable(); Search obj = new Search(strConnString);
            dt = obj.Get_Data("SELECT srl, full_name FROM dbo.bas_supcust");
            foreach(DataRow row in dt.Rows)
            {
                lst_customer.Add(new Customer { ID = Convert.ToInt32(row["srl"]), FullName = row["full_name"].ToString() });
            }
            return lst_customer;
        }
    }
}
