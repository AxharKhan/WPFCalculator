using System;
using System.Collections.Generic;
using System.Text;

namespace WPFCalculator.Models
{
    internal class Calculation
    {
        public int Id { get; set; }
        public double LeftOperand { get; set; }
        public double RightOperand { get; set; }
        public char Operator { get; set; }
        public double Result { get; set; }
    }
}
