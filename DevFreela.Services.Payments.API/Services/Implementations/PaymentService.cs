using DevFreela.Services.Payments.API.Models;
using DevFreela.Services.Payments.API.Services.Interfaces;

namespace DevFreela.Services.Payments.API.Services.Implementations;

public class PaymentService : IPaymentService
{
    public Task<bool> Process(PaymentInputModel paymentInputModel)
    {
        return Task.FromResult(true);
    }
}