package exercise2;
import java.util.Random;

/**
 * 
 * @author Aaron Voymas
 * @since 1/29/2019
 * This is a Stoplight object with methods for producing signal changes.
 */
public class Stoplight {
	// Variables for controlling the Stoplight signal state
    public StoplightStates StoplightState;
    public enum StoplightStates { Green,
                                 Yellow,
                                 Red,
                                 LeftTurnGreen };

    //Constructor
    public Stoplight()
    {
        StoplightState = StoplightStates.Green;
    }

    // Method to change the signal state
    public void ChangeSignal()
    {
        int newSignal = RollForSignal(); // Randomly determine the new signal
        StoplightStates newState = StoplightStates.values()[newSignal]; // convert from int to enumtype
        System.out.println("Signal Changed to: " + newState.name());
        StoplightState = newState;
    }

    // Revert the signal state to Green
    public void ResetSignal()
    {
    	System.out.println("Light reverts to green...");
        StoplightState = StoplightStates.Green;
    }

    // Methodized random signal generation
    private int RollForSignal()
    {
        Random signalChange = new Random(); // used for determining the next signal to make
        return signalChange.nextInt((65535 % StoplightStates.values().length) + 1) ; // Generate a value between 0 and the total number of StopLightStates
    }
}
