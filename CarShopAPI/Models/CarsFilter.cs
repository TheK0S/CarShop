﻿using System.ComponentModel.DataAnnotations;

namespace CarShopAPI.Models
{
    public class CarsFilter
    {
        public uint Price { get; set; } = 0;
        public bool IsFavourite { get; set; } = false;
        public int CategoryId { get; set; }
    }
}
