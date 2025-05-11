using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RakSport.Application.Interfaces
{
    public interface IJwtService
    {
        public Task<string> GenerateToken(Guid userid);
    }
}
