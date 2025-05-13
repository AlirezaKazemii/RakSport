using RakSport.Application.Services.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RakSport.Application.Interfaces.Auth
{
    public interface IMeliPayamak
    {
        Task<RestResponse> SendAsync(string to, string from, string message, bool isflash);
        Task<RestResponse> SendByBaseNumberAsync(string text, string to, int bodyId);
        Task<RestResponse> GetDeliveryAsync(long recid);
        Task<RestResponse> GetMessagesAsync(int location, string from, string index, int count);
        Task<RestResponse> GetCreditAsync();
        Task<RestResponse> GetBasePriceAsync();
        Task<RestResponse> GetUserNumbersAsync();
    }
}
