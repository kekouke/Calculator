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

        const string memoryIsEmpty = "В памяти ничего не сохранено";

        public CalculatorMachine()
        {
            leftOperand = "0";
            rightOperand = String.Empty;
            operation = String.Empty;
            memory = "0";
            result = "^";
        }

        public string Calc()
        {
            double num1 = 0, num2 = 0;
            num1 = double.Parse(leftOperand);

            if (rightOperand != String.Empty)
            {
                num2 = double.Parse(rightOperand);
            }

            rightOperand = String.Empty;

            if (operation == "/" && num2 == 0)
            {
                leftOperand = "0";
                operation = String.Empty;
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
                isAnswer = true;
                leftOperand = double.Parse(leftOperand).ToString();
                return leftOperand;

            }
            else
            {
                result = Calc();
            }
            operation = String.Empty;
            return result;
        }

        public string Clear()
        {
            leftOperand = "0";
            rightOperand = String.Empty;
            operation = String.Empty;
            isAnswer = false;
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
                rightOperand = String.Empty;
            }
            return "0";
        }

        public string ClearLastSymbol()
        {
            string value;
            if (!isAnswer || operation != String.Empty)
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

            if (rightOperand != String.Empty)
            {
                result = Equal();
            }
            else
            {
                result = "0";    
            }

            operation = buttonContent;
            return result;
        }

        public string NumberClick(string buttonContent)
        {
            if (isAnswer && operation == String.Empty)
            {
                leftOperand = "0";
                isAnswer = false;
            }

            if (operation == String.Empty)
            {
                if (leftOperand.Length < 13)
                {

                    if (leftOperand == String.Empty || leftOperand == "0")
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

                    if (rightOperand == String.Empty || rightOperand == "0")
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
            if (operation == String.Empty)
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
            switch(buttonContent)
            {
                case "MC":
                    memoryActive = false;
                    memory = "0";
                    return memoryIsEmpty;

                case "MR":
                    if (memoryActive)
                    {
                        if (operation == String.Empty)
                        {
                            leftOperand = memory;
                        }
                        else
                        {
                            rightOperand = memory;
                        }
                        return memory;
                    }
                    else
                    {
                        return memoryData;
                    }

                case "M+":
                    if (double.TryParse(memoryData, out _))
                    {
                        memory = (double.Parse(memory) + double.Parse(memoryData)).ToString();
                        memoryActive = true;
                        return memory;
                    }
                    else
                    {
                        return memoryIsEmpty;
                    }

                case "M-":
                    if (double.TryParse(memoryData, out _))
                    {
                        memory = (double.Parse(memory) - double.Parse(memoryData)).ToString();
                        memoryActive = true;
                        return memory;
                    }
                    else
                    {
                        return memoryIsEmpty;
                    }

                case "MS":
                    if (double.TryParse(memoryData, out _))
                    {
                        memory = double.Parse(memoryData).ToString();
                        memoryActive = true;
                        return memory;
                    }
                    else
                    {
                        return memoryIsEmpty;
                    }

                default:
                    return null;
            }
        }

        public string PointParse(ref string operand)
        {   

            if (isAnswer && operation == String.Empty)
            {
                operand = "0";
                isAnswer = false;
            }

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
