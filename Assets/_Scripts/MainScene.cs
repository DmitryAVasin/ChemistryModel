using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class MainScene : MonoBehaviour, IInputClickHandler, IInputHandler
{

    public App app;
    public Camera mCamera;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(app.GetState() == App.AppState.CreateCompound)
        {
            app.CorrectPeriodicTablePosition(mCamera.transform.position);
        }
	}

    public void OnInputClicked(InputClickedEventData eventData)
    {
        // AirTap code goes here
    }
    public void OnInputDown(InputEventData eventData)
    { }

    public void OnInputUp(InputEventData eventData)
    { }
}
