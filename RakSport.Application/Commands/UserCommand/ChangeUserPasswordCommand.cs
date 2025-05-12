using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RakSport.Application.Commands.UserCommand
{
    public class ChangeUserPasswordCommand
    {
        public string? NewPassword { get; set; }

        public string? RepeatPassword { get; set; }
    }
}
