﻿
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DbLevel.Models
{
    public class User : IdentityUser
    {
        public Guid? CartId { get; set; }
        public string? Surname { get; set; }
        public string? Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public IEnumerable<Product>? FavoriteList { get; set; } 

    }
}