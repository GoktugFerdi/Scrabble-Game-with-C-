using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ScrabbleVize2
{
    public class Tahta
    {
        static int boyut = 15;
        Hücre[,] hucreler = new Hücre[boyut, boyut];
        Torba torba = new Torba();
        public void TahtaÇiz()
        {
            for (int i = 0; i < boyut; i++)
            {
                for (int j = 0; j < boyut; j++)
                {
                    hucreler[i, j] = new Hücre();
                }
            }

            Carpanlar();
        }
        void Carpanlar()
        {
            // K3 Konumları
            CK3(0, 0);
            CK3(0, 7);
            CK3(0, 14);
            CK3(7, 0);
            CK3(7, 14);
            CK3(14, 0);
            CK3(14, 7);
            CK3(14, 14);

            // K2 Konumları
            CK2(1, 1); CK2(2, 2); CK2(3, 3);
            CK2(4, 4); CK2(7, 7); CK2(10, 10);
            CK2(11, 11); CK2(12, 12); CK2(13, 13);
            CK2(1, 13); CK2(2, 12); CK2(3, 11);
            CK2(4, 10); CK2(10, 4); CK2(11, 3); CK2(12, 2);
            CK2(13, 1);

            // H2 Konumları
            CH2(0, 3); CH2(0, 11);
            CH2(2, 6); CH2(2, 8);
            CH2(3, 0); CH2(3, 7); CH2(3, 14);
            CH2(6, 2); CH2(6, 6); CH2(6, 8); CH2(6, 12);
            CH2(7, 3); CH2(7, 11);
            CH2(8, 2); CH2(8, 6); CH2(8, 8); CH2(8, 12);
            CH2(11, 0); CH2(11, 7); CH2(11, 14);
            CH2(12, 6); CH2(12, 8);
            CH2(14, 3); CH2(14, 11);

            // H3 Konumları
            CH3(1, 5); CH3(1, 9);
            CH3(5, 1); CH3(5, 5); CH3(5, 9); CH3(5, 13);
            CH3(9, 1); CH3(9, 5); CH3(9, 9); CH3(9, 13);
            CH3(13, 5); CH3(13, 9);
        }

        private void CH2(int x, int y)
        {
            hucreler[x, y].kutu = "H2";
            hucreler[x, y].kayıtkutusu = "H2";
        }
        private void CH3(int x, int y)
        {
            hucreler[x, y].kutu = "H3";
            hucreler[x, y].kayıtkutusu = "H3";
        }
        private void CK2(int x, int y)
        {
            hucreler[x, y].kutu = "K2";
            hucreler[x, y].kayıtkutusu = "K2";
        }
        private void CK3(int x, int y)
        {
            hucreler[x, y].kutu = "K3";
            hucreler[x, y].kayıtkutusu = "K3";
        }

        public void Yazdır()
        {
            Console.WriteLine("     0    1    2    3    4    5    6    7    8    9   10   11   12   13   14");
            for (int i = 0; i < boyut; i++)
            {
                Console.Write("  ");
                for (int j = 0; j < boyut; j++)
                    Console.Write("+----");
                Console.WriteLine("+");

                Console.Write($"{i,2} ");
                for (int j = 0; j < boyut; j++)
                {
                    string ic;

                    if (hucreler[i, j].harf != ' ')
                    {
                        ic = hucreler[i, j].harf.ToString();
                    }
                    else
                    {

                        ic = hucreler[i, j].kutu.Trim();
                    }

                    Console.Write($"|{ic.PadRight(3)} "); //Tablo yamuk oluyodu bunu kullanmam gerekmiş yapay zekadan öğrendim burayı ama copy paste yapmadım
                }
                Console.WriteLine("|");
            }
            Console.Write("  ");
            for (int j = 0; j < boyut; j++)
                Console.Write("+----");
            Console.WriteLine("+");
        }
        public bool KelimeDiz(string kelime, Koordinat koordinat, char yon)
        {
            if (yon != 'Y' && yon != 'D')
            {
                return false; 
            }

            
            if ((yon == 'Y' && koordinat.Y + kelime.Length > boyut) ||
                (yon == 'D' && koordinat.X + kelime.Length > boyut))
            {
                return false; 
            }

            
            for (int i = 0; i < kelime.Length; i++)
            {
                int satir;
                int sutun;

                
                if (yon == 'Y') 
                {
                    satir = koordinat.X;       
                    sutun = koordinat.Y + i;   
                }
                else 
                {
                    satir = koordinat.X + i;   
                    sutun = koordinat.Y;       
                }

                
                if (hucreler[satir, sutun].harf != ' ')
                {
                   
                    if (hucreler[satir, sutun].harf != kelime[i])
                    {
                        return false; 
                    }
                    
                }
                
            }

            
            for (int i = 0; i < kelime.Length; i++)
            {
                int satir;
                int sutun;

               
                if (yon == 'Y') 
                {
                    satir = koordinat.X;
                    sutun = koordinat.Y + i;
                }
                else 
                {
                    satir = koordinat.X + i;
                    sutun = koordinat.Y;
                }

                
                if (hucreler[satir, sutun].harf == ' ')
                {
                    hucreler[satir, sutun].harf = kelime[i]; 
                                                             
                    hucreler[satir, sutun].kutu = $" {kelime[i]} ";
                }
            }

            return true; 
        }

        public int KelimePuanHesapla(string kelime, Koordinat koordinat, char yon, Torba torba)
        {
            int kpuan = 0;   
            int kcarpan = 1; 

            for (int i = 0; i < kelime.Length; i++)
            {
                int satir;
                int sutun;

                if (yon == 'Y') 
                {
                    satir = koordinat.X;
                    sutun = koordinat.Y + i;
                }
                else 
                {
                    satir = koordinat.X + i;
                    sutun = koordinat.Y;
                }

                int hpuan = torba.HarfPuani(kelime[i]); 
                int hcarpan = 1;                        

                string kayıt = hucreler[satir, sutun].kayıtkutusu; 

                if (kayıt.StartsWith("H2")) 
                {
                    hcarpan = 2;
                }
                else if (kayıt.StartsWith("H3")) 
                {
                    hcarpan = 3;
                }
                else if (kayıt.StartsWith("K2")) 
                {
                    kcarpan *= 2; 
                }
                else if (kayıt.StartsWith("K3")) 
                {
                    kcarpan *= 3; 
                }

                kpuan += hpuan * hcarpan;
            }
            return kpuan * kcarpan;
        }

    }
} 


