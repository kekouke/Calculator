﻿using System;
using System.Collections.Generic;


namespace Calculator.Model
{
    public class CalculatorMachine
    {

        private Dictionary<string, OperationDelegate> _operations;

        public string leftOperand;
        public string rightOperand;
        public string operation;
        public bool isAnswer = false;

        public CalculatorMachine()
        {
            leftOperand = "0";
            rightOperand = "";
            operation = "";
        }

        public string Calc()
        {
            decimal num1 = 0, num2 = 0;

            if (leftOperand != "")
                num1 = decimal.Parse(leftOperand);
            if (rightOperand != "")
                num2 = decimal.Parse(rightOperand);

            rightOperand = "";

            if (operation == "/" && num2 == 0)
            {
                leftOperand = "0";
                operation = "";
                return "Can't divide by zero";
            }
            else
            {
                _operations = new Dictionary<string, OperationDelegate>
                {
                    { "+", (x, y) => x + y },
                    { "-", (x, y) => x - y },
                    { "*", (x, y) => x * y },
                    { "/", (x, y) => x / y },
                };
                isAnswer = true;
                return leftOperand = (_operations[operation](num1, num2)).ToString();
            }
        }

    }
}
