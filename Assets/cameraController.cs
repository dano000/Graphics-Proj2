using UnityEngine;
using System.Collections;


// References: http://answers.unity3d.com/questions/49542/smooth-camera-movement-between-two-targets.html

public class cameraController : MonoBehaviour {
    public bool isLerping = false;
    public Camera cam;
    public float speed;
    public Vector3 currTarget;
    public Vector3 initialCameraPosition;
    public bool inZoom = false;
    public bool isActiveGyro = false;
    public raycastController rcCtrl;
	// Use this for initialization
	void Start () {
        currTarget = cam.transform.position;
        initialCameraPosition = cam.transform.position;
    }
	
	// Update is called once per frame
	void Update () {

        if(isActiveGyro)
        {
            Input.gyro.enabled = true;
            //cameraGyro();

        }
        else
        {
            Input.gyro.enabled = false;
        }

        if((currTarget - cam.transform.position).magnitude > 0.001f) {
            if (isLerping) {  
        cam.transform.position = Vector3.Lerp(cam.transform.position, currTarget, speed * Time.deltaTime);
            }
        }
        else
        {
            cam.transform.position = currTarget;
            isLerping = false;
        }

    }


    public void doCameraLerp(Vector3 newCampos)
    {
        currTarget = newCampos;
        isLerping = true;
        inZoom = true;
    }

    public void resetCameraPos()
    {
        currTarget = initialCameraPosition;
        isLerping = true;
        inZoom = false;
    }

    public void cameraGyro()
    {
        //Debug.LogError("Gyro x: " + Input.gyro.rotationRateUnbiased.x + " y: " + Input.gyro.rotationRateUnbiased.y + " z: " + Input.gyro.rotationRateUnbiased.z);
        //rcCtrl.currentGuess.transform.Rotate(0, -Input.gyro.rotationRateUnbiased.y, 0);
        
    }
}
