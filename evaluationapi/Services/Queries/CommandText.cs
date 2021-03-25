using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eValuate.WebApi.Services.Interfaces;

namespace eValuate.WebApi.Services.Queries
{
    public class CommandText: ICommandText
    {
        public string GetProducts => "Select * From Products";
        public string GetProductById => "Select * From Products Where Id= @Id";
        public string AddProduct => "Insert Into  Products (Name, Cost, CreatedDate) Values (@Name, @Cost, @CreatedDate)";
        public string UpdateProduct => "Update Products set Name = @Name, Cost = @Cost, CreatedDate = GETDATE() Where Id =@Id";
        public string RemoveProduct => "Delete From Products Where Id= @Id";

        // GBW Database Server
        public string GetUsers => "Select * From Test_User";
    }
}
