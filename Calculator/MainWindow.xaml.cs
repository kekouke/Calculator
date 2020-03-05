﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Windows;
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
            int trash;
            string buttonContent = (string)((Button)e.OriginalSource).Content;
            bool isNumber = Int32.TryParse(buttonContent, out trash);

            if (leftOperand == "" && isNumber && operation == "")
            {
                textBlock.Text = buttonContent;
                answer = "";
            }
            else
                textBlock.Text += buttonContent;

            if (isNumber)
            {
                if (operation == "")
                {
                    if (leftOperand == "0")
                        leftOperand = textBlock.Text = buttonContent;
                    else
                        leftOperand += buttonContent;
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
                }
            }
            else
            {
                if (buttonContent == "=")
                {
                    if (answer != "")
                        textBlock.Text = answer;
                    else if (leftOperand == "")
                        textBlock.Text = "0";
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
                    if (rightOperand != "")
                    {
                        Calc();

                        leftOperand = textBlock.Text;
                        textBlock.Text += buttonContent;
                    }
                    else if (operation != "")
                        textBlock.Text = leftOperand != "" ? leftOperand + buttonContent: "0" + buttonContent;

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
    }
}