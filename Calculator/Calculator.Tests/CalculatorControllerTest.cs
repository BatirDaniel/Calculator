using Calculator.Controllers;
using Calculator.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Calculator.Tests;

[TestClass]
public class CalculatorControllerTest
{
    public CalculatorController controller = null;

    [TestInitialize]
    public void Initialize()
    {
        controller = new CalculatorController();
    }

    [TestMethod]
    public void AppendExpression()
    {
        string value = "5";
        controller.AppendExpression(value);

        Assert.AreEqual(value, controller.calculatorViewModel.Expression);
    }

    [TestMethod]
    public void ClearLastElement()
    {
        controller.calculatorViewModel.Expression = "1+2*(34-5)*3";
        controller.ClearLastElement();

        Assert.AreEqual("1+2*(34-5)*", controller.calculatorViewModel.Expression);
    }

    [TestMethod]
    public void ClearExpression()
    {
        controller.calculatorViewModel.Expression = "12345";
        controller.ClearExpression();

        Assert.AreEqual("", controller.calculatorViewModel.Expression);
    }

    [TestMethod]
    public void EvaluateExpression()
    {
        controller.calculatorViewModel.Expression = "3.5-3";

        double result =controller.EvaluateExpression();

        Assert.AreEqual(0.5, result);
    }

    [TestMethod]
    public void EvaluateExpression(CalculatorRequest request)
    {
        request = new CalculatorRequest { Expression = "2/0" };

        var result = controller.EvaluateExpression(request) as BadRequestObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual("Cannot divide by zero .", result.Value);
    }


    [TestMethod]
    public void Index()
    {
        var result = controller.Index() as ViewResult;

        Assert.IsNotNull(result);
    }
}