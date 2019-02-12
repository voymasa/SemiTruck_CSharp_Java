package exercise2;
/**
 * 
 * @author Aaron Voymas
 * @since 1/29/2019
 * This abstract class contains the basic methods and attributes and members common to all vehicles
 */
public abstract class AVehicle {

    // Variable for response to action attempt
    public Boolean actionSuccess;
	
	public abstract Boolean moveForward(Stoplight.StoplightStates stoplightStates);

	public abstract Boolean turnRight(Stoplight.StoplightStates stoplightState);

	public abstract Boolean turnLeft(Stoplight.StoplightStates stoplightState);
}
