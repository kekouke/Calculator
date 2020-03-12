﻿using System;
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

        #region Чтение кнопок
        private void button_5_Click(object sender, RoutedEventArgs e)
        {
            ButtonParse("5");
        }

        private void button_8_Click(object sender, RoutedEventArgs e)
        {
            ButtonParse("8");
        }

        private void button_9_Click(object sender, RoutedEventArgs e)
        {
            ButtonParse("9");
        }

        private void button_7_Click(object sender, RoutedEventArgs e)
        {
            ButtonParse("7");
        }

        private void button_4_Click(object sender, RoutedEventArgs e)
        {
            ButtonParse("4");
        }

        private void button_6_Click(object sender, RoutedEventArgs e)
        {
            ButtonParse("6");
        }

        private void button_1_Click(object sender, RoutedEventArgs e)
        {
            ButtonParse("1");
        }

        private void button_mtpl_Click(object sender, RoutedEventArgs e)
        {
            ButtonParse("*");
        }

        private void button_2_Click(object sender, RoutedEventArgs e)
        {
            ButtonParse("2");
        }

        private void button_3_Click(object sender, RoutedEventArgs e)
        {
            ButtonParse("3");
        }

        private void button_minus_Click(object sender, RoutedEventArgs e)
        {
            ButtonParse("-");
        }

        private void button_clear_Click(object sender, RoutedEventArgs e)
        {
            ButtonParse("C");
        }

        private void button_plus_Click(object sender, RoutedEventArgs e)
        {
            ButtonParse("+");
        }

        private void button_answer_Click(object sender, RoutedEventArgs e)
        {
            ButtonParse("=");
        }

        private void button_0_Click(object sender, RoutedEventArgs e)
        {
            ButtonParse("0");
        }

        private void button_point_Click(object sender, RoutedEventArgs e)
        {
            ButtonParse(".");
        }

        private void button_sqrt_Click(object sender, RoutedEventArgs e)
        {
            ButtonParse("sqrt");
        }


        private void button_inv_Click(object sender, RoutedEventArgs e)
        {
            ButtonParse("1/x");
        }

        private void button_dl_Click(object sender, RoutedEventArgs e)
        {
            ButtonParse("b");
        }

        private void button_MC_Click(object sender, RoutedEventArgs e)
        {
            ButtonParse("MC");
        }

        private void button_MR_Click(object sender, RoutedEventArgs e)
        {
            ButtonParse("MR");
        }

        private void button_MPlus_Click(object sender, RoutedEventArgs e)
        {
            ButtonParse("M+");
        }

        private void button_MMinus_Click(object sender, RoutedEventArgs e)
        {
            ButtonParse("M-");
        }

        private void button_CE_Click(object sender, RoutedEventArgs e)
        {
            ButtonParse("CE");
        }

        private void button_ChangeSin_Click(object sender, RoutedEventArgs e)
        {
            ButtonParse("+/-");
        }

        private void button_div_Click(object sender, RoutedEventArgs e)
        {
            ButtonParse("/");
        }

        private void button_sqr_Click(object sender, RoutedEventArgs e)
        {
            ButtonParse("x^2");
        }

        #endregion

        public CalculatorMachine Calc = new CalculatorMachine();

        private void ButtonParse(string buttonContent)
        {
            bool isNumber = Int32.TryParse(buttonContent, out _);

            if (isNumber || buttonContent == ",")
            {
                if (Calc.operation == "")
                {
                    if (Calc.isAnswer)
                    {
                        Calc.leftOperand = "0";
                        Calc.isAnswer = false;
                        historyBlock.Text = "";
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
                    ChangeSign();
                }
                else if (buttonContent == "CE")
                {
                    ClearWindow();
                }
                else if (buttonContent == "b")
                {
                    ClearLastSymbol();
                }
                else if (buttonContent == "sqrt")
                {
                    Sqrt();
                } 
                else if (buttonContent == "x^2")
                {
                    Square();
                }
                else if (buttonContent == "1/x")
                {
                    Inverse();
                }
                else if (buttonContent.Contains("M"))
                {
                    MemoryOperate(buttonContent);
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
                historyBlock.Text += Calc.rightOperand;
                textBlock.Text = Calc.Calc();
            }
           // historyBlock.Text += Calc.rightOperand;
            Calc.operation = "";
        }

        private void Clear()
        {
            Calc.leftOperand = textBlock.Text = "0";
            Calc.rightOperand = "0";
            Calc.operation = "";
            historyBlock.Text = "";
        }

        private void ClearWindow()
        {
            if (Calc.operation == "")
            {
                textBlock.Text = Calc.leftOperand = "0";
                historyBlock.Text = "";
            }
            else
            {
                textBlock.Text = Calc.rightOperand = "0";
            }
        }

        private void ClearLastSymbol()
        {
            if (!Calc.isAnswer)
            {
                if (Calc.operation == "")
                {
                    ClearLastSymbol(ref Calc.leftOperand);
                }
                else
                {
                    ClearLastSymbol(ref Calc.rightOperand);
                }
            }
        }

        private void ClearLastSymbol(ref string value)
        {
                if (value.Length == 1 && value != "0")
                {
                    textBlock.Text = value = "0";
                }
                else if (value.Length > 1)
                {
                    textBlock.Text = value = value[0..^1];
                }
        }

        private void Operate(string buttonContent)
        {
            if (Calc.rightOperand != "0")
            {
                Equal();
            }

            if (!textBlock.Text.Contains("Can't divide by zero"))
            {
                textBlock.Text = Calc.leftOperand;// + buttonContent;
                Calc.operation = buttonContent;
            }

            historyBlock.Text = Calc.leftOperand + Calc.operation;

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
                textBlock.Text = operand;
                textBlock.Text = PointParse(ref operand, buttonContent) ?? textBlock.Text;
            }
        }     

        private void ChangeSign()
        {
            if (Calc.operation == "")
            {
                textBlock.Text = Calc.leftOperand = (-1 * double.Parse(Calc.leftOperand)).ToString();
            }
            else
            {
                textBlock.Text = Calc.rightOperand = (-1 * double.Parse(Calc.rightOperand)).ToString();
            }
        }

        private void Sqrt()
        {
            if (double.Parse(Calc.leftOperand) < 0 || double.Parse(Calc.rightOperand) < 0)
            {
                Clear();
                textBlock.Text = "Error";
                return;
            }

            if (Calc.operation == "")
            {
                textBlock.Text = Calc.leftOperand = (Math.Sqrt(double.Parse(Calc.leftOperand))).ToString();
            }
            else
            {
                textBlock.Text = Calc.rightOperand = (Math.Sqrt(double.Parse(Calc.rightOperand))).ToString();
            }
        }

        private void Square()
        {
            if (Calc.operation == "")
            {
                textBlock.Text = Calc.leftOperand = (double.Parse(Calc.leftOperand) * double.Parse(Calc.leftOperand)).ToString();
            }
            else
            {
                textBlock.Text = Calc.rightOperand = (double.Parse(Calc.rightOperand) * double.Parse(Calc.rightOperand)).ToString();
            }
        }

        private void Inverse()
        {
            Calc.leftOperand = "1";
            Calc.rightOperand = textBlock.Text;
            Calc.operation = "/";
            Equal();
        }

        private void MemoryOperate(string buttonContent)
        {
            if (buttonContent == "MC")
            {
                Calc.memoryActive = false;
                Calc.memory = "0";
                memoryBlock.Text = "В памяти ничего не сохранено";
            }
            else if (buttonContent == "MR")
            {
                if (Calc.memoryActive)
                {
                    textBlock.Text = Calc.leftOperand = Calc.memory;
                }
            }
            else if (buttonContent == "M+")
            {
                Calc.memory = (double.Parse(Calc.memory) + double.Parse(textBlock.Text)).ToString();
                Calc.memoryActive = true;
                memoryBlock.Text = Calc.memory;
            }
            else if (buttonContent == "M-")
            {
                Calc.memory = (double.Parse(Calc.memory) - double.Parse(textBlock.Text)).ToString();
                Calc.memoryActive = true;
                memoryBlock.Text = Calc.memory;
            }
        }

    }
}