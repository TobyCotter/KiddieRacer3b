using UnityEngine;
using System.Collections;

//This class stores the lane position of each car
//Cars can query this script to get the lane position of other cars


public class CarPositionManager : MonoBehaviour {
	//Variables
	private int[] allPlayersLanePos = new int[4];							//Holds lane position of all 4 players
	
	void Start () {
	
	}//End
	

	void Update () {
	
	}//End


	public void ReportMyLanePos(int playerIdentity, int playerLanePos){
		allPlayersLanePos[playerIdentity-1] = playerLanePos;				//Ex:  playerIdentity = 4, playerLanePos = 2...array playerLanePos[3] will now = 2
	}//End


	public int[] ReturnEveryonesLanePos(){
		return allPlayersLanePos;
	}

}//End Class
