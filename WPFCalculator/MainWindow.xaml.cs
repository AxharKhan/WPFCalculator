using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using WPFCalculator.Models;

namespace WPFCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CollectionViewSource CalculationViewSource;
        private ObservableCollection<Calculation> calculations;
        private char LastOperator = '=';
        public MainWindow()
        {

            InitializeComponent();
            CalculationViewSource = (CollectionViewSource)FindResource(nameof(CalculationViewSource));
            calculations = new ObservableCollection<Calculation>();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CalculationViewSource.Source = calculations;
            InputTextBox.Focus();
        }



        private void InputTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //var textBox = sender as TextBox;
            //e.Handled = Regex.IsMatch(e.Text, "[^0-9]+*-/");
        }

        private void GetResult(char Operator = '=')
        {
            if (Operator != '=')
            {
                LastOperator = Operator;
            }
            else
                LastOperator = '=';
            int iOp = 0;
            if (InputTextBox.Text.Contains("+"))
            {
                iOp = InputTextBox.Text.IndexOf("+");
            }
            else if (InputTextBox.Text.Contains("-"))
            {
                iOp = InputTextBox.Text.IndexOf("-");
            }
            else if (InputTextBox.Text.Contains("*"))
            {
                iOp = InputTextBox.Text.IndexOf("*");
            }
            else if (InputTextBox.Text.Contains("/"))
            {
                iOp = InputTextBox.Text.IndexOf("/");
            }
            else
            {
                //error    
            }


            Operator = InputTextBox.Text.Substring(iOp, 1).ToCharArray()[0];
            if (InputTextBox.Text.Substring(0, iOp) == null || InputTextBox.Text.Substring(0, iOp) == "" || InputTextBox.Text.Substring(0, iOp) == String.Empty)
                return;
            double op1 = Convert.ToDouble(InputTextBox.Text.Substring(0, iOp));

            double op2 = Convert.ToDouble(InputTextBox.Text.Substring(iOp + 1, InputTextBox.Text.Length - iOp - 1));

            if (Operator == '+')
            {
                InputTextBox.Text = (op1 + op2).ToString();
            }
            else if (Operator == '-')
            {
                InputTextBox.Text = (op1 - op2).ToString();
            }
            else if (Operator == '*')
            {
                InputTextBox.Text = (op1 * op2).ToString();
            }
            else
            {
                InputTextBox.Text = (op1 / op2).ToString();
            }
            calculations.Add(new Calculation
            {
                Id = calculations.Count() > 0 ? calculations.Last().Id + 1 : 1,
                LeftOperand = op1,
                RightOperand = op2,
                Operator = Operator,
                Result = double.Parse(InputTextBox.Text)
            });
            //if (LastOperator != '=')
            //    InputTextBox.Text += LastOperator.ToString();

            InputTextBox.Select(InputTextBox.Text.Length, 0);


            CalculationDataGrid.Items.Refresh();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            Button b = (Button)sender;
            if (b.Content.ToString() == "C")
            {
                InputTextBox.Text = "";
                return;
            }
            if (b.Content.ToString() == "Clear")
            {
                calculations.Clear();
                CalculationDataGrid.Items.Refresh();
                return;
            }
            if (b.Content.ToString() == "Del")
            {
                InputTextBox.Text = InputTextBox.Text.Remove(InputTextBox.Text.Length - 1, 1);
                return;
            }
            if (LastOperator != '=' || b.Content.ToString() == "=")

                if (b.Content.ToString() == "+" ||
                b.Content.ToString() == "-" ||
                b.Content.ToString() == "*" ||
                b.Content.ToString() == "/" ||
                b.Content.ToString() == "=")
                {

                    GetResult(b.Content.ToString().ToCharArray()[0]);
                    return;
                }
            if (b.Content.ToString() == "." && InputTextBox.Text.Contains("."))
            {
                return;
            }
            InputTextBox.Text += b.Content.ToString();

        }

        private void InputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                InputTextBox.Text = "";
                return;
            }
            if (e.Key == Key.Add ||
               e.Key == Key.Multiply ||
                e.Key == Key.Divide ||
                e.Key == Key.Subtract ||
                e.Key == Key.Enter ||
                e.Key == Key.Return)
            {
                GetResult(GetCharacter(e.Key));
                return;
            }
            if (e.Key >= Key.A && e.Key <= Key.Z)
            {

                e.Handled = true;
                return;
            }
            if (InputTextBox.Text.Contains(".") && e.Key == Key.Decimal)
            {
                 e.Handled= true;

                //InputTextBox.Text = InputTextBox.Text.Remove(InputTextBox.Text.Length - 1, 1);
                return;

            }

        }

        private char GetCharacter(Key key)
        {
            switch (key)
            {
                case Key.Add:
                    return '+';
                case Key.Subtract:
                    return '-';
                case Key.Divide:
                    return '/';
                case Key.Multiply:
                    return '*';
                default:
                    return '=';

            }

        }
    }
}
