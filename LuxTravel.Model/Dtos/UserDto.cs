using System;
using System.Collections.Generic;
using System.Text;

namespace LuxTravel.Model.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool Male { get; set; }
        public string Gender { get; set; }
    }
}
