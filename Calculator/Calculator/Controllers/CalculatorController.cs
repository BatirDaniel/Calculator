﻿using Calculator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;

namespace Calculator.Controllers
{
    public class CalculatorController : Controller
    {
        public CalculatorViewModel? calculatorViewModel;

        public CalculatorController()
        {
            calculatorViewModel = new CalculatorViewModel();
        }

        [JSInvokable]

        public void AppendExpression(string value)
        {
            calculatorViewModel.Expression += value;
        }

        [JSInvokable]
        public void ClearLastElement()
        {
            int length = calculatorViewModel.Expression.Length;

            if (length > 0)
            {
                calculatorViewModel.Expression = calculatorViewModel.Expression.Substring(0, length - 1);
            }
        }

        [JSInvokable]
        public void ClearExpression()
        {
            calculatorViewModel.Expression = "";
        }

        public double EvaluateExpression()
        {
            return calculatorViewModel.CalculateExpression();
        }

        [JSInvokable]
        [HttpPost]
        public IActionResult EvaluateExpression([FromBody] CalculatorRequest request)
        {
            try
            {
                calculatorViewModel.Expression = request.Expression;
                double result = calculatorViewModel.CalculateExpression();
                return Ok(result);
            }
            catch (Exception err)
            {
                return BadRequest(new { error = err.Message });
            }
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
