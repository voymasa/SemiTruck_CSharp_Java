package exercise2;
import java.util.Random;

import exercise2.Stoplight.StoplightStates;
/**
 * 
 * @author Aaron Voymas
 * @since 1/29/2019
 * This is a Semitruck object with methods for responding to signals.
 * This extends the AVehicle class for access to the basic movements that a vehicle can make.
 */
public class SemiTruck extends AVehicle {
	// Variables to control movement state
    protected MovementStates movementState;
    protected MovementStates oldMoveState; // used for comparison purposes
    protected enum MovementStates { MoveForward,
                            TurnRight,
                            TurnLeft,
                            JackKnifeStop };
	
    public SemiTruck()
    {
    	movementState = MovementStates.MoveForward;
    }
                            
	// Methods to process signals
    public void ProcessSignal(Stoplight.StoplightStates signalState)
    {
        actionSuccess = false;
        switch(signalState)
        {
            case Green: System.out.println("Greenlight, GO!");
                while (!actionSuccess) // Checks if the chosen action succeeded
                {
                	oldMoveState = movementState;
                    changeMovement();
                    // call appropriate command
                    switch(movementState)
                    {
                        case MoveForward: moveForward(Stoplight.StoplightStates.Green);
                            break;
                        case TurnRight: turnRight(Stoplight.StoplightStates.Green);
                            break;
                        default: System.out.println("Action not available, here...Try another");
                            actionSuccess = false;
                            break;
                    }
                }
                break;
            case LeftTurnGreen: System.out.println("Left-Turn Green...");
                while (!actionSuccess)
                {
                    oldMoveState = movementState;
                    changeMovement();
                    // call appropriate command
                    switch (movementState)
                    {
                        case TurnLeft:
                            turnLeft(Stoplight.StoplightStates.LeftTurnGreen);
                            break;
                        default:
                        	System.out.println("Action not available, here...Try another");
                            actionSuccess = false;
                            break;
                    }
                }
                break;
            case Yellow: System.out.println("Clear the intersection, light is yellow.");
                while (!actionSuccess)
                {
                    oldMoveState = movementState;
                    changeMovement();
                    // call appropriate command
                    switch (movementState)
                    {
                        case MoveForward:
                            moveForward(Stoplight.StoplightStates.Yellow);
                            break;
                        case TurnRight:
                            turnRight(Stoplight.StoplightStates.Yellow);
                            break;
                        case JackKnifeStop:
                            stop(Stoplight.StoplightStates.Yellow);
                            break;
                        default:
                        	System.out.println("Action not available, here...Try another");
                            actionSuccess = false;
                            break;
                    }
                }
                break;
            case Red: System.out.println("You should be stopping now...");
                while (!actionSuccess)
                {
                    oldMoveState = movementState;
                    changeMovement();
                    // call appropriate command
                    switch (movementState)
                    {
                        case JackKnifeStop:
                            stop(Stoplight.StoplightStates.Red);
                            break;
                        default:
                        	System.out.println("Action not available, here...Try another");
                            actionSuccess = false;
                            break;
                    }
                }
                break;
            default: System.out.println("Unable to process that signal..."); break;
        }
    }

    // Randomly change the movement state
    private void changeMovement()
    {
        int newMove = rollForMovement(); // Randomly determine the new movement
        MovementStates newState = MovementStates.values()[newMove]; // convert from int to enumtype
        System.out.println("Semi-Truck Movement Changed to: " + newState.name());
        movementState = newState;
    }

    // Methodized random signal generation
    private int rollForMovement()
    {
        Random moveChange = new Random(); // used for determining the next signal to make
        return moveChange.nextInt((65535 % StoplightStates.values().length) + 1); // Generate a value between 1 and the total number of states + 1
    }

    // Methods to control movement state
    @Override
    public Boolean moveForward(Stoplight.StoplightStates state)
    {
        if (oldMoveState.equals(MovementStates.MoveForward))
        {
        	System.out.println("Error! Already moving forward!");
            actionSuccess = false;
        }
        else
        {
            movementState = MovementStates.MoveForward;
            System.out.println("Driving forward.");
            actionSuccess = true;
        }
        return actionSuccess;
    }

    public Boolean stop(Stoplight.StoplightStates state)
    {
        if(oldMoveState.equals(MovementStates.JackKnifeStop))
        {
        	System.out.println("Must move forward...");
            actionSuccess = false;
        }
        else
        {
            movementState = MovementStates.JackKnifeStop;
            System.out.println("Jack-Knifing to a stop...");
            actionSuccess = true;
        }
        return actionSuccess;
    }

    @Override
    public Boolean turnLeft(Stoplight.StoplightStates state)
    {
        if(!state.equals(Stoplight.StoplightStates.LeftTurnGreen))
        {
        	System.out.println("Can't Turn Left until Left-Turn Green.");
            actionSuccess = false;
        }
        else if (oldMoveState.equals(MovementStates.JackKnifeStop))
        {
            System.out.println("Can only move forward from Jack-Knifed to a Stop...");
            actionSuccess = false;
        }
        else
        {
            movementState = MovementStates.TurnLeft;
            System.out.println("Turning Left...");
            actionSuccess = true;
        }
        return actionSuccess;
    }

    @Override
    public Boolean turnRight(Stoplight.StoplightStates state)
    {
        if(oldMoveState.equals(MovementStates.JackKnifeStop))
        {
            System.out.println("Can only move forward from Jack-Knifed to a Stop...");
            actionSuccess = false;
        }
        else
        {
            movementState = MovementStates.TurnRight;
            System.out.println("Turning right...");
            actionSuccess = true;
        }
        return actionSuccess;
    }
}
