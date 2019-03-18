using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            string file_address = @"C:\Users\yura1\Documents";
            string file_name;

            file_name = "pangrams.txt.bz2base64";
            Obrah(file_address, file_name);




            Console.ReadKey(true);
        }

        static void Obrah(string file_address, string file_name)
        {
            char[] alphabet = new char[65] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '+', '/', '=' };
            double[] numberOfOccurence = new double[65];
            double frequency = 0;
            double entropy = 0;
            double amount_of_letters = 0;
            FileInfo file = new FileInfo(Path.Combine(file_address, file_name));
            using (StreamReader sr = new StreamReader(Path.Combine(file_address, file_name), System.Text.Encoding.GetEncoding("KOI8-u")))
            {
                int i = 1;
                char[] array = new char[1];
                while (sr.Peek() > -1)
                {
                    sr.Read(array, 0, 1);
                    for (int j = 0; j < 33; j++)
                    {
                        if (alphabet[j] == array[0])
                        {
                            numberOfOccurence[j]++;
                        }

                        i++;
                    }
                }
            }
            for (int k = 0; k < 65; k++)
            {
                amount_of_letters += numberOfOccurence[k];

            }
            for (int i = 0; i < 65; i++)
            {
                frequency = numberOfOccurence[i] / amount_of_letters;
                Console.WriteLine(alphabet[i] + "   " + numberOfOccurence[i] + "   " + frequency);
                if (frequency != 0)
                {
                    entropy += frequency * (Math.Log(1 / frequency, 2));
                }
            }
            Console.WriteLine("entropy:  " + entropy);
            Console.WriteLine("count of inf: " + entropy * amount_of_letters / 8 + " size of file: " + file.Length + " letters: " + amount_of_letters + "\n");
        }
    }
}
