using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Atm_system
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // User's password
            int password = 35921;
            int bakiye = 250;
            long tel = 05356723405;
            long tc = 12950485554;

            string text = @"
                 ____       _______ _____ _  __  ____          _   _ _  __
                |  _ \   /\|__   __|_   _| |/ / |  _ \   /\   | \ | | |/ /
                | |_) | /  \  | |    | | | ' /  | |_) | /  \  |  \| | ' / 
                |  _ < / /\ \ | |    | | |  <   |  _ < / /\ \ | . ` |  <  
                | |_) / ____ \| |   _| |_| . \  | |_) / ____ \| |\  | . \ 
                |____/_/    \_\_|  |_____|_|\_\ |____/_/    \_\_| \_|_|\_\" + "\n";

            // Seçim 1 ana menü
            string s1_main_menü = @"
            Para Çekmek için    1
            Para yatırmak için  2
            Para Transferleri   3
            Eğitim Ödemeleri    4
            Ödemeler            5
            Bilgi Güncelleme    6" + "\n>>>>";
            // Para yatırma menüsü

            string s2_deposit = @"
            Kredi Kartına   1
            Kendi Hesabınıza yatırmak için  2
            Ana Menü        9
            Çıkmak için     0" + "\n>>>>";
            // Para Transferi menüsü
            string s3_transfer = @"
                Başka Hesaba EFT    1
                Başka Hesaba Havale 2
                
               " + "\n>>>>";
            string s5_fatura = @"
            Elektrik Faturası       1
            Telefon Faturası        2
            İnternet faturası       3
            Su Faturası             4
            OGS Ödemeleri           5    
            
            ";
            string s2_anamenü = @"
            CepBank Para Çekmek 1
            Para yatırmak için  2
            Kredi Kartı Ödeme   3
            Eğitim Ödemeleri    4
            Ödemeler     
            
            "+"\n>>>>";


        atm_page:
            Console.WriteLine(text);
            Console.Write("Lütfen yapmak istediğiniz işlemi seçiniz\n1 --> Kartla işlem\n2 --> Kartsız işlem\n0 --> Çıkış\n>>>>");
            int atm = Convert.ToInt32(Console.ReadLine());
            if (atm == 1)
            {
                int have = 0;
                while (have <= 3)
                {
                    Console.WriteLine("Lütfen şifrenizi giriniz\n>>>>");
                    int user_password = Convert.ToInt32(Console.ReadLine());
                    if (user_password == password) { break; }
                    else if (have == 3) { Console.WriteLine("Hakkınız kalmadı ana menüye gidiyorsunuz"); goto atm_page; }
                    else { have++; }
                }
            main_page:
                Console.WriteLine(s1_main_menü); // Ana menü
                int user = Convert.ToInt32(Console.ReadLine());
                if (user == 1)// Para Çekme
                {
                withdraw_money: // Para çekme işlemi
                    Console.Write("Lütfen çekmek istediğiniz tutarı giriniz\n>>>>");
                    int user01 = Convert.ToInt32(Console.ReadLine());//Kişinin atmden istediği para
                    if (user01 > bakiye)
                    { // Eğer fazla bir tutar girerse
                        Console.Write("Malesef bukardar bakiyeniz yok");
                        goto withdraw_money;
                    }
                    else
                    {
                        bakiye = bakiye - user01;
                        Console.WriteLine("Çekme işlemi başarılı bir şekilde uygulandı şuan ki kalan bakiyeniz\n" + bakiye + "TL");
                        Console.WriteLine("Başka bir işlem yapmak istermisiniz ? Y/N");
                        char yn = Convert.ToChar(Console.ReadLine().ToLower());
                        if (yn == 'y') { goto main_page; }
                        Environment.Exit('n');
                        // Para çekme işlemi bitti
                    }

                } // Para Çekme
                else if (user == 2)// Para yatırma
                {
                    Console.WriteLine(s2_deposit);
                    int user01 = Convert.ToInt32(Console.ReadLine());
                deposit:
                    if (user01 == 1)//Karta para yatırma
                    {
                        Console.Write("Lütfen 12 Haneli kart numarasını giriniz\n>>>>");
                        long user02 = Convert.ToInt64(Console.ReadLine()); // Paranın yatırılacağı kredi kartı numarası
                        Console.Write("Göndermek istediğniz parayı yazın\n>>>>");
                        int user03 = Convert.ToInt32(Console.ReadLine()); // Kredi kartına yatıcak olan para
                        if (user03 > bakiye) { Console.WriteLine("Bakiyeniz yetersiz ana menüye gidiyorsunuz\n"); goto deposit; }
                        else
                        {
                            bakiye = bakiye - user03;
                            Console.WriteLine("İşleminiz başarı ile gerçekleştirildi\nŞuan ki güncel bakiyeniz " + bakiye);
                            Console.WriteLine("Başka bir işlem yapmak istermisiniz ? Y/N");
                            char yn = Convert.ToChar(Console.ReadLine().ToLower());
                            if (yn == 'y') { goto main_page; }
                            Environment.Exit('n');

                        }

                    }
                    else if (user01 == 2) // Hesaba para yatırma 
                    {
                        Console.Write("Lütfen Yatırmak istediğniz parayı giriniz\n>>>>");
                        int user02 = Convert.ToInt32(Console.ReadLine());// Kullancı istediği tutarı giricek
                        bakiye = bakiye += user02;
                        Console.WriteLine("Paranız hesabınıza başarılı bir şekilde eklenmiştir\nGüncel bakiyeniz {0}", bakiye);
                        Console.WriteLine("Başka bir işlem yapmak istermisiniz ? Y/N");
                        char yn = Convert.ToChar(Console.ReadLine().ToLower());
                        if (yn == 'y') { goto main_page; }
                        Environment.Exit('n');
                    }
                    else if (user01 == 9) { goto main_page; }
                    else if (user01 == 0) { Environment.Exit(0); }

                }//Para yatırma
                else if (user == 3)// Para transferi
                {
                    Console.Write(s3_transfer);
                    int user01 = Convert.ToInt32(Console.ReadLine());
                    if (user01 == 1) //EFT YÖNTEMİ (BUG FİX UNUTMA)
                    {
                    eft:
                        Console.Write("Lütfen EFT numarasını giriniz\n>>>>");
                        string user02 = Console.ReadLine();
                        if(user02.Length < 12 ||  user02.Length > 12)
                        {
                            Console.WriteLine("EFT Numarası 12 haneden oluşur");
                            goto eft;
                        }
                        string tr = "TR"; // EFT Numarasının başına eklenecek olan ifade
                        user02 = tr + user02;
                        string check = user02.ToString();
                        if (check.Length > 12 || check.Length < 12)
                        {
                            Console.WriteLine("Lütfen hesap numarasını doğru giriniz");
                            goto eft;
                        }
                        Console.WriteLine("Girdiğiniz EFT numarası {0}",user02);
                        //Hesap numarası alındıktan sonra transfer edilicek para
                        Console.WriteLine("Lütfen yatırılacak parayı giriniz\n>>>>");
                        int user03 = Convert.ToInt32(Console.ReadLine());
                        if(user03 > bakiye) { goto eft; }
                        else
                        {
                            bakiye = bakiye - user03;
                            Console.WriteLine("İşleminiz başarı ile gerçekleştirildi\nGüncel bakiyeniz {0}TL",bakiye);
                            Console.WriteLine("Başka bir işlem yapmak istermisiniz ? Y/N");
                            char yn = Convert.ToChar(Console.ReadLine().ToLower());
                            if (yn == 'y') { goto main_page; }
                            Environment.Exit('n');
                        }


                    }
                    else if(user01 == 2) // Havale (Bug fix numara kontrol + ana menüye dönüş)
                    {
                    havale:
                        Console.Write("Lütfen hesap numarasını giriniz\n>>>>");
                        long user02 = Convert.ToInt64(Console.ReadLine());
                        string check = user02.ToString();
                        if(check.Length > 11 || check.Length < 11)
                        {
                            Console.WriteLine("Lütfen hesap numarasını doğru giriniz");
                            goto havale;
                        }
                        
                     
                        Console.WriteLine("Lütfen yatırılacak parayı giriniz\n>>>>");
                        int user03 = Convert.ToInt32(Console.ReadLine());
                        if (user03 > bakiye) { goto havale ; }
                        else
                        {
                            bakiye = bakiye - user03;
                            Console.WriteLine("İşleminiz başarı ile gerçekleştirildi\nGüncel bakiyeniz {0}TL", bakiye);
                            Console.WriteLine("Başka bir işlem yapmak istermisiniz ? Y/N");
                            char yn = Convert.ToChar(Console.ReadLine().ToLower());
                            if (yn == 'y') { goto main_page; }
                            Environment.Exit('n');

                        }

                    }

                }// Para transferi
                else if (user == 4) { Console.WriteLine("Malesef bu bölüm arızalı"); goto main_page; }
                else if (user == 5) {//Fatura ödeme
                    Console.WriteLine(s5_fatura);
                    int user01 = Convert.ToInt32(Console.ReadLine());
                elek_fatura:
                    if (user01 == 1)
                    {
                        Console.WriteLine("Lütfen ödenecek tutarı giriniz\n>>>>");
                        int user02 = Convert.ToInt32(Console.ReadLine());
                        if (user02 > bakiye)
                        {
                            Console.WriteLine("Malesef bakiyeniz yetersiz");
                            goto elek_fatura;
                        }
                        else
                        {
                            bakiye = bakiye - user02;
                            Console.WriteLine("İşleminiz başarılı bir şekilde uygulandı şuan ki kalan bakiyeniz\n" + bakiye + "TL");
                            Console.WriteLine("Başka bir işlem yapmak istermisiniz ? Y/N");
                            char yn = Convert.ToChar(Console.ReadLine().ToLower());
                            if (yn == 'y') { goto main_page; }
                            Environment.Exit('n');
                        }

                    }
                    else if (user01 == 2)
                    {
                    tel_fatura:
                        Console.WriteLine("Lütfen ödenecek tutarı giriniz\n>>>>");
                        int user02 = Convert.ToInt32(Console.ReadLine());
                        if (user02 > bakiye)
                        {
                            Console.WriteLine("Malesef bakiyeniz yetersiz");
                            goto tel_fatura;
                        }
                        else
                        {
                            bakiye = bakiye - user02;
                            Console.WriteLine("İşleminiz başarılı bir şekilde uygulandı şuan ki kalan bakiyeniz\n" + bakiye + "TL");
                            Console.WriteLine("Başka bir işlem yapmak istermisiniz ? Y/N");
                            char yn = Convert.ToChar(Console.ReadLine().ToLower());
                            if (yn == 'y') { goto main_page; }
                            Environment.Exit('n');
                        }

                    }
                    else if (user == 3)
                    {
                    net_fatura:
                        Console.WriteLine("Lütfen ödenecek tutarı giriniz\n>>>>");
                        int user02 = Convert.ToInt32(Console.ReadLine());
                        if (user02 > bakiye)
                        {
                            Console.WriteLine("Malesef bakiyeniz yetersiz");
                            goto net_fatura;
                        }
                        else
                        {
                            bakiye = bakiye - user02;
                            Console.WriteLine("İşleminiz başarılı bir şekilde uygulandı şuan ki kalan bakiyeniz\n" + bakiye + "TL");
                            Console.WriteLine("Başka bir işlem yapmak istermisiniz ? Y/N");
                            char yn = Convert.ToChar(Console.ReadLine().ToLower());
                            if (yn == 'y') { goto main_page; }
                            Environment.Exit('n');
                        }


                    }
                    else if (user == 4)
                    {
                    su_fatura:
                        Console.WriteLine("Lütfen ödenecek tutarı giriniz\n>>>>");
                        int user02 = Convert.ToInt32(Console.ReadLine());
                        if (user02 > bakiye)
                        {
                            Console.WriteLine("Malesef bakiyeniz yetersiz");
                            goto su_fatura;
                        }
                        else
                        {
                            bakiye = bakiye - user02;
                            Console.WriteLine("İşleminiz başarılı bir şekilde uygulandı şuan ki kalan bakiyeniz\n" + bakiye + "TL");
                            Console.WriteLine("Başka bir işlem yapmak istermisiniz ? Y/N");
                            char yn = Convert.ToChar(Console.ReadLine().ToLower());
                            if (yn == 'y') { goto main_page; }
                            Environment.Exit('n');

                        }

                    }
                    else if (user == 5)
                    {
                    ogs:
                        Console.WriteLine("Lütfen ödenecek tutarı giriniz\n>>>>");
                        int user02 = Convert.ToInt32(Console.ReadLine());
                        if (user02 > bakiye)
                        {
                            Console.WriteLine("Malesef bakiyeniz yetersiz");
                            goto ogs;
                        }
                        else
                        {
                            bakiye = bakiye - user02;
                            Console.WriteLine("İşleminiz başarılı bir şekilde uygulandı şuan ki kalan bakiyeniz\n" + bakiye + "TL");
                            Console.WriteLine("Başka bir işlem yapmak istermisiniz ? Y/N");
                            char yn = Convert.ToChar(Console.ReadLine().ToLower());
                            if (yn == 'y') { goto main_page; }
                            Environment.Exit('n');
                        }
                    }
                }//Fatura ödemeleri
                else if (user == 6) // Şifre değiştirme Ana menüye dönme.
                {
                    Console.Write("Lütfen Yeni şifrenizi giriniz\n>>>>");
                    int user01 = Convert.ToInt32(Console.ReadLine());
                    password = user01;
                    Console.WriteLine("Başarılı şekilde değiştirildi!!");
                        goto atm_page ;

                }
            }
            else if (atm == 2) {
                Console.Write(s2_anamenü);
                int user = Convert.ToInt32(Console.ReadLine());
                if(user == 1)//CepBank Paraçekme
                {
                ceppara:
                    Console.Write("Lütfen Tc NO giriniz\n>>>>");
                    long user01 = Convert.ToInt64(Console.ReadLine());
                    Console.Write("Lütfen Tel no giriniz\n>>>>");
                    long user_tel = Convert.ToInt64(Console.ReadLine());
                    string check_tel = user_tel.ToString();
                    string check = user01.ToString();
                    int have = 0;
                    while (have <= 3)
                    {
                        Console.WriteLine("Lütfen tel giriniz\n>>>>");
                        if (user_tel == tel) { break; }
                        else if (have == 3) { Console.WriteLine("Hakkınız kalmadı ana menüye gidiyorsunuz"); goto ceppara; }
                        else { have++; }
                    }
                    if (check.Length > 12 || check.Length < 12)
                    {
                        Console.WriteLine("Lütfen Tc numarasını doğru giriniz");
                        goto ceppara;
                    }
                    if (check_tel.Length > 11 || check_tel.Length < 11)
                    {
                        Console.WriteLine("Lütfen Tel numarasını doğru giriniz");
                        goto ceppara;
                    }
                    else { if (user_tel == tel && user01 == tc) {
                            bakiye = bakiye + 1000;
                            Console.WriteLine("Şuanki güncel bakiyeniz {0}", bakiye);


                        } }
                  
                    


                }            

            } // kartsız işlemler
            else { Environment.Exit(0); }

            Console.ReadLine();
        }
    }
}
