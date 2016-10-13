using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class raycastController : MonoBehaviour
{
    private float tapTimeframe = 0.2f;
    private int numTaps = 0;
    private Vector3 campos;
    private Vector3 newCampos;
    public cameraController camCtrl;
    public gameController gameCtrl;
	public List<GameObject> answerOrder;
    public int currentAns = 0;
    public GameObject LoseText;
    public GameObject WinText;
	public Button menuBtn;
	public Button nextLevelBtn;
	public Button tryAgainBtn;
    public Canvas canvas;
    public Button btn;
	public Text numGuesses;
	public Text levelNum;
    public GameObject currentGuess;
	private Vector3 buttonPos;

    // Use this for initialization
    void Start()
    {
        LoseText.GetComponent<RectTransform>().position = new Vector3(canvas.pixelRect.width/2, LoseText.GetComponent<RectTransform>().rect.height * -2,0);
        WinText.GetComponent<RectTransform>().position = new Vector3(canvas.pixelRect.width / 2, WinText.GetComponent<RectTransform>().rect.height * -2, 0);
		buttonPos = tryAgainBtn.GetComponent<RectTransform> ().position;
		tryAgainBtn.GetComponent<RectTransform>().position = new Vector3(canvas.pixelRect.width * 2, canvas.pixelRect.height * 2, 0);
		nextLevelBtn.GetComponent<RectTransform>().position = new Vector3(canvas.pixelRect.width * 2, canvas.pixelRect.height * 2, 0);
		levelNum.text = gameController.level.ToString();
		numGuesses.text = currentAns.ToString ();
		answerOrder = new List<GameObject> ();
    }

    // Update is called once per frame
    void Update()
    {
        // Get certain amount of taps/clicks within a certain time frame

        if (tapTimeframe > 0)
        {
            tapTimeframe -= Time.deltaTime;

            if (Input.GetMouseButtonDown(0))
            {
                numTaps += 1;
            }
            //Debug.Log("Num Taps: " + numTaps);
        }

        else
        {
            RaycastHit rcHit = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            switch (numTaps)
            {

                case 1:
                    /*
                    // play the sound from raycast
                    if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                        RaycastHit rcHit = new RaycastHit();
                        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                        Debug.LogError(Input.GetTouch(0).position);
                        if (Physics.Raycast(ray, out rcHit))
                        {
                            soundObject sc = rcHit.transform.gameObject.transform.parent.gameObject.GetComponent<soundObject>();
                            AudioSource audio = sc.GetComponentInChildren<AudioSource>();
                            audio.pitch = sc.pitch;
                            audio.Play();
                        }
                    }
                    */
                    //if (Input.GetMouseButtonDown(0))
                    //{

                    //Debug.LogError(Input.mousePosition);
                    if (camCtrl.inZoom) {
                        if (Physics.Raycast(ray, out rcHit))
                        {
                            
                            soundObject sc = rcHit.transform.gameObject.transform.parent.gameObject.GetComponent<soundObject>();
                            AudioSource audio = sc.GetComponentInChildren<AudioSource>();
                            audio.pitch = sc.pitch;
                            audio.Play();
                        }
                    }
                    //  }

                    break;
                case 2:
                    if (camCtrl.inZoom == false) {
                        if (Physics.Raycast(ray, out rcHit))
                        {
						newCampos = new Vector3(rcHit.transform.gameObject.transform.position.x, camCtrl.cam.transform.position.y - 2.0f, rcHit.transform.gameObject.transform.position.z - 5.0f);
                            camCtrl.doCameraLerp(newCampos);
                            currentGuess = rcHit.transform.gameObject.transform.parent.gameObject;
                            camCtrl.isActiveGyro = true;
                        }
                    }
                    else
                    {
                        camCtrl.resetCameraPos();
                        currentGuess = null;
                        camCtrl.isActiveGyro = false;
                    }
                    break;
                default:
                    break;

            }
            numTaps = 0;
            tapTimeframe = 0.5f;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            handleAdd();
        }
    }

    public void loseGame()
    {
		LoseText.GetComponent<Animation>().Play("LoseTextHolder");
		tryAgainBtn.GetComponent<RectTransform> ().position = buttonPos;
    }

	public void tryAgain(){
		SceneManager.LoadScene("main");
	}

    public void winGame()
    {
		WinText.GetComponent<Animation>().Play("LoseTextHolder");
		nextLevelBtn.GetComponent<RectTransform> ().position = buttonPos;
    }

	public void nextLevel() {
		gameController.level++;
		SceneManager.LoadScene("main");
	}

	public void loadMenu() {
		SceneManager.LoadScene("menu");
	}

    public void handleAdd()
    {
        
		answerOrder.Add(currentGuess);
        if (answerOrder[currentAns] == gameCtrl.soundOrder[currentAns])
        {
			currentAns++;
			numGuesses.text = currentAns.ToString ();
        }
        else
        {
			loseGame();
        }
        if (currentAns == (gameCtrl.soundOrder.Count))
        {
            winGame();
        }
    }
}


