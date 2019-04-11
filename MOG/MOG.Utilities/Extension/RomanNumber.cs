using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOG.Utilities.Extension
{
    public static class RomanNumber
    {
        static Dictionary<char, int> CharValues;
        private static string[] ThouLetters = { "", "M", "MM", "MMM" };
        private static string[] HundLetters = { "", "C", "CC", "CCC", "CD", "D", "DC", "DCC", "DCCC", "CM" };
        private static string[] TensLetters = { "", "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC" };
        private static string[] OnesLetters = { "", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX" };

        static RomanNumber()
        {
            CharValues = new Dictionary<char, int>();
            CharValues.Add('I', 1);
            CharValues.Add('V', 5);
            CharValues.Add('X', 10);
            CharValues.Add('L', 50);
            CharValues.Add('C', 100);
            CharValues.Add('D', 500);
            CharValues.Add('M', 1000);
        }
        public static int ToArabic(this string roman)
        {
            int total = 0;
            int last = 0;
            for (int i = roman.Length - 1; i >= 0; i--)
            {
                int value = CharValues[roman[i]];
                if (value < last)
                    total -= value;
                else
                {
                    total += value;
                    last = value;
                }
            }
            return total;
        }

        public static string ToRoman(this int arabic)
        {
            if (arabic >= 4000)
            {
                int thou = arabic / 1000;
                arabic %= 1000;
                return "(" + thou.ToRoman() + ")" +
                    arabic.ToRoman();
            }

            string result = "";

            int num;
            num = arabic / 1000;
            result += ThouLetters[num];
            arabic %= 1000;

            num = arabic / 100;
            result += HundLetters[num];
            arabic %= 100;

            num = arabic / 10;
            result += TensLetters[num];
            arabic %= 10;

            result += OnesLetters[arabic];

            return result;
        }
    }
}
