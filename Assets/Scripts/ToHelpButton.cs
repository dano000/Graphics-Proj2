using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ToHelpButton : MonoBehaviour {

    public void ToHelp(){

        SceneManager.LoadScene("help");
    }
}
