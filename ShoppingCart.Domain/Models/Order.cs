﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShoppingCart.Domain.Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime DateTime { get; set; }

        public string UserEmail { get; set; }

    }
}
