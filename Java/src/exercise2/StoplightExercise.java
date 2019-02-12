package exercise2;
import java.io.IOException;

/**
 * 
 * @author Aaron Voymas
 * @since 1/29/2019
 * This program runs a short simulation of a Semi-Truck responding to a series of signal changes from a stoplight.
 */
public class StoplightExercise {

	public static void main(String[] args) {
		//Create Semi Truck and Traffic Control Device objects
        SemiTruck st1 = new SemiTruck();
        Stoplight sl1 = new Stoplight();
        SUV suv1 = new SUV();

        // Variables for controlling the "game"
        int travelDistance = 10; // number of signal changes and responses

        //Initiate TCD signal and ST movement with outputs to console
        for(int i = 0; i < travelDistance; i++)
        {
            System.out.println("Mile " + i);
            // Change the signal
            sl1.ChangeSignal();
            // Notify the semi truck of the signal
            st1.ProcessSignal(sl1.StoplightState);
            suv1.ProcessSignal(sl1.StoplightState);
            // Reset the signal
            sl1.ResetSignal();
            //Process that it is green again
            st1.ProcessSignal(sl1.StoplightState);
            suv1.ProcessSignal(sl1.StoplightState);
        }
        
        try {
			System.in.read();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

}
