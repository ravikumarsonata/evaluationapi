﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eValuate.WebApi.Models
{
    public class Product
    {
        //[Key]
        //[DisplayName("Id")]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Cost { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
