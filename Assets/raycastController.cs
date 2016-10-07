using UnityEngine;
using System.Collections;

public class raycastController : MonoBehaviour
{
    private float tapTimeframe = 0.2f;
    private int numTaps = 0;
    private Vector3 campos;
    private Vector3 newCampos;
    private bool isLerping = false;
    public cameraController camCtrl;
    public float zSpace = 5.0f;

    // Use this for initialization
    void Start()
    {

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
                    if (Physics.Raycast(ray, out rcHit))
                    {
                        soundObject sc = rcHit.transform.gameObject.transform.parent.gameObject.GetComponent<soundObject>();
                        AudioSource audio = sc.GetComponentInChildren<AudioSource>();
                        audio.pitch = sc.pitch;
                        audio.Play();

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

                        }
                    }
                    else
                    {
                        camCtrl.resetCameraPos();
                    }
                    break;
                case 3:
                    Debug.Log("Triple Tap!");
                    break;
                default:
                    break;

            }



            numTaps = 0;
            tapTimeframe = 0.5f;
        }





    }



}


