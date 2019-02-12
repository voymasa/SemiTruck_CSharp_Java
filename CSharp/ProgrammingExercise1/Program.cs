using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Author: Aaron Voymas
/// Date: 1/29/2019
/// This program runs a short simulation of a Semi-Truck responding to a series of signal changes from a stoplight.
/// </summary>
namespace ProgrammingExercise1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create Semi Truck and Traffic Control Device objects
            SemiTruck st1 = new SemiTruck();
            StopLight sl1 = new StopLight();

            // Variables for controlling the "game"
            int travelDistance = 10; // number of signal changes and responses

            //Initiate TCD signal and ST movement with outputs to console
            for(int i = 0; i < travelDistance; i++)
            {
                Console.WriteLine("Mile " + i);
                // Change the signal
                sl1.ChangeSignal();
                // Notify the semi truck of the signal
                st1.ProcessSignal(sl1.StoplightState);
                // Reset the signal
                sl1.ResetSignal();
                //Process that it is green again
                st1.ProcessSignal(sl1.StoplightState);
            }

            Console.ReadKey();
        }
    }
}
