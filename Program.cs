using System;
using System.Collections.Generic;
using System.IO;

namespace Polynom
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string input = "4 1 2 3 4 5";

                Polynom polynom = new Polynom(new List<double> { 12.5, 1.23, 4.4545, 23, 5.2, 7.8, 15 }, 6);
                Polynom polynom1 = new Polynom();
                polynom1.Parse(input);

                Console.WriteLine("1 polinom:\n" + polynom);
                Console.WriteLine("2 polinom:\n" + polynom1);

                Console.WriteLine("After add 2 polinoms:\n" + polynom.AddPolynom(polynom1));

                Console.WriteLine("After subtraction 2 polinoms:\n" + polynom.SubtractionPolynom(polynom1));

                Console.WriteLine("After multiply 2 polinoms:\n" + polynom.MultiplyPolynom(polynom1));
            }
            catch (ArgumentNullException exception)
            {
                Console.WriteLine(exception.Message);
            }
            catch (ArgumentException exception)
            {
                Console.WriteLine(exception.Message);
            }
            catch (FileNotFoundException exception)
            {
                Console.WriteLine(exception.Message);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
