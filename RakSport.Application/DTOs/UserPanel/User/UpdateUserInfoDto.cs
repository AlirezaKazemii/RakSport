using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RakSport.Application.DTOs.UserPanel.User
{
    public class UpdateUserInfoDto
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public bool? Gender { get; set; }
    }
}
