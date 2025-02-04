﻿//Uses accelerometer to increase/decrease spotlight intensity.
//http://devblog.aliasinggames.com/accelerometer-unity/


using UnityEngine;
using System.Collections;

public class AccelerometerControl : MonoBehaviour {

    public Light lt;

    public float SmoothingValue = 0.2f;

    private LowPassFilter filterY;
    private LowPassFilter filterZ;

	// Use this for initialization
	void Start () {
        lt = GetComponent<Light>();
        filterY = new LowPassFilter(SmoothingValue);
        filterZ = new LowPassFilter(SmoothingValue); 
	}

	// Update is called once per frame
	void Update () {
        float increment = 0.0f;
        // calculate new filtered values
        float filteredY = filterY.NextStep(Time.deltaTime, Input.acceleration.x);
        float filteredZ = filterZ.NextStep(Time.deltaTime, Input.acceleration.z);

        // calculate tilt - angle between y and negative z-axis
        float tilt = Mathf.Atan2(filteredY, -filteredZ) * Mathf.Rad2Deg; 
        float clampedTilt = Mathf.Clamp(tilt, -45.0f, 45.0f);

        if (clampedTilt > 0.0)
            increment = 0.3f;
        else if (clampedTilt < 0.0)
            increment = -0.3f;

        lt.intensity += increment;
        
    }
}
