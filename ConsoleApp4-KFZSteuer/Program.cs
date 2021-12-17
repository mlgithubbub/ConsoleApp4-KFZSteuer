using System;
using System.Globalization;

namespace ConsoleApp4_KFZSteuer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Function to input motor type
            string motorTyp = inputMotorTyp();
            //Function to input Hubraum and parse from string to double
            double hubraum = inputHubraum();
            //Function to input co2 and parse from string to double
            double co2 = inputCO2();

            //Function to calculate Hubraum tax, Co2 tax, and total KFZ tax
            dataOutput(motorTyp, hubraum, co2);
        }

        private static string inputMotorTyp()
        {
            Console.WriteLine("Please enter the motor type. B = Benzin, D = Diesel.");
            string motorTyp = Console.ReadLine();
            return motorTyp;
        }

        private static double inputHubraum()
        {
            Console.WriteLine("Please enter the Hubraum in CCM.");
            string inputHubraum = Console.ReadLine();
            double hubraum = double.Parse(inputHubraum);
            return hubraum;
        }

        private static double inputCO2()
        {
            Console.WriteLine("Please enter the CO2 in g.");
            string inputCo2 = Console.ReadLine();
            double co2 = double.Parse(inputCo2);
            return co2;
        }

        //Function to calculate total KFZ tax
        private static decimal kfzSteuer(string motorTyp, double hubraum, double co2)
        {
            decimal kfzST = hubraumAnteil(motorTyp, hubraum) + co2Anteil(co2);
            return kfzST;
        }

        //Function to calculate Hubraum tax
        private static decimal hubraumAnteil(string motorTyp, double hubraum)
        {
            decimal hrAnteil = 0;
            if (motorTyp == "B")
            {
                hrAnteil = Convert.ToDecimal((Math.Round(hubraum / 100) * 2.00));
            }
            else if (motorTyp == "D")
            {
                hrAnteil = Convert.ToDecimal((Math.Round(hubraum / 100)) * 9.50);
            }

            return hrAnteil;
        }

        //Function to calculate Co2 tax
        private static decimal co2Anteil(double co2)
        {
            decimal co2AT;
            if (co2 > 195)
            {
                co2AT = Convert.ToDecimal(((co2 - 195) * 4) + ((195 - 176 + 1) * 3.4) +
                                          ((175 - 156 + 1) * 2.9) + ((155 - 136 + 1) * 2.5) +
                                          ((135 - 116 + 1) * 2.2) + ((115 - 96 + 1) * 2.0));
            }
            else if (co2 >= 176 && co2 <= 195)
            {
                co2AT = Convert.ToDecimal(((co2 - 176 + 1) * 3.4) +
                                          ((175 - 156 + 1) * 2.9) + ((155 - 136 + 1) * 2.5) +
                                          ((135 - 116 + 1) * 2.2) + ((115 - 96 + 1) * 2.0));
            }
            else if (co2 >= 156 && co2 <= 175)
            {
                co2AT = Convert.ToDecimal(((co2 - 156 + 1) * 2.9) + ((155 - 136 + 1) * 2.5) +
                                          ((135 - 116 + 1) * 2.2) + ((115 - 96 + 1) * 2.0));
            }
            else if (co2 >= 136 && co2 <= 155)
            {
                co2AT = Convert.ToDecimal(((co2 - 136 + 1) * 2.5) +
                                          ((135 - 116 + 1) * 2.2) + ((115 - 96 + 1) * 2.0));
            }
            else if (co2 >= 135 && co2 <= 116)
            {
                co2AT = Convert.ToDecimal(((co2 - 116 + 1) * 2.2) + ((115 - 96 + 1) * 2.0));
            }
            else if (co2 >= 96 && co2 <= 115)
            {
                co2AT = Convert.ToDecimal(((co2 - 96 + 1) * 2.0));
            }
            else
            {
                co2AT = 0;
            }

            return co2AT;
        }

        //Function to calculate all taxes and print to the console
        private static void dataOutput(string motorTyp, double hubraum, double co2)
        {
            decimal hubraumSteuer = hubraumAnteil(motorTyp, hubraum);
            decimal co2Steuer = co2Anteil(co2);
            decimal KFZSteuer = kfzSteuer(motorTyp, hubraum, co2);

            Console.WriteLine(
                $"You must pay this amount in Hubraumsteuer: {hubraumSteuer.ToString("C", CultureInfo.CreateSpecificCulture("de-DE"))}.");
            Console.WriteLine(
                $"You must pay this amount in CO2 Steuer: {co2Steuer.ToString("C", CultureInfo.CreateSpecificCulture("de-DE"))}.");
            Console.WriteLine(
                $"You must pay total KFZ-Steuer: {KFZSteuer.ToString("C", CultureInfo.CreateSpecificCulture("de-DE"))}.");
        }
    }
}