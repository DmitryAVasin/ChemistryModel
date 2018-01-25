using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class FloorScript : MonoBehaviour, IInputClickHandler, IInputHandler
{
    public App app;
    public Camera camera;


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
            var headPosition = Camera.main.transform.position;
            var gazeDirection = Camera.main.transform.forward;

            RaycastHit hitInfo;

            if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
            {
                pos = hitInfo.point;
                if(hitInfo.collider.gameObject.name == "FloorPlane")
                {
                    app.SetPodium(pos);
                    app.SetState(App.AppState.CreateCompound);

                    Vector3 direction = new Vector3(camera.transform.forward.x, 0, camera.transform.forward.z);
                    direction.Normalize();

                    float distance = (pos - camera.transform.position).magnitude;

                    app.SetPeriodicTable(direction, distance, camera.transform.position);
                }
            }
        }
    }
    public void OnInputDown(InputEventData eventData)
    { }

    public void OnInputUp(InputEventData eventData)
    { }
}
