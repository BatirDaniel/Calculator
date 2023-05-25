﻿using Calculator.Models;
using Microsoft.AspNetCore.Mvc;

namespace Calculator.Controllers
{
    public class CalculatorController : Controller
    {
        private CalculatorViewModel calculatorViewModel;

        public CalculatorController()
        {
            calculatorViewModel = new CalculatorViewModel();
        }

        public void AppendExpression(string value)
        {
            calculatorViewModel.Expression += value;
        }

        public void ClearLastElement()
        {
            int length = calculatorViewModel.Expression.Length;

            calculatorViewModel.Expression.Remove(length - 1);
        }

        public void ClearExpression()
        {
            calculatorViewModel.Expression = "";
        }

        public double EvaluateExpression()
        {
            return calculatorViewModel.CalculateExpression();
        }

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
