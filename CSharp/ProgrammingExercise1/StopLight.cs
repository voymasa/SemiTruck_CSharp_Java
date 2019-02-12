using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Author: Aaron Voymas
/// Date: 1/29/2019
/// This is a Stoplight object with methods for producing signal changes.
/// </summary>
namespace ProgrammingExercise1
{
    class StopLight
    {
        // Variables for controlling the Stoplight signal state
        public StoplightStates StoplightState;
        public enum StoplightStates { Green = 1,
                                     Yellow,
                                     Red,
                                     LeftTurnGreen };

        //Constructor
        public StopLight()
        {
            StoplightState = StoplightStates.Green;
        }

        // Method to change the signal state
        public void ChangeSignal()
        {
            int newSignal = RollForSignal(); // Randomly determine the new signal
            StoplightStates newState = (StoplightStates)newSignal; // convert from int to enumtype
            Console.WriteLine("Signal Changed to: " + newState.ToString());
            StoplightState = newState;
        }

        // Revert the signal state to Green
        public void ResetSignal()
        {
            Console.WriteLine("Light reverts to green...");
            StoplightState = StoplightStates.Green;
        }

        // Methodized random signal generation
        private int RollForSignal()
        {
            Random signalChange = new Random(Guid.NewGuid().GetHashCode()); // used for determining the next signal to make
            return (signalChange.Next(0, 65535) % (Enum.GetNames(typeof(StoplightStates)).Length) + 1); // Generate a value between 0 and the total number of StopLightStates
        }
    }
}
