using System;
using Soru3.YirmibirOyunu;

namespace Soru3.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("21 Oyunununa Hoşgeldiniz");
            Console.WriteLine("Lütfen Oyuncu Sayısını Giriniz");
            if (int.TryParse(Console.ReadLine(), out var oyuncuSayisi))
            {
                var oyun = new Oyun(oyuncuSayisi);
                while (!oyun.OyunBittiMi())
                {
                    foreach (var aktifOyuncu in oyun.AktifOyuncular())
                    {
                        Console.WriteLine("_____________________________");
                        Console.WriteLine($"{aktifOyuncu.Ad} elinizdeki kartlar {aktifOyuncu.KartlariListele()}");
                        Console.WriteLine($"Kart çekmek istiyor musunuz? (e/h)");
                        string yanit = "";
                        do
                        {
                            yanit = Console.ReadLine();
                            if (yanit == "e" || yanit == "h")
                            {
                                if (yanit == "e")
                                {
                                    oyun.KartEkle(aktifOyuncu);
                                    Console.WriteLine($"{aktifOyuncu.Ad} elinizdeki kartlar {aktifOyuncu.KartlariListele()}");
                                }
                                else
                                {
                                    aktifOyuncu.KartAlmayiBırak();
                                }
                            }
                            else
                            {
                                Console.Write("Lütfen geçerli bi yanıt giriniz");
                            }
                        } while (yanit != "e" && yanit != "h");
                    }
                }
                Console.WriteLine("-----------SONUC-----------");
                Console.WriteLine(oyun.Sonuclar());
                Console.WriteLine("----------------------");
            }
            else
            {
                Console.WriteLine("Oyuncu sayısı bir tam sayı değeri olmalı");
            }
        }
    }
}