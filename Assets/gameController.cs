using UnityEngine;
using System.Collections;

public class gameController : MonoBehaviour {

    public GameObject[] sounds;
    public float spacing = 5.0f;
    public GameObject[] soundOrder;
    public int[] soundOrderRotation;
    public int maxRepeats = 3;
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

    void generateSoundOrder(int level)
    {
        for(int i=0; i< level; i++)
        {
            GameObject selectedSound = sounds[Random.Range(0,sounds.Length)];
            for(int j=0; j < Random.Range(0, maxRepeats);j++)
            {
                soundOrder[j] = selectedSound;
                soundOrderRotation[j] = 1;
            }
        }
    }

    void playSoundOrder()
    {
        for(int i=0;i <soundOrder.Length; i++)
        {
            AudioSource currentAudio = soundOrder[i].GetComponent<AudioSource>();
            
            currentAudio.pitch = soundOrderRotation[i];
            currentAudio.Play();
            //WaitForSeconds(currentAudio.clip.length);
        }
    }
}
