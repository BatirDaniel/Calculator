using System.Data;
using System.Security.Cryptography.Xml;
using System.Text.RegularExpressions;

namespace Calculator.Models
{
    public class CalculatorViewModel
    {
        public string? Expression { get; set; }

        public double CalculateExpression()
        {
            var dataTable = new DataTable();

            var result = dataTable.Compute(Expression, "");

            Regex regex = new Regex(@"\/\s*0\b");

            if (Expression != null && regex.IsMatch(Expression))
            {
                throw new DivideByZeroException("Cannot divide by zero .");
            }

            if (result == DBNull.Value)
            {
                throw new Exception("The result of the computation is null or empty.");
            }
            else
            {
                return Convert.ToDouble(result);
            }
        }
    }
}
