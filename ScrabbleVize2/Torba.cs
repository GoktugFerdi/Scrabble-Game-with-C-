using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ScrabbleVize2
{
    public class Torba
    {
        List<Tas> taslar = new List<Tas>();
        Random randomDagıt = new Random();

        public void TasOlustur()
        {
            Ekle('A', 12, 1);
            Ekle('B', 2, 3);
            Ekle('C', 2, 4);
            Ekle('Ç', 2, 4);
            Ekle('D', 2, 3);
            Ekle('E', 8, 1);
            Ekle('F', 1, 7);
            Ekle('G', 1, 5);
            Ekle('Ğ', 1, 8);
            Ekle('H', 1, 5);
            Ekle('I', 4, 2);
            Ekle('İ', 7, 1);
            Ekle('J', 1, 10);
            Ekle('K', 7, 1);
            Ekle('L', 7, 1);
            Ekle('M', 4, 2);
            Ekle('N', 5, 1);
            Ekle('O', 3, 2);
            Ekle('Ö', 1, 7);
            Ekle('P', 1, 5);
            Ekle('R', 6, 1);
            Ekle('S', 3, 2);
            Ekle('Ş', 2, 4);
            Ekle('T', 5, 1);
            Ekle('U', 3, 2);
            Ekle('Ü', 2, 3);
            Ekle('V', 1, 7);
            Ekle('Y', 2, 3);
            Ekle('Z', 2, 4);
            Ekle('*', 2, 0);

        }

        void Ekle(char harf , int adet ,int puan)
        {
            for(int i = 0; i < adet; i++)
            {
                Tas tas = new Tas();
                tas.TasAta(harf, puan);
                taslar.Add(tas);
            }      
        }

        public List<Tas> Cek(int adet)
        {
            List<Tas> cekilen = new List<Tas>();
            for(int i = 0;i < adet && taslar.Count > 0; i++)
            {
                int index = randomDagıt.Next(taslar.Count);  //rastgele bir index seçip oradan taş çekiyoruz
                cekilen.Add(taslar[index]);                  //çektiğimiz taşı cekilen adlı yeni bir yere ekliyoruz
                taslar.RemoveAt(index);                      //artık taşımız taslar listesinde olmadığından onu ordan siliyoruz
            }

            return cekilen;
        }

        public int HarfPuani (char harf)
        {
            if(harf == '*')
            {
                return 0;
            }

            switch(char.ToUpper(harf))
            {
                case 'A':
                case 'E':
                case 'İ':
                case 'K':
                case 'N':
                case 'R':
                case 'L':
                case 'T':
                    return 1;

                case 'I':
                case 'O':
                case 'M':
                case 'U':
                case 'S':
                    return 2;

                case 'B':
                case 'D':
                case 'Ü':
                case 'Y':
                    return 3;

                case 'C':
                case 'Ç':
                case 'Ş':
                case 'Z':
                    return 4;

                case 'G':
                case 'H':
                case 'P':
                    return 5;
                case 'V':
                case 'F':
                case 'Ö':
                    return 7;

                case 'Ğ':
                    return 8;

                case 'J':
                    return 10;

                default:
                    return 0;
            }
        }

        private static int TP(Tas bulunanTas)
        {
            if (bulunanTas != null)
            {
                return bulunanTas.Puan;

            }
            else
            {
                Console.WriteLine("Kullanmaya Çalıştığınız Taş Mevcut Değil Lütfen Geçerli Bir Taş Girişi Yapınız");
                return 0;
            }
        }

        public int Kalan => taslar.Count;                   
    }
}
