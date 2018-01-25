﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.MRDL.PeriodicTable;
using TMPro;

public class App : MonoBehaviour { 

    public enum AppState {
        Unknown = 0,
        Init,
        Welcome,
        PodiumSetup,
        CreateCompound,
        ViewCompound
    };

    public GameObject sceneContent;
    public GameObject podium;
    public GameObject table;
    public GameObject elementTemplate;
    public GameObject componentName; 

    protected CompoundDescription mCompoundDescription;
    protected int mCompoundIndex = 0;
    protected List<GameObject> elementsObjects = new List<GameObject>();

    protected AppState mAppState = AppState.Init;

    // Use this for initialization
    void Start()
    {
        Debug.Log("Find AppObject");

        GameObject appObject = GameObject.Find("AppObject");

        if (appObject != null)
        {
            DontDestroyOnLoad(appObject);
        }
        else
        {
            Debug.LogError("appObject not exists");
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    public bool IsState(AppState state)
    {
        return (mAppState == state);
    }

    public AppState GetState()
    {
        return mAppState;
    }

    public void SetState(AppState state)
    {
        switch (mAppState)
        {
            case AppState.Init:
                {
                    GameObject obj = GameObject.Find("WelcomePanel");
                    if (obj != null)
                    {
                        obj.SetActive(false);
                    }
                }
                break;
            case AppState.PodiumSetup:
                {
                    SetNextComponent();
                }
                break;
        }
        mAppState = state;
    }

    public void SetPodium(Vector3 pos)
    {
        podium.transform.position = pos;
        podium.SetActive(true);
    }

    public void SetPeriodicTable(Vector3 direction, float distance,  Vector3 cameraPos)
    {
        table.transform.position = direction * (3.0f + distance);
        table.transform.LookAt(-cameraPos, new Vector3(0, 1, 0));
        table.transform.Rotate(new Vector3(0, 1, 0), 180);
        table.SetActive(true);
    }

    public void TryToAddElement(Element element)
    {
        // check that element could be added

        if (mCompoundDescription.IsEmptyElement(element.ElementName.text))
        {
            // find target position of an element on the podium
            Vector3 pos;
            if(mCompoundDescription.GetElementPosition(element.ElementName.text, out pos))
            {
                pos += podium.transform.position;
                pos.y += 1.5f;

                // Create element
                GameObject elementObject = (GameObject)Instantiate(elementTemplate, sceneContent.transform);
                elementObject.transform.position = element.transform.position;
                elementsObjects.Add(elementObject);

                // start animation
                StartCoroutine(SmoothMovement(elementObject, pos));
            }
        }

    }


    protected IEnumerator SmoothMovement(GameObject obj, Vector3 pos)
    {
        float moveTime = 0.20f;
        float inverseMoveTime = 1f / moveTime;

        //Calculate the remaining distance to move based on the square magnitude of the difference between current position and end parameter. 
        //Square magnitude is used instead of magnitude because it's computationally cheaper.
        float sqrRemainingDistance = (obj.transform.position - pos).sqrMagnitude;

        //While that distance is greater than a very small amount (Epsilon, almost zero):
        while (sqrRemainingDistance > float.Epsilon)
        {
            //Find a new position proportionally closer to the end, based on the moveTime
            Vector3 newPostion = Vector3.MoveTowards(obj.transform.position, pos, inverseMoveTime * Time.deltaTime);

            //Call MovePosition on attached Rigidbody2D and move it to the calculated position.
            obj.transform.position = newPostion;

            //Recalculate the remaining distance after moving.
            sqrRemainingDistance = (obj.transform.position - pos).sqrMagnitude;

            //Return and loop until sqrRemainingDistance is close enough to zero to end the function
            yield return null;
        }
    }

    public void SetNextComponent()
    {
        foreach(GameObject obj in elementsObjects)
        {
            Destroy(obj); 
        };

        switch(mCompoundIndex)
        {
            case 0:
               // mCompoundDescription = CompoundDescription.GetWater();
                mCompoundDescription = CompoundDescription.GetOxygen();
                break;
            case 1:
                mCompoundDescription = CompoundDescription.GetWater();
                break;
            case 2:
                mCompoundDescription = CompoundDescription.GetSodiumChloride();
                break;
        };

        mCompoundIndex++;
        mCompoundIndex %= 3;

        StartCoroutine(UpdatePodiumTitle());
    }

    public bool IsCurrentCompoundCompletted()
    {
        return mCompoundDescription.IsCompletted();
    }

    public IEnumerator UpdatePodiumTitle()
    {
        yield return new WaitForSeconds(1.0f);
        var textMesh = componentName.GetComponent<TextMeshPro>();
        if (textMesh != null)
        {
            textMesh.text = mCompoundDescription.mName;
        }
    }
}
