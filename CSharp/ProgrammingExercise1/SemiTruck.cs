using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Author: Aaron Voymas
/// Date: 1/29/2019
/// This is a Semitruck object with methods for responding to signals.
/// This implements the IVehicle inteface for access to the basic movements that a vehicle can make.
/// </summary>
namespace ProgrammingExercise1
{
    class SemiTruck : IVehicle
    {
        // Variables to control movement state
        private MovementStates movementState;
        private MovementStates oldMoveState; // used for comparison purposes
        private enum MovementStates { MoveForward = 1,
                                TurnRight,
                                TurnLeft,
                                JackKnifeStop };
        // Variable for response to action attempt
        public bool actionSuccess;

        // Constructor
        public SemiTruck() => movementState = MovementStates.MoveForward;

        // Methods to process signals
        public void ProcessSignal(StopLight.StoplightStates signalState)
        {
            actionSuccess = false;
            switch(signalState)
            {
                case StopLight.StoplightStates.Green: Console.WriteLine("Greenlight, GO!");
                    while (!actionSuccess) // Checks if the chosen action succeeded
                    {
                        oldMoveState = movementState;
                        ChangeMovement();
                        // call appropriate command
                        switch(movementState)
                        {
                            case MovementStates.MoveForward: MoveForward(StopLight.StoplightStates.Green);
                                break;
                            case MovementStates.TurnRight: TurnRight(StopLight.StoplightStates.Green);
                                break;
                            default: Console.WriteLine("Action not available, here...Try another");
                                actionSuccess = false;
                                break;
                        }
                    }
                    break;
                case StopLight.StoplightStates.LeftTurnGreen: Console.WriteLine("Left-Turn Green...");
                    while (!actionSuccess)
                    {
                        oldMoveState = movementState;
                        ChangeMovement();
                        // call appropriate command
                        switch (movementState)
                        {
                            case MovementStates.TurnLeft:
                                TurnLeft(StopLight.StoplightStates.LeftTurnGreen);
                                break;
                            default:
                                Console.WriteLine("Action not available, here...Try another");
                                actionSuccess = false;
                                break;
                        }
                    }
                    break;
                case StopLight.StoplightStates.Yellow: Console.WriteLine("Clear the intersection, light is yellow.");
                    while (!actionSuccess)
                    {
                        oldMoveState = movementState;
                        ChangeMovement();
                        // call appropriate command
                        switch (movementState)
                        {
                            case MovementStates.MoveForward:
                                MoveForward(StopLight.StoplightStates.Yellow);
                                break;
                            case MovementStates.TurnRight:
                                TurnRight(StopLight.StoplightStates.Yellow);
                                break;
                            case MovementStates.JackKnifeStop:
                                Stop(StopLight.StoplightStates.Yellow);
                                break;
                            default:
                                Console.WriteLine("Action not available, here...Try another");
                                actionSuccess = false;
                                break;
                        }
                    }
                    break;
                case StopLight.StoplightStates.Red: Console.WriteLine("You should be stopping now...");
                    while (!actionSuccess)
                    {
                        oldMoveState = movementState;
                        ChangeMovement();
                        // call appropriate command
                        switch (movementState)
                        {
                            case MovementStates.JackKnifeStop:
                                Stop(StopLight.StoplightStates.Red);
                                break;
                            default:
                                Console.WriteLine("Action not available, here...Try another");
                                actionSuccess = false;
                                break;
                        }
                    }
                    break;
                default: Console.WriteLine("Unable to process that signal..."); break;
            }
        }

        // Randomly change the movement state
        private void ChangeMovement()
        {
            int newMove = RollForMovement(); // Randomly determine the new movement
            MovementStates newState = (MovementStates)newMove; // convert from int to enumtype
            Console.WriteLine("Movement Changed to: " + newState.ToString());
            movementState = newState;
        }

        // Methodized random signal generation
        private int RollForMovement()
        {
            Random moveChange = new Random(Guid.NewGuid().GetHashCode()); // used for determining the next signal to make
            return moveChange.Next(0, 65535) % (Enum.GetNames(typeof(MovementStates)).Length) + 1; // Generate a value between 1 and the total number of states + 1
        }

        // Methods to control movement state
        public bool MoveForward(StopLight.StoplightStates state)
        {
            if (oldMoveState.Equals(MovementStates.MoveForward))
            {
                Console.WriteLine("Error! Already moving forward!");
                actionSuccess = false;
            }
            else
            {
                movementState = MovementStates.MoveForward;
                Console.WriteLine("Driving forward.");
                actionSuccess = true;
            }
            return actionSuccess;
        }

        public bool Stop(StopLight.StoplightStates state)
        {
            if(oldMoveState.Equals(MovementStates.JackKnifeStop))
            {
                Console.WriteLine("Must move forward...");
                actionSuccess = false;
            }
            else
            {
                movementState = MovementStates.JackKnifeStop;
                Console.WriteLine("Jack-Knifing to a stop...");
                actionSuccess = true;
            }
            return actionSuccess;
        }

        public bool TurnLeft(StopLight.StoplightStates state)
        {
            if(!state.Equals(StopLight.StoplightStates.LeftTurnGreen))
            {
                Console.WriteLine("Can't Turn Left until Left-Turn Green.");
                actionSuccess = false;
            }
            else if (oldMoveState.Equals(MovementStates.JackKnifeStop))
            {
                Console.WriteLine("Can only move forward from Jack-Knifed to a Stop...");
                actionSuccess = false;
            }
            else
            {
                movementState = MovementStates.TurnLeft;
                Console.WriteLine("Turning Left...");
                actionSuccess = true;
            }
            return actionSuccess;
        }

        public bool TurnRight(StopLight.StoplightStates state)
        {
            if(oldMoveState.Equals(MovementStates.JackKnifeStop))
            {
                Console.WriteLine("Can only move forward from Jack-Knifed to a Stop...");
                actionSuccess = false;
            }
            else
            {
                movementState = MovementStates.TurnRight;
                Console.WriteLine("Turning right...");
                actionSuccess = true;
            }
            return actionSuccess;
        }
    }
}
