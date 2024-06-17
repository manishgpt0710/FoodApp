﻿namespace FoodApp.Services.AuthAPI.Models.Dto
{
    public class RegistrationRequestDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public RoleType Role { get; set; }
    }

    public enum RoleType
    {
        ADMIN,
        CUSTOMER
    }
}
