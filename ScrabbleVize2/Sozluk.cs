using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrabbleVize2
{
    public class Sozluk
    {
        static List<string> kelimeler;

    static Sozluk()
        {
            kelimeler = new List<string>(File.ReadAllLines("kelimeler.txt"));
        }
    
        public bool SozlukteVarmı(string kelime)
        {
            return kelimeler.Contains(kelime.ToLower());
        }
    }
}
