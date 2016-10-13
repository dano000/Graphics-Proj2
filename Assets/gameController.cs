using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gameController : MonoBehaviour {

	public static int level = 1;
    public GameObject[] sounds;
    public GameObject[] instanciatedSounds;
    public float spacing = 5.0f;
    public List<GameObject> soundOrder;
    public GameObject[] answerOrder;
    public int[] soundOrderRotation;
    public int maxRepeats = 3;
	public Camera cubemapCamera;
	public Cubemap map;

    // Use this for initialization
    void Start () {
		// create cubemap for reflective glass
		Vector3 oldPos = cubemapCamera.transform.position;
		Quaternion oldRot = cubemapCamera.transform.rotation;
		cubemapCamera.transform.position = new Vector3 (0.0f, 1.0f, 0.0f);
		cubemapCamera.transform.rotation = Quaternion.LookRotation (Vector3.left);
		cubemapCamera.RenderToCubemap (map);
		cubemapCamera.transform.position = oldPos;
		cubemapCamera.transform.rotation = oldRot;
		// create objects
        soundOrder = new List<GameObject>();
        generateObjects(sounds);
        generateSoundOrder(gameController.level);
        StartCoroutine(playEngineSound());

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
            GameObject s = (GameObject)Instantiate(soundObjectArray[i], currPos, currRot);
            instanciatedSounds[i] = s;
        }

    }

    void generateSoundOrder(int level)
    {
        for(int i=0; i< level; i++)
        {
            GameObject selectedSound = instanciatedSounds[Random.Range(0,sounds.Length)];
            for(int j=0; j < Random.Range(1, maxRepeats+1);j++)
            {
                soundOrder.Add(selectedSound);
                //soundOrderRotation[j] = 1;
            }
        }
    }


    // Source: http://answers.unity3d.com/questions/904981/how-to-play-an-audio-file-after-another-finishes.html
    IEnumerator playEngineSound()
    {
        for (int i = 0; i < soundOrder.Count; i++)
        {
            // add try/except here for null sounds
            AudioSource currentAudio = soundOrder[i].GetComponentInChildren<AudioSource>();
            
            //currentAudio.pitch = soundOrderRotation[i];
            currentAudio.Play();
            yield return new WaitForSeconds(currentAudio.clip.length);
        }
    }


        
}
