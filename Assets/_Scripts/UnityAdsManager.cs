using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class UnityAdsManager : MonoBehaviour {
	//Variables
	public static int PLAYER_COINS;
	public GameObject getSpeedButton;
	private CongratsManager congratsManager;


	void Awake () {
		PLAYER_COINS = PlayerPrefs.GetInt("PLAYER_COINS");
		congratsManager = GameObject.FindObjectOfType<CongratsManager>();
		DisableGetSpeedButton();
		Debug.Log("PLAYER_COINS @ start: " + PLAYER_COINS);
	}//End
	

	void Update () {
		
	}//End


	public void DecrementPlayerCoinCount(){
		PLAYER_COINS--;
		PlayerPrefs.SetInt("PLAYER_COINS", PLAYER_COINS);
	}


	public void ShowRewardedAd(){
	    if (Advertisement.IsReady("rewardedVideo")){
	        var options = new ShowOptions { resultCallback = HandleShowResult };
	      	Advertisement.Show("rewardedVideo", options);
	    }
	}//End


	private void HandleShowResult(ShowResult result){
	    switch (result){
	      case ShowResult.Finished:
	      	//User watched entire video, give them reward
	        PLAYER_COINS = PLAYER_COINS + 3;							//We are adding three coins so the player gets 3 games of speed bursts
	        PlayerPrefs.SetInt("PLAYER_COINS", PLAYER_COINS);
	       	congratsManager.EnableCongratsImage();
	        break;
	      case ShowResult.Skipped:
	        Debug.Log("The ad was skipped before reaching the end.");
	        break;
	      case ShowResult.Failed:
	        Debug.LogError("The ad failed to be shown.");
	        break;
	    }
	}


	public void EnableGetSpeedButton(){
		getSpeedButton.SetActive(true);
	}//End


	public void DisableGetSpeedButton(){
		getSpeedButton.SetActive(false);
	}//End
}//End class





/*
public class UnityAdsExample : MonoBehaviour
{
  public void ShowRewardedAd()
  {
    if (Advertisement.IsReady("rewardedVideoZone"))
    {
      var options = new ShowOptions { resultCallback = HandleShowResult };
      Advertisement.Show("rewardedVideoZone", options);
    }
  }

  private void HandleShowResult(ShowResult result)
  {
    switch (result)
    {
      case ShowResult.Finished:
        Debug.Log("The ad was successfully shown.");
        //
        // YOUR CODE TO REWARD THE GAMER
        // Give coins etc.
        break;
      case ShowResult.Skipped:
        Debug.Log("The ad was skipped before reaching the end.");
        break;
      case ShowResult.Failed:
        Debug.LogError("The ad failed to be shown.");
        break;
    }
  }
}
*/
