using DevFreela.Services.Payments.API.Models;

namespace DevFreela.Services.Payments.API.Services.Interfaces;

public interface IPaymentService
{
    Task<bool> Process(PaymentInputModel paymentInputModel);
}