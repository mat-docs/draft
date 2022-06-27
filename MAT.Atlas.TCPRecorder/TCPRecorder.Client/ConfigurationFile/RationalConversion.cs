// <copyright file="RationalConversion.cs" company="McLaren Applied Ltd.">
// Copyright (c) McLaren Applied Ltd.</copyright>

namespace TCPRecorder.Client.ConfigurationFile
{
    public sealed class RationalConversion : Conversion
    {
        public RationalConversion(
            string name,
            double coefficient1,
            double coefficient2,
            double coefficient3,
            double coefficient4,
            double coefficient5,
            double coefficient6)
            : base(name, ConversionType.Rational)
        {
            this.Coefficients = new[]
            {
                coefficient1, coefficient2, coefficient3, coefficient4, coefficient5, coefficient6
            };
        }

        public double[] Coefficients { get; set; }

        public static RationalConversion CreateSimple1To1Conversion(string name)
        {
            return new RationalConversion(name, 0.0, 1.0, 0.0, 0.0, 0.0, 1.0);
        }
    }
}