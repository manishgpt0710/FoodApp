﻿namespace FoodApp.WebApp.Models.AuthModelDto
{
    public class LoginResponseDto
    {
        public UserDto User { get; set; }
        public string Token { get; set; }
    }
}