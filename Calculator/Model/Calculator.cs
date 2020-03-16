using System;
using System.Collections.Generic;
using System.Windows;
namespace Calculator.Model
{
    public class CalculatorMachine
    {

        private Dictionary<string, OperationDelegate> _operations;

        public string leftOperand;
        public string rightOperand;
        public string operation;
        public string memory;
        private string result;
        public bool isAnswer = false;
        public bool memoryActive = false;

        public CalculatorMachine()
        {
            leftOperand = "0";
            rightOperand = "";
            operation = "";
            memory = "0";
            result = "^";
        }

        public string Calc()
        {
            double num1 = 0, num2 = 0;
            num1 = double.Parse(leftOperand);

            if (rightOperand != "")
            {
                num2 = double.Parse(rightOperand);
            }

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
                    { "*", (x, y) => x * y + 0 },
                    { "/", (x, y) => x / y },
                };
                isAnswer = true;
                return leftOperand = Math.Round((_operations[operation](num1, num2)), 10).ToString();
            }
        }

        public string Equal()
        {
            if (operation == String.Empty)
            {
                return leftOperand;

            }
            else
            {
                result = Calc();
            }
            operation = String.Empty; // Возможно, это можно удалить в функции Calc()
            return result;
        }

        public string Clear()
        {
            leftOperand = "0";
            rightOperand = "";
            operation = "";
            return "0";
        }

        public string ClearWindow()
        {
            if (operation == String.Empty)
            {
                leftOperand = "0";
            }
            else
            {
                rightOperand = "";
            }
            return "0";
        }

        public string ClearLastSymbol()
        {
            string value;
            if (!isAnswer)
            {
                if (operation == String.Empty)
                {
                    value = ClearLastSymbol(ref leftOperand);
                }
                else
                {
                    value = ClearLastSymbol(ref rightOperand);
                }
                return value;
            }
            else
            {
                return leftOperand;
            }
            
        }

        private string ClearLastSymbol(ref string value)
        {
            if (value.Length == 1 && value != "0")
            {
                value = "0";
            }
            else if (value.Length > 1)
            {
                value = value[0..^1];
            }

            return value;
        }

        public string Operate(string buttonContent)
        {
            result = "^";

            if (rightOperand != "")
            {
                result = Equal();
            }

            if (!result.Contains("Can't divide by zero"))
            {
                result = leftOperand;
                operation = buttonContent;
            }
            return result;
        }

        public string NumberClick(string buttonContent) // TODO : ENUM
        {
            if (operation == "")
            {
                if (leftOperand.Length < 13)
                {
                    if (leftOperand == "0")
                    {
                        leftOperand = buttonContent;
                    }
                    else
                    {
                        leftOperand += buttonContent;
                    }
                }
                return leftOperand;
            }
            else
            {
                if (rightOperand.Length < 13)
                {

                    if (rightOperand == "" || rightOperand == "0")
                    {
                        rightOperand = buttonContent;
                    }
                    else
                    {
                        rightOperand += buttonContent;
                    }
                }
                return rightOperand;
            }

            
        }

        public string ChangeSign()
        {
            if (operation == "")
            {
                result = leftOperand = (-1 * double.Parse(leftOperand) + 0).ToString();
            }
            else
            {
                if (rightOperand == String.Empty)
                {
                    rightOperand = "0";
                }
                result = rightOperand = (-1 * double.Parse(rightOperand) + 0).ToString();
            }

            return result;
        }

        public string MemoryOperate(string buttonContent, string memoryData)
        {
            if (buttonContent == "MC")
            {
                memoryActive = false;
                memory = "0";
                return "В памяти ничего не сохранено";
            }
            else if (buttonContent == "MR")
            {
                if (memoryActive)
                {
                    leftOperand = memory;
                    return memory;
                }
            }
            else if (buttonContent == "M+")
            {
                memory = (double.Parse(memory) + double.Parse(memoryData)).ToString();
                memoryActive = true;
                return memory;
            }
            else if (buttonContent == "M-")
            {
                memory = (double.Parse(memory) - double.Parse(memoryData)).ToString();
                memoryActive = true;
                return memory;
            }
            else if (buttonContent == "MS")
            {
                memory = memoryData;
                memoryActive = true;
                return memory;
            }
            return memoryData;

        }

        public string PointParse(ref string operand)
        {          
            if (!operand.Contains(","))
            {
                operand += ",";
            }

            if (operand == ",")
            {
                operand = "0,";
            }

            return operand;
        }
    }
}
