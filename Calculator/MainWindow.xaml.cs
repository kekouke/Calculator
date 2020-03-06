using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Windows.Controls;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public delegate double OperationDelegate(double x, double y);

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private Dictionary<string, OperationDelegate> _operations;


        string leftOperand = "";
        string rightOperand = "";
        string operation = "";
        string answer = "";

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string buttonContent = (string)((Button)e.OriginalSource).Content;
            bool isNumber = Int32.TryParse(buttonContent, out _);

            if (leftOperand == "" && (isNumber || buttonContent == "," ) && operation == "")
            {
                textBlock.Text = buttonContent;
                answer = "";
            }
            else
                textBlock.Text += buttonContent;

            if (isNumber || buttonContent == ",")
            {
                if (operation == "")
                {
                    if (leftOperand == "0")
                        leftOperand = textBlock.Text = buttonContent;
                    else
                        leftOperand += buttonContent;
                    PointParse(ref leftOperand, buttonContent);
                }
                else
                {
                    if (operation != "" && answer != "")
                        leftOperand = answer;

                    if (rightOperand == "0")
                    {
                        textBlock.Text = textBlock.Text[0..^2] + buttonContent;
                        rightOperand = buttonContent;
                    }
                    else
                    {
                        rightOperand += buttonContent;
                    }
                    PointParse(ref rightOperand, buttonContent);
                }
            }
            else
            {
                if (buttonContent == "=")
                {
                    if (leftOperand == "")
                    {
                        if (answer == "")
                            textBlock.Text = "0";
                        else
                            textBlock.Text = answer;
                    }
                    else if (leftOperand != "" && rightOperand == "")
                        textBlock.Text = leftOperand;
                    else
                        Calc();
                    operation = "";
                }
                else if (buttonContent == "C")
                {
                    textBlock.Text = "0";
                    leftOperand = rightOperand = operation = answer = "";
                }
                else
                {
                    if (leftOperand == "")
                        leftOperand = "0";
                    if (rightOperand != "")
                    {
                        Calc();

                        leftOperand = textBlock.Text;
                        textBlock.Text += buttonContent;
                    }
                    else if (operation != "")
                        textBlock.Text = leftOperand != "" ? leftOperand + buttonContent : "0" + buttonContent;

                    operation = buttonContent;
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
        private void Calc()
        {
            double num1 = 0, num2 = 0;

            if (leftOperand != "")
                num1 = Double.Parse(leftOperand);
            if (rightOperand != "")
                num2 = Double.Parse(rightOperand);

            if (operation == "/" && num2 == 0)
            {
                MessageBox.Show("Can't divide by zero");
                textBlock.Text = "0";
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

                answer = textBlock.Text = (_operations[operation](num1, num2)).ToString();
            }
            leftOperand = rightOperand = operation = "";
        }

        private void PointParse(ref string operand, string content)
        {

            if (content == ",")
            {

                if (operand == ",")
                     operand = "0,";
                else if (operand[0..^1].Contains(","))
                    operand = operand[0..^1];
                textBlock.Text = leftOperand + operation + rightOperand;
            }
        }
    }
}