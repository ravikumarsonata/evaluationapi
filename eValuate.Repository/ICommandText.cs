using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eValuate.Repository
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
        string AddFFQTemplate { get; }
        string AddMOTLayout { get; }
    }
}
