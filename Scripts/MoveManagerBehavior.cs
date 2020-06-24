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
    public bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        pathSelector = transform.GetChild(0).gameObject.GetComponent<PathSelector>();

    }

    // Update is called once per frame
    void Update()
    {
        if (active)
            Move();
    }
    public void Move()
    {
        if (p != null)
        {
            if (select)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (pathSelector.getPath().Count != 1)
                    {
                        select = false;
                        path = pathSelector.getPath();
                        pathSelector.enabled = false;
                    }
                    else
                    {
                        pathSelector.Init(p.transform.position);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.B)) 
                {
                    if (pathSelector.getPath().Count != 1)
                        pathSelector.Init(p.transform.position);
                    else
                        Desactivate();
                }
            }
            else if (path.Count != 0)
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
                    pathSelector.enabled = true;
                    Desactivate();

                }
            }
        }
    }
    public void Activate()
    {
        pathSelector.Activate();
        active = true;
        pathSelector.Init(p.transform.position);
        
    }
    public void Desactivate()
    {
        pathSelector.Desactivate();
        active = false;
        pathSelector.Init(p.transform.position);
        
    }
}
