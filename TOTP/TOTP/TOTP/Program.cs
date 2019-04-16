using System;

namespace TOTP
{
    class Program
    {
        static void Main(string[] args)
        {
            var key = "Hello";
            var counter = "GYY";
            for (int i = 0; i < 10; i++)
            {
                var otp = TOTPHelper.TOTP(key, 10);
                Console.WriteLine(otp);
                System.Threading.Thread.Sleep(3000);
            }
            Console.ReadKey();
            //nsole.WriteLine("Hello World!");
        }

    }
}
