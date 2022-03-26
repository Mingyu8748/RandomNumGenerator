using System;
using System.Numerics;
using System.Reflection.Metadata;

namespace RandomNumGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("For BoxMullerTransform Method: ");
            Console.WriteLine("Enter the correlation between the two random variables");
            var resultBoxMuller = BoxMullerTransform(Console.ReadLine());
            Console.WriteLine("The first random number is: " + resultBoxMuller["z1"]);
            Console.WriteLine("The second random number  is: " + resultBoxMuller["z2"]);
            
            Console.WriteLine();
            Console.WriteLine("======================================================");
            Console.WriteLine();
            
            Console.WriteLine("For Polar Rejection Method: ");
            Console.WriteLine("Enter the correlation between the two random variables");
            var resultPolarRejection = PolarRejection(Console.ReadLine());
            Console.WriteLine("The first random number is: " + resultPolarRejection["z1"]);
            Console.WriteLine("The second random number  is: " + resultPolarRejection["z2"]);
            
            Console.WriteLine();
            Console.WriteLine("======================================================");
            Console.WriteLine();
            
            Console.WriteLine("For Sum Twelve Method: ");
            Console.WriteLine("Enter the correlation between the two random variables");
            var resultTSumTwelve = SumTwelve(Console.ReadLine());
            Console.WriteLine("The first random number is: " + resultTSumTwelve["z1"]);
            Console.WriteLine("The second random number  is: " + resultTSumTwelve["z2"]);
        }

        public static Dictionary<string, double> BoxMullerTransform(string? corrString)
        {
            var random = new Random();
            var r1 = random.NextDouble();
            var r2 = random.NextDouble();
            var y1 = Math.Sqrt(-2.0 * Math.Log(r1)) * Math.Cos(2.0 * Math.PI * r2);
            var y2 = Math.Sqrt(-2.0 * Math.Log(r1)) * Math.Sin(2.0 * Math.PI * r2);
            var corr = Convert.ToDouble(corrString);
            var z1 = y1;
            var z2 = corr * y1 + Math.Sqrt(1 - Math.Pow(corr, 2) * y2);
            return new Dictionary<string, double>
            {
                {"z1", z1},
                {"z2", z2}
            };
        }


        public static Dictionary<string, double>  PolarRejection(string? corrString)
        {
            double w,v1,v2;
            do {
                v1 = new Random().NextDouble();
                v2 = new Random().NextDouble();
                w = Math.Pow(v1, 2) + Math.Pow(v2, 2);
            } while (w > 1.0) ;

            var c = Math.Sqrt(-2.0 * Math.Log(w) / w);

            var y1 = c * v1;
            var y2 = c * v2;

            var corr = Convert.ToDouble(corrString);
            var z1 = y1;
            var z2 = corr * y1 + Math.Sqrt(1 - Math.Pow(corr, 2) * y2);
            return new Dictionary<string, double>
            {
                {"z1", z1},
                {"z2", z2}
            };
        }


        public static Dictionary<string, double> SumTwelve(string? corrString)
        {
            var random = new Random();
            var uniformDisArray1 = new double[12];
            var uniformDisArray2 = new double[12];

            for (int j = 0; j < 12; j++)
            {
                uniformDisArray1[j] = random.NextDouble();
            }

            var y1 = uniformDisArray1.Sum() -6;
            
            for (int i = 0; i < 12; i++)
            {
                uniformDisArray2[i] = random.NextDouble();
            }
            var y2 = uniformDisArray2.Sum() -6;
            
            var corr = Convert.ToDouble(corrString); 
            var z1 = y1;
            var z2 = corr * y1 + Math.Sqrt(1 - Math.Pow(corr, 2) * y2);
            return new Dictionary<string, double>
            {
                {"z1", z1},
                {"z2", z2}
            };

        }

    }
}