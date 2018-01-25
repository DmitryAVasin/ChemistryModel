using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class MainScene : MonoBehaviour, IInputClickHandler, IInputHandler
{

    public enum AppState {
        Unknown = 0,
        Init,
        Welcome,
        PodiumSetup,
        CreateCompound,
        ViewCompound
    };

    protected AppState mAppState = AppState.Unknown;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetState(AppState state)
    {
        mAppState = state;
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
