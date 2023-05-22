using DevFreela.Payments.API.Models;

namespace DevFreela.Payments.API.Services.Interfaces;

public interface IPaymentService
{
    Task<bool> Process(PaymentInputModel paymentInputModel);
}