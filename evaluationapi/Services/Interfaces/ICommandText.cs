using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eValuate.WebApi.Services.Interfaces
{
    public interface ICommandText
    {
        string GetProducts { get; }
        string GetProductById { get; }
        string AddProduct { get; }
        string UpdateProduct { get; }
        string RemoveProduct { get; }

        // GBW Database
        string GetUsers { get; }
    }
}
