using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1_Q1
{
    class Program
    {
        private static readonly Random random = new Random();

        private static double RandomNumberBetween(double minValue, double maxValue)
        {
            var next = random.NextDouble();//Verilen aralıklar içinde rastgele sayılar üretildi.

            return minValue + (next * (maxValue - minValue));
        }

        static void Main(string[] args)
        {
            int n = 10;
            double minimum = 0;
            double width = 100;
            double height = 100;
            double[,] arr = new double[n, 2];
            Program p = new Program();

            for (int i = 0; i < n; i++)
            {
                arr[i, 0] = RandomNumberBetween(minimum, width);
                arr[i, 1] = RandomNumberBetween(minimum, height);
                //Verilen aralıkta n adet rastgele sayı üretmek için.

                for (int j = 0; j < 2; j++)
                {
                    Console.Write(arr[i, j] + " ");//Üretilen rastgele sayılar burda yazdırıldı.
                }
                Console.WriteLine();//Her biri ayrı satıra yazılması için.
            }

            Console.WriteLine(string.Format("{0,92}","Distance Matrix"));

            double[,] DM = new double[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    double xDist = (arr[j, 0] - arr[i, 0]);
                    double pow_tt1 = Math.Pow(xDist, 2);
                    double yDist = (arr[j, 1] - arr[i, 1]);
                    double pow_tt2 = Math.Pow(yDist, 2);
                    //Her noktanın kendi arasında uzaklıkları alındı.

                    DM[i, j] = Math.Sqrt(pow_tt1 + pow_tt2);
                    Console.Write(string.Format("{0,17}", DM[i, j]));
                    //Alınan uzaklıkların her biri ekrana verdirildi.
                }
                Console.WriteLine();//Her biri ayrı satıra yazılması için.
            }
            Console.ReadLine();

        }
    }
}