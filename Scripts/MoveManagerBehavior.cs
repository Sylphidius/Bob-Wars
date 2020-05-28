using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManagerBehavior : MonoBehaviour
{
    public Queue<GameObject> path;
    
    public GridMovement p;
    public PathSelector pathSelector;
    public bool select = true;
    public bool move = false;
    // Start is called before the first frame update
    void Start()
    {
        pathSelector = transform.GetChild(0).gameObject.GetComponent<PathSelector>();

    }

    // Update is called once per frame
    void Update()
    {
        if(p != null)
        {
            if (select)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    select = false;
                    path = pathSelector.getPath();
                    pathSelector.gameObject.SetActive(false);
                } else if (Input.GetKeyDown(KeyCode.B)) pathSelector.Init(p.transform.position);
            } else if (path.Count != 0)
            {
                if (p.stop)
                {
                    GameObject o = path.Dequeue();
                    p.targetPoint.position = o.transform.position;
                    Destroy(o);
                }
            }
            else
            {
                if (p.stop)
                {
                    select = true;
                    pathSelector.gameObject.SetActive(true);
                    pathSelector.Init(p.transform.position);
                }
            }
        }
    }
}
