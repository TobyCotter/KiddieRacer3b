using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class RestartScene : MonoBehaviour {
	//Variables



	void Start () {
		Invoke("ReloadOrigScene", 0.5f);
	}//End
	
	// Update is called once per frame
	void Update () {
	
	}//End


	private void ReloadOrigScene(){
		SceneManager.LoadScene(0);
	}//End
}//End Class
