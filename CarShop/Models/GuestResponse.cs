﻿using System.ComponentModel.DataAnnotations;

namespace CarShop.Models
{
    public class GuestResponse
    {
        [Required(ErrorMessage ="Пожалуйста, введите свое имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите email")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage ="Вы ввели некорректный email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите телефон")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Пожалуйста, укажите, примите ли вы участие в вечеринке")]
        public bool? WillAttend { get; set; }
    }
}
