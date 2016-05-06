using System;

namespace Krowiorsch.MediaAccount
{
    public class Program
    {

        public static void Main()
        {
            MediaAccountClient client = new IntializeClient().GetClient();



            Console.Read();
            Console.WriteLine();
        }
    }
}
