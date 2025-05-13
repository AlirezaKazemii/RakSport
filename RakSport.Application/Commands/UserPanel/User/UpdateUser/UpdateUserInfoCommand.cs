using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RakSport.Application.Commands.UserPanel.User.UpdateUser
{
    public class UpdateUserInfoCommand
    {
        public string? FullName { get; set; }

        public string? Email { get; set; }

        public string? ShabaNumber { get; set; }

        public bool? Gender { get; set; }
    }
}
