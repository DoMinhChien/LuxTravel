using System.Threading.Tasks;
using LuxTravel.Api.Core.Commands;
using LuxTravel.Model.Dtos;

namespace LuxTravel.Api.Core.Services
{
    public interface IPaymentService
    {
        Task<PaymentConfirmDto> CreatePaymentIntent(decimal amount);
    }
}
