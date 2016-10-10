using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class raycastController : MonoBehaviour
{
    private float tapTimeframe = 0.2f;
    private int numTaps = 0;
    private Vector3 campos;
    private Vector3 newCampos;
    private bool isLerping = false;
    public cameraController camCtrl;
    public gameController gameCtrl;
    public float zSpace = 5.0f;
    public GameObject[] answerOrder;
    public int currentAns = 0;
    public GameObject LoseText;
    public GameObject WinText;
    public Canvas canvas;
    public Button btn;
    public GameObject currentGuess;

    // Use this for initialization
    void Start()
    {

        LoseText.GetComponent<RectTransform>().position = new Vector3(canvas.pixelRect.width/2, LoseText.GetComponent<RectTransform>().rect.height * -2,0);
        WinText.GetComponent<RectTransform>().position = new Vector3(canvas.pixelRect.width / 2, WinText.GetComponent<RectTransform>().rect.height * -2, 0);
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
                    Debug.Log("Double Tap!");
                    if (camCtrl.inZoom == false) {
                        if (Physics.Raycast(ray, out rcHit))
                        {
                            newCampos = new Vector3(rcHit.transform.gameObject.transform.position.x, camCtrl.cam.transform.position.y, rcHit.transform.gameObject.transform.position.z - zSpace);
                            camCtrl.doCameraLerp(newCampos);
                            currentGuess = rcHit.transform.gameObject.transform.parent.gameObject;
                        }
                    }
                    else
                    {
                        camCtrl.resetCameraPos();
                        currentGuess = null;
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

    IEnumerator loseGame()
    {
        
        
        yield return new WaitForSeconds(3);

        SceneManager.LoadScene("main");

    }

    public void winGame()
    {

    }

    public void handleAdd()
    {
        
        answerOrder[currentAns] = currentGuess;
        if (answerOrder[currentAns] == gameCtrl.soundOrder[currentAns])
        {
            Debug.Log("Correct Guess!");
        }
        else
        {
            Debug.Log("Incorrect Guess!");
            LoseText.GetComponent<Animation>().Play("LoseTextHolder");
            StartCoroutine(loseGame());

        }

        currentAns++;
        if (currentAns == (gameCtrl.soundOrder.Count))
        {
            Debug.Log("Game Won!");
            winGame();
            WinText.GetComponent<Animation>().Play("LoseTextHolder");
        }
  
        Debug.Log("Button Pressed!");
    }

        
}


