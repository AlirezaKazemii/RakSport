using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RakSport.Application.Commands.UserCommand
{
    public class UpdateUserInfoCommand
    {
        public string? FullName { get; set; }

        public string? Email { get; set; }

        public string? ShabaNumber { get; set; }

        public bool? Gender { get; set; }

        public void Validate()
        {
            var validator = new UpdateUserCommandValidator();

            var validationResult = validator.Validate(this);

            if (!validationResult.IsValid)
            {
                var error = validationResult.Errors.FirstOrDefault();
            }
        }
    }
}
