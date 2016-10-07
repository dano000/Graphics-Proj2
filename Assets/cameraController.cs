using UnityEngine;
using System.Collections;


// References: http://answers.unity3d.com/questions/49542/smooth-camera-movement-between-two-targets.html

public class cameraController : MonoBehaviour {
    public bool isLerping = false;
    public Camera cam;
    public float speed;
    public Vector3 currTarget;
    public Vector3 initialCameraPosition;
	// Use this for initialization
	void Start () {
        currTarget = cam.transform.position;
        initialCameraPosition = cam.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if(currTarget != cam.transform.position) { 
        cam.transform.position = Vector3.Lerp(cam.transform.position, currTarget, speed * Time.deltaTime);
        }
    }


    public void doCameraLerp(Vector3 newCampos)
    {
        currTarget = newCampos;
        Debug.Log("Inside cDoCamLerp");
        Debug.Log("currentCamPos" + cam.transform.position);
        Debug.Log("endCamPos" + newCampos);

    }
}
