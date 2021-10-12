using System;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp19
{
    class Program
    {
        static double[] printSmall(double[] arr, int asize, int n)
        { 
            double[] copy_arr = new double[asize];
            Array.Copy(arr, copy_arr, asize);
 
            Array.Sort(copy_arr);

            double[] smallest = new double[n];
            int counter = 0;

 
            for (int i = 0; i < asize; ++i)
            {
                if (Array.BinarySearch(copy_arr, 0, n, arr[i]) > -1)
                {
                    smallest[counter] = arr[i];
                    counter = counter + 1;
                }

            }

            return smallest;
        }

        static double[] printSmallTest(double[] arr, int asize, int n)
        {
            double[] copy_arr = new double[asize];
            Array.Copy(arr, copy_arr, asize);

            Array.Sort(copy_arr);

            double[] smallestTest = new double[n];
            int counter = 0;
 
            for (int i = 0; i < asize; ++i)
            {
                if (Array.BinarySearch(copy_arr, 0, n, arr[i]) > -1)
                {
                    smallestTest[counter] = arr[i];
                    counter = counter + 1;
                }

            }

            return smallestTest;
        }

        static void Main(string[] args)
        {
            //1172 verinin olduğu Data dosyası okutuldu.
            //Text'in olduğu kaynak açılan bilgisayara göre farklılık gösterir.
            var lines = File.ReadAllLines(@"C:\Users\sbk07\Desktop\Data.txt");
            double[,] DM = new double[lines.Length, 5];
            for (int i = 0; i < lines.Length; i++)
            {
                var fields = lines[i].Split(',');
                for (int j = 0; j < fields.Length; j++)
                {
                    //Okutulan değerler DM matrisine atandı.
                    DM[i, j] = Convert.ToDouble(fields[j], CultureInfo.InvariantCulture);
                }
            }

            for (int i = 0; i < DM.GetLength(0); i++)
            {
                for (int j = 0; j < DM.GetLength(1); j++)
                {
                    //1172 veri konsolda listelendi.
                    Console.Write(string.Format("{0,10}", DM[i, j]));
                }
                Console.WriteLine();
            }

            double[,] userInput = new double[1, 4];

            //Kullanıcıdan 4 özellik için değerler istendi ve bu değerler userInput'ta tutuldu.
            Console.Write("Varyans değeri girin:");
            userInput[0, 0] = Convert.ToDouble(Console.ReadLine(), CultureInfo.InvariantCulture);
            Console.WriteLine();//new line at each row

            Console.Write("Çarpıklık değeri girin:");
            userInput[0, 1] = Convert.ToDouble(Console.ReadLine(), CultureInfo.InvariantCulture);
            Console.WriteLine();//new line at each row

            Console.Write("Basıklık değeri girin:");
            userInput[0, 2] = Convert.ToDouble(Console.ReadLine(), CultureInfo.InvariantCulture);
            Console.WriteLine();//new line at each row

            Console.Write("Entropi değeri girin:");
            userInput[0, 3] = Convert.ToDouble(Console.ReadLine(), CultureInfo.InvariantCulture);
            Console.WriteLine();//new line at each row

            Console.WriteLine("Kullanıcıdan girilen değerler:");

            for (int j = 0; j < userInput.GetLength(1); j++)
            {
                //Kullanıcının girdiği özellikler ekrana verildi.
                Console.Write(string.Format("{0,10}", userInput[0, j]));
            }
            Console.WriteLine();//yeni bir satıra geçmek için.

            double[] differences = new double[DM.GetLength(0)];

            for (int i = 0; i < DM.GetLength(0); i++)
            {
                double sum = 0;
                for (int j = 0; j < userInput.GetLength(1); j++)
                {
                    //Girdinin 1172 veri ile uzaklık ölçümleri yapıldı.
                    double dist = (userInput[0, j] - DM[i, j]);
                    double dist_pow = Math.Pow(dist, 2);
                    sum = sum + dist_pow;
                }
                differences[i] = Math.Sqrt(sum);
            }

            Console.Write("K numarası girin:");
            int n = Convert.ToInt32(Console.ReadLine(), CultureInfo.InvariantCulture);
            int asize = differences.Length;
            double[] smallest = new double[n];
            //istenen k numarası kadar en yakın nokta bulmak için fonksiyona gönderildi.
            smallest = printSmall(differences, asize, n);

            double[,] banknoteInfo = new double[n, 6];

            int zeroCounter = 0;
            int oneCounter = 0;

            for (int i = 0; i < smallest.GetLength(0); i++)
            {
                double findingValue = smallest[i];
                for (int j = 0; j < differences.GetLength(0); j++)
                {
                    if (findingValue == differences[j])
                    {
                        double findingIndex = j;
                        double findingTür = DM[j, 4];

                        banknoteInfo[i, 0] = DM[j, 0];
                        banknoteInfo[i, 1] = DM[j, 1];
                        banknoteInfo[i, 2] = DM[j, 2];
                        banknoteInfo[i, 3] = DM[j, 3];
                        banknoteInfo[i, 4] = DM[j, 4];
                        banknoteInfo[i, 5] = differences[j];

                        if (findingTür == 0)
                        {
                            zeroCounter = zeroCounter + 1;
                        }
                        else if (findingTür == 1)
                        {
                            oneCounter = oneCounter + 1;
                        }
                    }
                }
            }

            for (int i = 0; i < banknoteInfo.GetLength(0); i++)
            {
                for (int j = 0; j < banknoteInfo.GetLength(1); j++)
                {
                    Console.Write(string.Format("{0,18}", banknoteInfo[i, j]));
                }
                Console.WriteLine();//yeni bir satıra geçmek için.
            }

            //En yakın noktaların 1 ve 0 durumlarına göre girilen paranın sınıfı belirlendi.

            if (zeroCounter > oneCounter)
            {
                Console.WriteLine("Girdinizin değeri 0.");
            }

            else if (zeroCounter < oneCounter)
            {
                Console.WriteLine("Girdinizin değeri 1.");
            }

            var linesTest = File.ReadAllLines(@"C:\Users\sbk07\Desktop\Test.txt");//200 adet test verisi okutuldu.
            double[,] DMTest = new double[linesTest.Length, 5];
            for (int i = 0; i < linesTest.Length; i++)
            {
                var fieldsTest = linesTest[i].Split(',');
                for (int j = 0; j < fieldsTest.Length; j++)
                {
                    DMTest[i, j] = Convert.ToDouble(fieldsTest[j], CultureInfo.InvariantCulture);
                }
            }

            double[] differencesTest = new double[DM.GetLength(0)];

            Console.Write("K değeri girin:");
            int k = Convert.ToInt32(Console.ReadLine(), CultureInfo.InvariantCulture);
            int asizeTest = differencesTest.Length;
            double[] smallestTest = new double[k];
            double[] typeGuess = new double[DMTest.GetLength(0)];
            double[] typeReal = new double[DMTest.GetLength(0)];
            int correct = 0;

            for (int i = 0; i < DMTest.GetLength(0); i++)
            {
                typeReal[i] = DMTest[i, 4];
                for (int p = 0; p < DM.GetLength(0); p++)
                {
                    double sumTest = 0;
                    double dist1 = (DMTest[i, 0] - DM[p, 0]);
                    double dist_pow1 = Math.Pow(dist1, 2);
                    double dist2 = (DMTest[i, 1] - DM[p, 1]);
                    double dist_pow2 = Math.Pow(dist2, 2);
                    double dist3 = (DMTest[i, 2] - DM[p, 2]);
                    double dist_pow3 = Math.Pow(dist3, 2);
                    double dist4 = (DMTest[i, 3] - DM[p, 3]);
                    double dist_pow4 = Math.Pow(dist4, 2);
                    sumTest = dist_pow1 + dist_pow2 + dist_pow3 + dist_pow4;
                    differencesTest[p] = Math.Sqrt(sumTest);
                }
                smallestTest = printSmallTest(differencesTest, asizeTest, k);

                int zeroCounterTest = 0;
                int oneCounterTest = 0;

                for (int a = 0; a < smallestTest.GetLength(0); a++)
                {
                    double findingValue = smallestTest[a];
                    for (int b = 0; b < differences.GetLength(0); b++)
                    {
                        if (findingValue == differencesTest[b])
                        {
                            double findingIndex = b;
                            double findingTür = DM[b, 4];

                            if (findingTür == 0)
                            {
                                zeroCounterTest = zeroCounterTest + 1;
                            }
                            else if (findingTür == 1)
                            {
                                oneCounterTest = oneCounterTest + 1;
                            }
                        }
                    }
                }

                if (zeroCounterTest > oneCounterTest)
                {
                    typeGuess[i] = 0;
                }

                else if (zeroCounterTest < oneCounterTest)
                {
                    typeGuess[i] = 1;
                }

                if (typeGuess[i] == typeReal[i])
                {
                    correct = correct + 1;
                }
            }
            int oran = 0;
            oran = correct / 200;
            Console.WriteLine("Sizin basari oraniniz:" + oran);
            Console.ReadLine();
        }
    }
}