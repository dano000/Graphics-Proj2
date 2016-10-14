using UnityEngine;
using System.Collections;

public class soundObject : MonoBehaviour {

    public AudioSource sound;
    public float pitch = 1;

    private float JiggleTime = 0.2f;
    private float JiggleRadius = 0.05f;
    private float CurrTime = 0;
    public bool isJiggling = false;
    private Vector3 initialPosition;


    // Use this for initialization
    void Start () {
        initialPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        /*Vector3 dir = Vector3.zero;
        dir.x = -Input.acceleration.y;
        dir.z = Input.acceleration.x;
        if (dir.sqrMagnitude > 1)
            dir.Normalize();

        dir *= Time.deltaTime;
        transform.Translate(dir * 1);
       // Debug.LogError(dir);

        //Debug.LogError(Mathf.Atan(dir.y / dir.x));
	*/
    if(isJiggling)
        {
            Jiggle();
        }
    }

    public void startJiggle()
    {
        isJiggling = true;
    }

    private void Jiggle() {

        if(CurrTime < JiggleTime)
        {
            float x;
            float y;
            Vector2 randJig = Random.insideUnitCircle * JiggleRadius;
            x = randJig.x;
            y = randJig.y;
            transform.position = transform.position + new Vector3(x, 0, y);
            CurrTime += Time.deltaTime;
        }
        else
        {
            isJiggling = false;
            CurrTime = 0.0f;
            transform.position = initialPosition;
        }

    }
}
