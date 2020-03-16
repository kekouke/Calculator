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

        #region Чтение чисел

        private void button_0_Click(object sender, RoutedEventArgs e)
        {
            ButtonInt("0");
        }

        private void button_1_Click(object sender, RoutedEventArgs e)
        {
            ButtonInt("1");
        }

        private void button_2_Click(object sender, RoutedEventArgs e)
        {
            ButtonInt("2");
        }

        private void button_3_Click(object sender, RoutedEventArgs e)
        {
            ButtonInt("3");
        }

        private void button_4_Click(object sender, RoutedEventArgs e)
        {
            ButtonInt("4");
        }

        private void button_5_Click(object sender, RoutedEventArgs e)
        {
            ButtonInt("5");
        }

        private void button_6_Click(object sender, RoutedEventArgs e)
        {
            ButtonInt("6");
        }

        private void button_7_Click(object sender, RoutedEventArgs e)
        {
            ButtonInt("7");
        }

        private void button_8_Click(object sender, RoutedEventArgs e)
        {
            ButtonInt("8");
        }

        private void button_9_Click(object sender, RoutedEventArgs e)
        {
            ButtonInt("9");
        }

        #endregion

        #region Чтение кнопок

        private void button_mtpl_Click(object sender, RoutedEventArgs e)
        {
            OperateParse("*");
        }

        private void button_minus_Click(object sender, RoutedEventArgs e)
        {
            OperateParse("-");
        }

        private void button_clear_Click(object sender, RoutedEventArgs e)
        {
            textBlock.Text = Calc.Clear();
            historyBlock.Text = String.Empty;
        }

        private void button_plus_Click(object sender, RoutedEventArgs e)
        {
            OperateParse("+");
        }

        private void button_answer_Click(object sender, RoutedEventArgs e)
        {
            textBlock.Text = Calc.Equal();
            historyBlock.Text = String.Empty;
        }

        private void button_point_Click(object sender, RoutedEventArgs e)
        {
            if (Calc.operation == "")
            {
                textBlock.Text = Calc.PointParse(ref Calc.leftOperand);
            }
            else
            {
                textBlock.Text = Calc.PointParse(ref Calc.rightOperand);
            }
        }

        private void button_dl_Click(object sender, RoutedEventArgs e)
        {
            textBlock.Text = Calc.ClearLastSymbol();
        }

        private void button_MS_Click(object sender, RoutedEventArgs e)
        {
            var memoryData = textBlock.Text;
            memoryBlock.Text = Calc.MemoryOperate("MS", memoryData); 
        }

        private void button_MC_Click(object sender, RoutedEventArgs e)
        {
            var memoryData = textBlock.Text;
            memoryBlock.Text = Calc.MemoryOperate("MC", memoryData);
        }

        private void button_MR_Click(object sender, RoutedEventArgs e)
        {
            var memoryData = textBlock.Text;
            textBlock.Text = Calc.MemoryOperate("MR", memoryData);
        }

        private void button_MPlus_Click(object sender, RoutedEventArgs e)
        {
            var memoryData = textBlock.Text;
            memoryBlock.Text = Calc.MemoryOperate("M+", memoryData);
        }

        private void button_MMinus_Click(object sender, RoutedEventArgs e)
        {
            var memoryData = textBlock.Text;
            memoryBlock.Text = Calc.MemoryOperate("M-", memoryData);
        }

        private void button_CE_Click(object sender, RoutedEventArgs e)
        {
            textBlock.Text = Calc.ClearWindow();
        }

        private void button_ChangeSin_Click(object sender, RoutedEventArgs e)
        {
            textBlock.Text = Calc.ChangeSign();
        }

        private void button_div_Click(object sender, RoutedEventArgs e)
        {
            OperateParse("/");
        }

        #endregion

        private void OperateParse(string buttonContent)
        {
            textBlock.Text = Calc.Operate(buttonContent);
            historyBlock.Text = Calc.operation;
        }     

        private void ButtonInt(string buttonContent)
        {

            if (Calc.isAnswer && Calc.operation == String.Empty)
            {
                Calc.leftOperand = "0";
                Calc.isAnswer = false;
            }

            textBlock.Text = Calc.NumberClick(buttonContent);
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
    }
}