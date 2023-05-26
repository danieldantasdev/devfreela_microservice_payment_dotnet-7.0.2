using DevFreela.Services.Payments.API.Models;
using DevFreela.Services.Payments.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.Services.Payments.API.Controllers;

[Route("api/payments")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PaymentInputModel paymentInputModel)
    {
        var result = await _paymentService.Process(paymentInputModel);

        if (!result)
        {
            return BadRequest();
        }

        return NoContent();
    }
}