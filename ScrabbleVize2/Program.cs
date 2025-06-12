using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrabbleVize2
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("Oyuncu 1 ismini girsin");
            string o1name = Console.ReadLine();
            Console.WriteLine("Oyuncu 1 Soyisimini girsin");
            string o1sname = Console.ReadLine();
            Console.WriteLine("Oyuncu 1 yaşını girsin");
            int o1yas = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Oyuncu 2 ismini girsin");
            string o2name = Console.ReadLine();
            Console.WriteLine("Oyuncu 2 Soyisimini girsin");
            string o2sname = Console.ReadLine();
            Console.WriteLine("Oyuncu 2 yaşını girsin");
            int o2yas = Convert.ToInt32(Console.ReadLine());


            Torba torba = new Torba();
            torba.TasOlustur();
            Sozluk sozluk = new Sozluk();

            Tahta tahta = new Tahta();
            tahta.TahtaÇiz();
            tahta.Yazdır();
            Console.ReadLine();

            Oyuncular o1 = new Oyuncular(o1name, o1sname, o1yas, torba.Cek(7));
            Oyuncular o2 = new Oyuncular(o2name, o2sname, o2yas, torba.Cek(7));

            while (torba.Kalan > 0 || o1.El.Count > 0 || o2.El.Count > 0)
            {
                OyuncuOynat(o1, torba, sozluk , tahta);
                if (torba.Kalan == 0 && o1.El.Count == 0 && o2.El.Count == 0)
                    break;

                OyuncuOynat(o2, torba, sozluk, tahta);
                if (torba.Kalan == 0 && o1.El.Count == 0 && o2.El.Count == 0)
                    break;
            }

            Console.WriteLine("------------------------------");
            Console.WriteLine("Taşlar Tükendi, Oyun Bitmiştir!");
            Console.WriteLine("------------------------------");

            
            Console.WriteLine($"{o1.Isim} {o1.Soyisim} puanı: {o1.Puan}");
            Console.WriteLine($"{o2.Isim} {o2.Soyisim} puanı: {o2.Puan}");

            Console.WriteLine("------------------------------");

            
            if (o1.Puan > o2.Puan)
            {
                Console.WriteLine($"Oyunun Kazananı: {o1.Isim} {o1.Soyisim} ({o1.Puan} Puan)");
            }
            else if (o2.Puan > o1.Puan)
            {
                Console.WriteLine($"Oyunun Kazananı: {o2.Isim} {o2.Soyisim} ({o2.Puan} Puan)");
            }
            else
            {
                Console.WriteLine("Her iki oyuncuda eşit puana sahip oyun berabere.");
            }

            
            Console.ReadLine();
        }
    




        static void OyuncuOynat(Oyuncular oyuncular, Torba torba, Sozluk sozluk , Tahta tahta )
        {

            if (oyuncular.El.Count == 0)
            {
                Console.WriteLine($"{oyuncular.Isim}'ın elinde taş kalmadı, tur geçiliyor.");
                return;
            }

            while (true)
            {
                oyuncular.ElAc();
                Console.WriteLine($"{oyuncular.Isim}, bir kelime giriniz (PAS yazarak pas geçebilirsin):");
                string kelime = Console.ReadLine().ToUpper();

                char yon;
                int x;
                int y;


                if (kelime == "PAS")
                {
                    Console.WriteLine(" Tur pas geçildi.");
                    return;
                }

                if (!oyuncular.HarfVarMı(kelime))
                {
                    Console.WriteLine("Bu kelimeyi oluşturmak için yeterli harfe sahip değilsin");
                    continue;
                }

                if (!sozluk.SozlukteVarmı(kelime))
                {
                    Console.WriteLine("Bu kelime sözlükte bulunmuyor");
                    continue;
                }

                bool key1 = false;
                do
                {
                    Console.WriteLine("Kelimenizin Başlayacağı X(Yatay Sütun) Koordinatını Giriniz(0-14):");
                    x = Convert.ToInt32(Console.ReadLine());
                    if (x <= 14 && x >= 0)
                    {
                        key1 = true;
                    }
                    else
                    {
                        Console.WriteLine("Geçersiz X koordinatı girdiniz! Lütfen (0-14) arası giriş yapın");
                        key1 = false;
                    }
                } while (!key1);


                bool key2 = false;
                do
                {
                    Console.WriteLine("Kelimenizin Başlayacağı Y(Dikey Sütun) Koordinatını Giriniz(0-14):");
                    y = Convert.ToInt32(Console.ReadLine());
                    if (y <= 14 && y >= 0)
                    {
                        key2 = true;
                    }
                    else
                    {
                        Console.WriteLine("Geçersiz Y koordinatı girdiniz! Lütfen (0-14) arası giriş yapın");
                        key2 = false;
                    }
                } while (!key2);


                Koordinat koordinat = new Koordinat();
                koordinat.KoordinatAta(x, y);
                
                Console.WriteLine("Yatay/Dikey hangi şekilde yazacaksınız(Y)/(D) Kullan");
                yon = char.Parse(Console.ReadLine().ToUpper());

                if (!tahta.KelimeDiz(kelime, koordinat, yon))
                {
                    Console.WriteLine("Kelime Tahtaya Yerleştirilemedi Doğru Koordinatlar girdiğinizi emin olun.");
                    continue;
                }
                
                if(!tahta.KelimeDiz(kelime , koordinat, yon))
                {
                    Console.WriteLine("Kelimeniz Tahtadaki Kelimelerle Temas Etmelidir");
                    continue;
                }

                tahta.Yazdır();
                int puan = tahta.KelimePuanHesapla(kelime, koordinat, yon, torba);
                oyuncular.Puan += puan;
                Console.WriteLine("Kelime Başarıyla Yerleştirildi");
                Console.WriteLine("Alınan Puan:" + " " + puan);
                Console.WriteLine("Toplam Puanınız:" + " " + oyuncular.Puan);
                oyuncular.HarfleriKullan(kelime);
                Console.WriteLine("Kelime Yazıldı");
                oyuncular.YeniTasEkle(torba.Cek(kelime.Length));
                break;

            }

            oyuncular.ElAc();
            Console.WriteLine("Kalan Taş: " + torba.Kalan);
            Console.WriteLine("----------------------------------------------");
        }
    }

    }


