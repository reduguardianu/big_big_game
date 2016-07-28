using UnityEngine;
using System.Collections;

public class VideoKeynes : MonoBehaviour {

    public MovieTexture movieTexture;
    float timeToEnd;
    // Use this for initialization
    void Start () {
        movieTexture.Play();
        timeToEnd = Time.time;
        movieTexture.loop = true;
    }

    // Update is called once per frame
    void Update () {
        if (Time.time - timeToEnd >= movieTexture.duration- 0.1f)
        {
            Debug.Log("replay");
            movieTexture.Play();
            timeToEnd = Time.time;

        }
    }
}
