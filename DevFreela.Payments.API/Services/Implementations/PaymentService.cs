using DevFreela.Payments.API.Models;
using DevFreela.Payments.API.Services.Interfaces;

namespace DevFreela.Payments.API.Services.Implementations;

public class PaymentService : IPaymentService
{
    public Task<bool> Process(PaymentInputModel paymentInputModel)
    {
        return Task.FromResult(true);
    }
}