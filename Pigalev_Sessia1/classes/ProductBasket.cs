﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pigalev_Sessia1
{
    public class ProductBasket
    {
        public Product product { get; set; }
        public int count { get; set; }
        public string textDecoration
        {
            get
            {
                if (product.ProductDiscountAmount != 0)
                {
                    return "Strikethrough";
                }
                else
                {
                    return "Baseline";
                }
            }
        }
    }
}
