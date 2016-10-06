using UnityEngine;
using System.Collections;

public class raycastController : MonoBehaviour {

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
    }

}

