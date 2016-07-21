using UnityEngine;
using System.Collections;

public class Video : MonoBehaviour
{
    public MovieTexture mt;
    public GameObject hideBg;
    float timeToEnd;
	// Use this for initialization
	void Start ()
    {
        hideBg.SetActive(false);
        mt.Play();
        timeToEnd = Time.time;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Time.time - timeToEnd >= (mt.duration - 0.1f))
        {
            Debug.Log("HIDE");
            hideBg.SetActive(true);
        }
       /* if (!mt.isPlaying)
        {
            hideBg.SetActive(true);

        }*/
    }
}
