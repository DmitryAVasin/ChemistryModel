using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class PodiumScript : MonoBehaviour, IInputClickHandler, IInputHandler
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
        if(app.IsCurrentCompoundCompletted())
        {
            app.SetNextComponent();
        }
    }
    public void OnInputDown(InputEventData eventData)
    { }

    public void OnInputUp(InputEventData eventData)
    { }
}
