﻿using ETicaretAPI.Domain.Entites.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Domain.Entites
{
    public class Order : BaseEntity
    {
        public Guid customerId {  get; set; }

        public string Description { get; set; }

        public string Address {  get; set; }

        public ICollection<Product> Products { get; set; }

        public Customer Customer { get; set; }
    }
}
