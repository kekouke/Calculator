using System;
using System.Windows;
using Calculator.Model;
using System.Windows.Controls;

namespace Calculator
{

    public delegate double OperationDelegate(double x, double y);

    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        public CalculatorMachine Calc = new CalculatorMachine();

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string buttonContent = (string)((Button)e.OriginalSource).Content;
            bool isNumber = Int32.TryParse(buttonContent, out _);


            if (isNumber || buttonContent == ",")
            {
                if (Calc.operation == "")
                {
                    if (Calc.isAnswer)
                    {
                        Calc.leftOperand = "0";
                        Calc.isAnswer = false;
                    }

                    NumberClick(ref Calc.leftOperand, buttonContent);
                }
                else
                {
                    NumberClick(ref Calc.rightOperand, buttonContent);
                }
            }
            else
            {
                if (buttonContent == "=")
                {
                    Equal();
                }
                else if (buttonContent == "C")
                {
                    Clear();
                }
                else if (buttonContent == "+/-")
                {
                    
                }
                else if (buttonContent == "CE")
                {
                    ClearWindow();
                }
                else if (buttonContent == "(x)")
                {
                    ClearLastSymbol();
                }
                else if (buttonContent == "sqrt")
                {
                    Calc.leftOperand = Math.Sqrt(double.Parse(Calc.leftOperand)).ToString();
                    textBlock.Text = Calc.leftOperand;
                }
                else if (buttonContent == "x^2")
                {
                    Calc.rightOperand = Calc.leftOperand;
                    Calc.operation = "*";
                    Calc.Calc();
                }
                else
                {
                    Operate(buttonContent);
                }
            }

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            string value = menuItem.Header.ToString();

            if (value == "Exit")
                Close();
            if (value == "Help")
                MessageBox.Show("Калькулятор 1.0.0.0\nСпособен выполнять базовые арифметические операции" +
                            " - сложение, вычитание, умножение, деление.\n© Козицкий Михаил(kekouke),\n2020.");
        }

        private string PointParse(ref string operand, string content)
        {

            if (content == ",")
            {

                if (operand == ",")
                {
                    operand = "0,";
                }
                else if (operand[0..^1].Contains(","))
                {
                    operand = operand[0..^1];
                }

                operationHistory.Text = Calc.leftOperand + Calc.operation + Calc.rightOperand;
                return operand;
            }
            return null;
        }

        private void Equal()
        {
            if (Calc.rightOperand == "")
            {
                textBlock.Text = Calc.leftOperand;
            }
            else
            {
                textBlock.Text = Calc.Calc();
            }
            Calc.operation = "";
        }

        private void Clear()
        {
            Calc.leftOperand = textBlock.Text = "0";
            Calc.rightOperand = Calc.operation = "";
            operationHistory.Text = "0";
        }

        private void ClearWindow()
        {

        } //TODO

        private void ClearLastSymbol()
        {

        } //TODO

        private void Operate(string buttonContent)
        {
            if (Calc.rightOperand != "")
            {
                Equal();
            }

            if (!textBlock.Text.Contains("Can't divide by zero"))
            {
                textBlock.Text = Calc.leftOperand + buttonContent;
                operationHistory.Text = Calc.leftOperand + buttonContent + Calc.rightOperand;
                Calc.operation = buttonContent;
            }

        }

        private void NumberClick(ref string operand, string buttonContent)
        {
            if (operand.Length < 16)
            {
                if (operand == "0")
                {
                    operand = buttonContent;
                }
                else
                {
                    operand += buttonContent;
                }
                operationHistory.Text = Calc.leftOperand + Calc.operation + Calc.rightOperand;
                textBlock.Text = operand;
                textBlock.Text = PointParse(ref operand, buttonContent) ?? textBlock.Text;
            }
        }     
    }
}