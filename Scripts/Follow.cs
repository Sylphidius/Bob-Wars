using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform track;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (track != null)
            transform.position = track.position;
    }
    public void Show()
    {
        GetComponent<SpriteRenderer>().enabled = true;
    }
    public void Hide()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }
    public void SetTrack(Transform t)
    {
        track = t;
    }
}
