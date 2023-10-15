using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Business.Constants
{
    public class GuidGenerator
    {
        public string NewGuid()
        {
            Random rnd = new Random();
            int length = rnd.Next(20, 25);
            string[] code = new string[length];
            int letterOrNumber, upperOrLower, number;
            char letter;
            for (int i = 0; i < code.Length; i++)
            {
                letterOrNumber = rnd.Next(0, 2);

                if (letterOrNumber == 0)
                {
                    upperOrLower = rnd.Next(0, 2);
                    if (upperOrLower == 0)
                    {
                        letter = (char)rnd.Next(65, 91);
                    }
                    else
                    {
                        letter = (char)rnd.Next(97, 123);
                    }

                    code[i] = letter.ToString();
                }
                else
                {
                    number = rnd.Next(0, 10);
                    code[i] = number.ToString();
                }
            }
            return String.Join("", code);
        }
    }
}
