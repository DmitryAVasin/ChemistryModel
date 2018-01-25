using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class FloorScript : MonoBehaviour, IInputClickHandler, IInputHandler
{
    public App app;
    public Camera mCamera;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnInputClicked(InputClickedEventData eventData)
    {
        // AirTap code goes here
        if(app.IsState(App.AppState.PodiumSetup))
        {
            // find position
            Vector3 pos = new Vector3(0, 0.0f, 5.0f);

            // head position and orientation.
            var headPosition = mCamera.transform.position;
            var gazeDirection = mCamera.transform.forward;

            RaycastHit hitInfo;

            if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
            {
                pos = hitInfo.point;
                if(hitInfo.collider.gameObject.name == "FloorPlane")
                {
                    app.SetPodium(pos);
                    app.SetState(App.AppState.CreateCompound);

                    Vector3 direction = new Vector3(mCamera.transform.forward.x, 0, mCamera.transform.forward.z);
                    direction.Normalize();

                    float distance = (pos - mCamera.transform.position).magnitude;

                    app.SetPeriodicTable(direction, distance, mCamera.transform.position);
                }
            }
        }
    }
    public void OnInputDown(InputEventData eventData)
    { }

    public void OnInputUp(InputEventData eventData)
    { }
}
