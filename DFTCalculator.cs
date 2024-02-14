///////////////////////////////
//Made by Sifeddine Akaaboune//
///////////////////////////////

using System;
using System.IO;

namespace LearnC
{
    public struct Complex
    {
        public float real;
        public float imaginary;
        public Complex(float real, float imaginary)
        {
            this.real = real;
            this.imaginary = imaginary;
        }
        public static Complex operator +(Complex one, Complex two)
        {
            return new Complex(one.real + two.real, one.imaginary + two.imaginary);
        }
        public override string ToString()
        {
            return (String.Format("{0} + {1}i", real, imaginary));
        }
    }

    class DFTCalculator
    {
        static void Main(string[] args)
        {
            Console.Write("Input the number of samples (N) : ");
            int N = Convert.ToInt32(Console.ReadLine()); //Number of samples
            string filepath = @"dftpoints.txt";

            //[ 0, 0.707, 1, 0.707, 0, -0.707, -1, -0.707] <- test sample points for N = 8, Sin wave of 1 Hz and Amplitude 1.

            double[] s = new double[N]; //Sample points
            Complex q = new Complex(0, 0);
            Complex[] ca = new Complex[N];   //Array that stores the complex values of one frequency bin to add them up(Gets cleared in the next frequency bin.
            Complex[] res = new Complex[N]; //Sum for each frequency bin

            Console.WriteLine("\nInput the samples:");
            for (int i = 0; i < s.Length; i++)
            {
                s[i] = Convert.ToDouble(Console.ReadLine());
            }

            int j = 0;

            Console.WriteLine("\n\nSample points: \n[{0}]", string.Join(", ", s));
            Console.WriteLine("\n\n\n");

            for (int k = 0; k < N; k++) // k represents the frequency bin.
            {                           // So this will loop up to X(N-1).
                Console.WriteLine("k : " + k + "\n");

                int i = 0;

                for (int n = 0; n < N; n++) // n represents the nth sample 
                {
                    double b = -(2 * Math.PI * k * n / N);

                    double cosB = s[i] * Math.Cos(b);
                    double sinB = s[i] * Math.Sin(b);

                    Complex e = new Complex((float)cosB, (float)sinB);

                    ca[i] = e;
                    i++;
                }

                Complex sum = new Complex(0, 0);
                foreach (Complex a in ca)
                {
                    sum += a;
                }
                
                if (sum.real < 1 && sum.real > -1)
                {
                    sum.real = 0;
                }

                if (sum.imaginary < 1 && sum.imaginary > -1)
                {
                    sum.imaginary = 0;
                }

                sum.real = MathF.Round(sum.real);
                sum.imaginary = MathF.Round(sum.imaginary);

                res[j] = sum;

                Console.WriteLine("X(k) = " + sum + "\n\n");

                j++;
            }

            string[] lines = new string[2*N];

            int d = 0;
            foreach (Complex c in res)
            {
                lines[d] = c.ToString();
                d++;
            }
        
            File.WriteAllLines(filepath, lines);

            Console.ReadKey();
        }
    }
}
