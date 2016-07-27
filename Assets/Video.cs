using UnityEngine;
using System.Collections;

public class Video : MonoBehaviour
{
    public MovieTexture mt;
    public GameObject hideBg;
    public GameObject particle1;
    public GameObject particle2;

    float timeToEnd;
	// Use this for initialization
	void Start ()
    {
        hideBg.SetActive(false);
        mt.Play();
        timeToEnd = Time.time;
	}

    // Update is called once per frame
    void Update()
    {
        if (Time.time - timeToEnd >= 0.25f && !particle1.activeSelf)
        {
            particle1.SetActive(true);
        }
        if (Time.time - timeToEnd >= 1.30f && !particle2.activeSelf)
        { 
            particle2.SetActive(true);
        }
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
