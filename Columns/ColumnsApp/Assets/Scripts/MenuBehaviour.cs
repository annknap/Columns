using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuBehaviour : MonoBehaviour {

    public Button singleRowButton;
    public Button doubleRowButton;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void onSingleRowButton()
    {
        //Debug.Log("1");
        SceneManager.LoadScene("SingleRowScene");
    }

    public void onDoubleRowButton()
    {
        //Debug.Log("2");
        SceneManager.LoadScene("DoubleRowScene");
    }
}
