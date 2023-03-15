using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Payment_Calculator.Controllers;
using Payment_Calculator.Enums;
using Payment_Calculator.Interfaces;
using Payment_Calculator.Interfaces.IServices;
using Payment_Calculator.Models;

namespace Payment_Calculator_Tests;

public class PaymentControllerTests
{
    private Mock<ILoansHub> _mockLoansHub;
    private Mock<ICalculationService> _mockCalculationService;
    private PaymentController _controller;
    
    [SetUp]
    public void Setup()
    {
        _mockLoansHub = new Mock<ILoansHub>();
        _mockCalculationService = new Mock<ICalculationService>();
        _controller = new PaymentController(_mockLoansHub.Object, _mockCalculationService.Object);
    }

    [Test]
    public void GetFullPayment_WithInvalidId_ReturnsBadRequest()
    {
        // Arrange
        int id = 0;

        // Act
        var result = _controller.GetFullPayment(id);

        // Assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
    }
    
    [Test]
    public void GetFullPayment_WithValidId_ReturnsOk()
    {
        // Arrange
        int id = 1;
        var loanMock = new Mock<ILoan>();
        _mockLoansHub.Setup(x => x.GetLoanById(id)).Returns(loanMock.Object);

        // Act
        var result = _controller.GetFullPayment(id);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);
    }
    
    [Test]
    public void GetPartialPayment_WithInvalidId_ReturnsBadRequest()
    {
        // Arrange
        int id = 0;

        // Act
        var result = _controller.GetPartialPayment(id);

        // Assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
    }
    
    [Test]
    public void GetPartialPayment_WithValidId_ReturnsOk()
    {
        // Arrange
        int id = 1;
        var loanMock = new Mock<ILoan>();
        _mockLoansHub.Setup(x => x.GetLoanById(id)).Returns(loanMock.Object);

        // Act
        var result = _controller.GetPartialPayment(id);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);
    }
}