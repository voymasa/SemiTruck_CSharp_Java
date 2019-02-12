using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Author: Aaron Voymas
/// Date: 1/29/2019
/// This inteface contains the basic movements common to all vehicles
/// </summary>
namespace ProgrammingExercise1
{
    interface IVehicle
    {
        bool MoveForward(StopLight.StoplightStates stoplightStates);

        bool TurnRight(StopLight.StoplightStates stoplightState);

        bool TurnLeft(StopLight.StoplightStates stoplightState);
    }
}
