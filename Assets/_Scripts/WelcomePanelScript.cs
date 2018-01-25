using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class WelcomePanelScript : MonoBehaviour, IInputClickHandler, IInputHandler
{
    public App app;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnInputClicked(InputClickedEventData eventData)
    {
        // AirTap code goes here
        app.SetState(App.AppState.PodiumSetup);
    }
    public void OnInputDown(InputEventData eventData)
    { }

    public void OnInputUp(InputEventData eventData)
    { }
}
