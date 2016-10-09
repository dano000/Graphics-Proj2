using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ToGameButton : MonoBehaviour {

	public void ToGame () {

        SceneManager.LoadScene("main");
	}
}
