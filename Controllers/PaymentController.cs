using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Payment_Calculator.Enums;
using Payment_Calculator.Interfaces;
using Payment_Calculator.Interfaces.IServices;
using Payment_Calculator.Models;

namespace Payment_Calculator.Controllers;

[ApiController]
[Route("[controller]")]
public class PaymentController: ControllerBase
{
    private readonly ILoansHub _loansHub;
    private readonly DateTime _currentDate;
    private readonly ICalculationService _calculationService;
    
    public PaymentController(ILoansHub loansHub, 
        ICalculationService calculationService)
    {
        _loansHub = loansHub;
        _currentDate = DateTime.Now;
        _calculationService = calculationService;
    }


    [HttpGet("[action]")]
    public ActionResult<FullPaymentModel> GetFullPayment(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Id must be greater than 0");
        }
        
        var loan = _loansHub.GetLoanById(id);
        var baseDebt = _calculationService.GetAmountFromAccount(loan, AccountBaseType.Base, _currentDate); // кредит закрыт, если baseDept == 0

        var result = new FullPaymentModel()
        {
            BaseDebt = baseDebt,
            Interest = baseDebt == 0 ? 0 : _calculationService.GetAmountFromAccount(loan, AccountBaseType.Interest, _currentDate),
            Penalty = baseDebt == 0 ? 0 : _calculationService.GetAmountFromOperations(loan, AccountType.PENALTY, _currentDate)
        };
        
        return Ok(result);
    }

    [HttpGet("[action]")]
    public ActionResult<PartialPaymentModel> GetPartialPayment(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Id must be greater than 0");
        }
        
        var loan = _loansHub.GetLoanById(id);
        var remainingOverdueBase = 
            _calculationService.GetAmountFromOperations(loan, AccountType.OVERDUE_BASE_DEBT, _currentDate) 
            - _calculationService.GetAmountFromOperations(loan, AccountType.PREPAID_BASE_DEBT, _currentDate); // долг минус заранее внесеные платежи, не может быть отрицательным
        
        var remainingOverdueInterest = 
            _calculationService.GetAmountFromOperations(loan, AccountType.OVERDUE_INTEREST, _currentDate) 
            - _calculationService.GetAmountFromOperations(loan, AccountType.PREPAID_INTEREST, _currentDate);
        
        var result = new PartialPaymentModel()
        {
            PrepaidBaseDebt = remainingOverdueBase < 0 ? 0 : remainingOverdueBase,
            PrepaidInterestDebt = remainingOverdueInterest < 0 ? 0 : remainingOverdueInterest
        };
        
        return Ok(result);
    }
}