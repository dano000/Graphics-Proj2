using UnityEngine;
using System.Collections;

public class gameController : MonoBehaviour {

    public GameObject[] sounds;
    public GameObject[] instanciatedSounds;
    public float spacing = 5.0f;
    public GameObject[] soundOrder;
    public int[] soundOrderRotation;
    public int maxRepeats = 3;
    // Use this for initialization
    void Start () {
        generateObjects(sounds);
        generateSoundOrder(1);
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
            for(int j=0; j < Random.Range(1, maxRepeats);j++)
            {
                Debug.Log(j);
                soundOrder[j] = selectedSound;
                soundOrderRotation[j] = 1;
            }
        }
    }


    // Source: http://answers.unity3d.com/questions/904981/how-to-play-an-audio-file-after-another-finishes.html
    IEnumerator playEngineSound()
    {
        for (int i = 0; i < soundOrder.Length; i++)
        {
            AudioSource currentAudio = soundOrder[i].GetComponentInChildren<AudioSource>();
            
            currentAudio.pitch = soundOrderRotation[i];
            currentAudio.Play();
            yield return new WaitForSeconds(currentAudio.clip.length);
        }
    }


        
}
