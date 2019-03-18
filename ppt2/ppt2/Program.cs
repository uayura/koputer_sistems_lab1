using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ppt2
{
    class Program
    {
        public static int eqv;
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            string file_address_read = @"C:\Users\yura1\Documents";
            string file_name_read = @"pangrams.txt.bz2";
            string file_address_write = @"C:\Users\yura1\Documents";
            string file_name_write = file_name_read + "base64";

            byte[] temp_byte = con_to_byte(file_address_read, file_name_read);
            List<string> temp_string = con_to_bit_6(temp_byte);
            create_file(temp_string, file_address_write, file_name_write);


            


            Console.ReadKey();

        }

        public static byte [] con_to_byte(string file_address, string file_name)
        {
            string strText = "";
            using (StreamReader sr = new StreamReader(Path.Combine(file_address, file_name), System.Text.Encoding.Default))
            {
                strText = sr.ReadToEnd();
            }
            byte[] bytes = Encoding.UTF8.GetBytes(strText);

            return bytes;
        }

        public static List<string> con_to_bit_6(byte[] temp)
        {
            List<string> st = new List<string>();
            List<string> stb = new List<string>();
            string temp_s = "";
            //претворяємо байти в біти, дописуємо нулі на початок неповного октету (наприклад 10110  ==> 00010110) записуємо все в ліст
            for (int i = 0; i < temp.Length; i++)
            {
                temp_s = Convert.ToString(temp[i], 2);
                while (temp_s.Length != 8)
                {
                    temp_s = "0" + temp_s;
                }
                st.Add(temp_s);
            }

            eqv = st.Count % 3;
            Console.WriteLine(eqv);
            //витягуємо по 3 байти, претворюємо в 4 масива (стрінга) по 6 біт, записуємо в новий ліст
            for (int i = 0; i < st.Count-eqv; i += 3)
            {
                temp_s = st[i] + st[i + 1] + st[i + 2];
                char[] a = temp_s.ToCharArray();
                for (int j = 0; j < a.Length; j += 6)
                {
                    stb.Add(Convert.ToString(a[j]) + Convert.ToString(a[j + 1]) + Convert.ToString(a[j + 2]) + Convert.ToString(a[j + 3]) + Convert.ToString(a[j + 4]) + Convert.ToString(a[j + 5]));
                }
            }

            if (eqv == 2)
            {
                temp_s = st[st.Count-eqv] + st[st.Count - eqv+1] + "00";
                char[] a = temp_s.ToCharArray();
                for (int j = 0; j < a.Length; j += 6)
                {
                    stb.Add(Convert.ToString(a[j]) + Convert.ToString(a[j + 1]) + Convert.ToString(a[j + 2]) + Convert.ToString(a[j + 3]) + Convert.ToString(a[j + 4]) + Convert.ToString(a[j + 5]));
                }
            }
            else if(eqv==1)
            {
                temp_s = st[st.Count - eqv] + "0000";
                char[] a = temp_s.ToCharArray();
                for (int j = 0; j < a.Length; j += 6)
                {
                    stb.Add(Convert.ToString(a[j]) + Convert.ToString(a[j + 1]) + Convert.ToString(a[j + 2]) + Convert.ToString(a[j + 3]) + Convert.ToString(a[j + 4]) + Convert.ToString(a[j + 5]));
                }
            }
            return stb;
        }

        public static void create_file(List<string> stb, string file_address, string file_name)
        {
            char[] base64_alphabet = new char[65]{'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h','i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q' , 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z','0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '+', '/', '='};
            using (StreamWriter sw = new StreamWriter(Path.Combine(file_address, file_name)))
            {
                foreach(string s in stb)
                {
                    //Console.WriteLine(Convert.ToInt32(s, 2));
                    //Console.WriteLine(base64_alphabet[Convert.ToInt32(s, 2)]);
                    sw.Write(base64_alphabet[Convert.ToInt32(s, 2)]);
                }
                while (eqv > 1)
                {
                    sw.Write(base64_alphabet[64]);
                    eqv--;
                }
                Console.WriteLine("end");
            }
        }
    }
}
