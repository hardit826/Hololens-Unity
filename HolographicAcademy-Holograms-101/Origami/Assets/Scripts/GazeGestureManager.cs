using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR.WSA.Input;


public class GazeGestureManager : MonoBehaviour {

    public static GazeGestureManager Instance{  get;private set;}

    // Represents the hologram that is currently being gazed at.
    public GameObject FocusedObject { get; private set; }

    GestureRecognizer recognizer;

    // Use this for initialization
    void Start()
    {
        Instance = this;
        //Set up gesture GestureRecognizer to select the gesture
        recognizer = new GestureRecognizer();

        recognizer.TappedEvent += (source, tapCount, ray) =>
        {
            if (FocusedObject != null)
            {
                FocusedObject.SendMessageUpwards("OnSelect");
            }
        };
        recognizer.StartCapturingGestures();
	}

    // Update is called once per frame
    void Update()
    {
        GameObject oldFocusObject = FocusedObject;

        var headPos = Camera.main.transform.position;
        var gazeDir = Camera.main.transform.forward;

        RaycastHit hitInfo;
        if (Physics.Raycast(headPos, gazeDir, out hitInfo))
        {
            FocusedObject = hitInfo.collider.gameObject;
        }
        else
        {
            FocusedObject = null;
        }
        if (FocusedObject != oldFocusObject)
        {
            recognizer.CancelGestures();
            recognizer.StartCapturingGestures();
        }
	}
}
