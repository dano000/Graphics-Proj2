using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ToAudioButton : MonoBehaviour {

    public void ToAudio()
    {
        SceneManager.LoadScene("audio");
    }

}
