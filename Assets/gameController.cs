using UnityEngine;
using System.Collections;

public class gameController : MonoBehaviour {

    public GameObject[] sounds;
    public float spacing = 5.0f;
    // Use this for initialization
    void Start () {
        generateObjects(sounds);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void generateObjects(GameObject[] soundObjectArray)
    {
        for(int i=0; i < soundObjectArray.Length; i++)
        {
            Vector3 currPos = new Vector3(-spacing + (i* spacing), 0, 0);
            Quaternion currRot = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
            Instantiate(soundObjectArray[i],currPos,currRot);
        }
    }
}
