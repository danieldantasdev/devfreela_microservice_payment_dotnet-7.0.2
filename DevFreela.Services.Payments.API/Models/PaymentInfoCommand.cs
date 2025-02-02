namespace DevFreela.Services.Payments.API.Models;

public class PaymentInputModel
{
    public int idProject { get; set; }
    public string CreditCardNumber { get; set; }
    public string Cvv { get; set; }
    public string ExpiresAt { get; set; }
    public string FullName { get; set; }
    public string Amount { get; set; }
}