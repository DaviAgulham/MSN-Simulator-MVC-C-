using System;
 

namespace ConsoleApp_p2
{
    class Program
    {
        static void Main(string[] args)
        {
            Controladoor.Controlador control = new Controladoor.Controlador();
            control.Funcionar();

            Console.ReadLine();
        }
    }
}
