using UnityEngine;
using System.Collections;

public class soundObject : MonoBehaviour {

    public AudioSource sound;
    public float pitch = 1;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 dir = Vector3.zero;
        dir.x = -Input.acceleration.y;
        dir.z = Input.acceleration.x;
        if (dir.sqrMagnitude > 1)
            dir.Normalize();

        dir *= Time.deltaTime;
        transform.Translate(dir * 1);
       // Debug.LogError(dir);

        //Debug.LogError(Mathf.Atan(dir.y / dir.x));
	}
}
