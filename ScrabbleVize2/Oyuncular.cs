using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ScrabbleVize2
{
    public class Oyuncular
    {
        
        public string Isim;
        public string Soyisim;
        public int Yas;
        public int Puan;
        public List<Tas> El;
        

        
        public Oyuncular(string isim, string soyisim, int yas,List<Tas> baslangıc)
        {
            Isim = isim;
            Soyisim = soyisim;
            Yas = yas;
            El = baslangıc;
            
        }

        public void ElAc()
        {
            Console.WriteLine($"{Isim} {Soyisim} Elindeki Harfler:");
            foreach(var tas in El)
            {
                Console.WriteLine($"{tas.Harf}");
            }
            Console.WriteLine();
        }

        public bool HarfVarMı(string kelime)                           
        {
            List<Tas> Kopya = new List<Tas>();
            foreach (Tas tas in El)
            {
                Kopya.Add(tas);
            }

            foreach (char harf in kelime)
            {
                bool key = false;
                
                for (int i = 0; i < Kopya.Count; i++)
                {
                    if (Kopya[i].Harf == harf)
                    {
                        Kopya.RemoveAt(i); 
                        key = true;
                        break; 
                    }
                }

                if (!key) 
                {
                    for (int i = 0; i < Kopya.Count; i++)
                    {
                        if (Kopya[i].Harf == '*')
                        {
                            Kopya.RemoveAt(i); 
                            key = true;
                            break; 
                        }
                    }
                }

                if (!key) 
                {
                    return false; 
                }
            }
            return true; 
        }
        public void HarfleriKullan(string kelime)
        {
            List<Tas> temp = new List<Tas>();
            foreach (Tas tas in El)
            {
                temp.Add(tas);
            }

            foreach (char harf in kelime)
            {
                bool key2 = false;
                for (int i = 0; i < temp.Count; i++)
                {
                    if (temp[i].Harf == harf)
                    {
                        temp.RemoveAt(i);
                        key2 = true;
                        break;
                    }
                }

                if (!key2) 
                {
                    for (int i = 0; i < temp.Count; i++)
                    {
                        if (temp[i].Harf == '*')
                        {
                            temp.RemoveAt(i);
                            key2 = true;
                            break;
                        }
                    }
                }
            }
            El = temp; 
        }


        public void YeniTasEkle(List<Tas> yeniTas)
        {
            El.AddRange(yeniTas);
        }

    }
}
