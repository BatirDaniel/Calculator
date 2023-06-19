# Calculator
This is a simple Calculator application developed in (ASP.NET) using HTML, CSS, JavaScript and C# allowing users to perform basic mathematical operations.

## Features

- User-friendly and easy-to-use interface.
- Supports addition, subtraction, multiplication, and division operations.
- Handles both integer and decimal numbers.
- The "=" button calculates and displays the result of the entered expression.
- The "C" button clears the entire expression.
- The "<i class='bx bx-undo'></i>" button deletes the last entered element in the expression.
- Supports parentheses for grouping mathematical expressions.

## Technologies Used

- HTML5 for structuring the web page.
- CSS3 for styling the elements and visual appearance.
- JavaScript for interactive logic and manipulation of mathematical expressions.
- Boxicons CSS Library for displaying icons used in buttons.
- Utilizes AJAX technology through XMLHttpRequest (XHR) to fetch and display the expression result.

## Algorithm for Evaluating Mathematical Expressions with Two Stacks

The algorithm for evaluating mathematical expressions with two stacks can be used to process expressions in Reverse Polish Notation (RPN). The algorithm follows these steps:

- Initialize two stacks, one for operators and another for operands.
- Read the expression from left to right.
- If the current token is an operand, push it onto the operand stack.
- If the current token is an operator, pop the top two operands from the operand stack.
- Apply the operator to the operands and push the result onto the operand stack.
- Continue reading and processing the expression until all tokens have been processed.
- The final result will be at the top of the operand stack.
This algorithm allows for the correct evaluation of mathematical expressions, taking into account the precedence and associativity of the operators.

## Unit testing

Unit testing plays a important role in ensuring the correctness and reliability of the Calculator application. Several unit tests have been implemented to verify the functionalities related to adding elements, deleting elements, and calculating expressions.

The unit tests are written using a unit testing framework compatible with ASP.NET.
Here are some examples of the unit tests:

- `AppendExpression` verifies that the `AppendExpression` function correctly adds an element to the expression.
- `ClearLastElement` tests the `ClearLastElement` function to ensure that it removes the last entered element from the expression.
- `ClearExpression` checks if the `ClearExpression` function clears the entire expression.
- `EvaluateExpression` tests the calculation of an expression and verifies that the result is correct.
- `EvaluateExpression_CannotDivideByZero` validates that the appropriate error message is returned when attempting to divide by zero.

These unit tests provide confidence in the reliability and accuracy of the Calculator application. They are executed automatically during the development process, and any failures or unexpected behavior are identified and resolved promptly.

Feel free to explore the test files and add more tests as needed to further enhance the quality of the Calculator application.
