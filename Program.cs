// See https://aka.ms/new-console-template for more information
using DesignPatterns.solid;

namespace DesignPatterns
{
    public class Start
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Here all it starts - From Main");

            //Call to the solid - Specifically for SRP - in SOLID 
            //Solid.ExecutionSRP();

            Console.WriteLine("\n OCP starts \n");
            OpenClosedExample.ExecutionOCP();
        }
    }
}
