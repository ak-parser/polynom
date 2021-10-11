using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Polynom
{
    class Polynom
    {
        private List<double> coef;
        private double x;
        private int m;

        public List<double> Coef
        {
            get
            {
                return coef;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Coefficients not set");
                coef = value;
            }
        }

        public double X { get; set; }

        public int M
        {
            get
            {
                return m;

            }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Power of polymon cannot be < 0");
                m = value;
            }
        }

        public double this[int index]
        {
            get
            {
                if (index < 0 || index > M)
                    throw new IndexOutOfRangeException("Index cannot be < 0");
                return Coef[index];
            }
            set
            {
                if (index < 0)
                    throw new IndexOutOfRangeException("Index cannot be < 0");

                if (value != 0)
                {
                    if (index >= M)
                    {
                        List<double> tempCoef = new List<double>(index + 1);
                        for (int i = 0; i < M; i++)
                            tempCoef[i] = coef[i];

                        coef = tempCoef;
                        M = index + 1;
                    }

                    coef[index] = value;
                }
                else if (index < M)
                    coef[index] = value;

            }
        }

        public Polynom()
            : this(new List<double>() { 0 }, 0)
        {
                
        }

        public Polynom(List<double> coefficient, int m)
        {
            Coef = coefficient;
            M = m;
        }

        public Polynom AddPolynom(Polynom polynom)
        {
            if (polynom == null)
                throw new ArgumentNullException("Polynom to add not set");

            int tempMinM = this.M < polynom.M ? this.M : polynom.M;
            int tempMaxM = this.M > polynom.M ? this.M : polynom.M;
            List<double> tempCoef = new List<double>(tempMaxM + 1);

            for (int i = 0; i <= tempMinM; i++)
                tempCoef.Add(this[i] + polynom[i]);

            if (this.M > polynom.M)
            {
                for (int i = tempMinM + 1; i <= tempMaxM; i++)
                    tempCoef.Add(this[i]);
            }
            else
            {
                for (int i = tempMinM + 1; i <= tempMaxM; i++)
                    tempCoef.Add(polynom[i]);
            }

            return new Polynom(tempCoef, tempMaxM);
        }

        public Polynom SubtractionPolynom(Polynom polynom)
        {
            if (polynom == null)
                throw new ArgumentNullException("Polynom to add not set");

            int tempMinM = this.M < polynom.M ? this.M : polynom.M;
            int tempMaxM = this.M > polynom.M ? this.M : polynom.M;
            List<double> tempCoef = new List<double>(tempMaxM + 1);

            for (int i = 0; i <= tempMinM; i++)
                tempCoef.Add(this[i] - polynom[i]);

            if (this.M > polynom.M)
            {
                for (int i = tempMinM + 1; i <= tempMaxM; i++)
                    tempCoef.Add(this[i]);
            }
            else
            {
                for (int i = tempMinM + 1; i <= tempMaxM; i++)
                    tempCoef.Add(polynom[i]);
            }

            return new Polynom(tempCoef, tempMaxM);
        }

        public Polynom MultiplyPolynom(Polynom polynom)
        {
            if (polynom == null)
                throw new ArgumentNullException("Polynom to add not set");

            int tempM = this.M + polynom.M;
            double[] tempCoef = new double[tempM + 1];

            for (int i = 0; i <= this.M; i++)
                for (int j = 0; j <= polynom.M; j++)
                    tempCoef[i + j] += this[i] * polynom[j];

            List<double> tempCoefList = new List<double>(tempM + 1);
            for (int i = 0; i < tempCoef.Length; i++)
                tempCoefList.Add(tempCoef[i]);

            return new Polynom(tempCoefList, tempM);
        }

        public void Parse(string s)
        {
            if (s == null)
                throw new ArgumentNullException("String to add not set");

            string[] input = new StringReader(s).ReadToEnd().Split(" ", StringSplitOptions.RemoveEmptyEntries);

            int tempM = Convert.ToInt32(input[0]);
            
            M = tempM;
            Coef.Clear();
            
            for (int i = 1; i <= tempM + 1; i++)
                Coef.Add(Convert.ToInt32(input[i]));
        }

        public override string ToString()
        {
            string info = $"Polynom: m = {M}\n{this[0]:f4}";

            for (int i = 1; i <= M; i++)
                info += $" + {this[i]:f4}*x^{i}";

            return info + "\n";
        }

        public static Polynom operator+(Polynom polynom1, Polynom polynom2)
        {
            return polynom1.AddPolynom(polynom2);
        }
        public static Polynom operator-(Polynom polynom1, Polynom polynom2)
        {
            return polynom1.SubtractionPolynom(polynom2);
        }
        public static Polynom operator*(Polynom polynom1, Polynom polynom2)
        {
            return polynom1.MultiplyPolynom(polynom2);
        }

        public static implicit operator Polynom(double koef)
        {
            return new Polynom(new List<double> { koef}, 0);
        }
    }
}
