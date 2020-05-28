using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSelector : MonoBehaviour
{
    public Vector3 start;
    public GameObject HG;
    public GameObject HD;
    public GameObject BG;
    public GameObject BD;
    public Sprite Up;
    public Sprite Down;
    public Sprite Left;
    public Sprite Right;
    public GameObject H;
    public GameObject V;
    private Queue<GameObject> path;
    public Transform targetPoint;
    public LayerMask stopMovement;
    public string precedent = "";
    public string actuel = "";
    public float speed = 5f;
    public bool arrived = false;
    public SpriteRenderer sRenderer;
    // Start is called before the first frame update
    void Start()
    {
        path = new Queue<GameObject>();
        targetPoint.parent = null;
        sRenderer = GetComponent<SpriteRenderer>();
        sRenderer.sprite = Right;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPoint.position) <= 0)
        {
            if (arrived)
            {
                AddPath();
                arrived = false;
            }
            if (!Physics2D.OverlapCircle(targetPoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), 0.4f, stopMovement) && Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                arrived = true;
                targetPoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                if(actuel == "") 
                {
                    actuel = Input.GetAxisRaw("Vertical") == 1f ? "H" : "B";
                    precedent = actuel;
                }
                else
                {
                    precedent = actuel;
                    actuel = Input.GetAxisRaw("Vertical") == 1f ? "H":"B";
                }
            }
            else if (!Physics2D.OverlapCircle(targetPoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), 0.4f, stopMovement) && Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                arrived = true;
                targetPoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                if (actuel == "")
                {
                    actuel = Input.GetAxisRaw("Horizontal") == 1f ? "D" : "G";
                    precedent = actuel;
                }
                else
                {
                    precedent = actuel;
                    actuel = Input.GetAxisRaw("Horizontal") == 1f ? "D" : "G";
                }
            }
        }
    }
    void AddPath() 
    {
        string s = precedent + actuel;
        switch (s)
        {
            case "HH":
                sRenderer.sprite = Up;
                path.Enqueue(Instantiate(V, new Vector3(0f, -1f, 0f) + transform.position, Quaternion.identity));
                break;
            case "GH":
                sRenderer.sprite = Up;
                path.Enqueue(Instantiate(HD, new Vector3(0f, -1f, 0f) + transform.position, Quaternion.identity));
                break;
            case "DH":
                sRenderer.sprite = Up;
                path.Enqueue(Instantiate(HG, new Vector3(0f, -1f, 0f) + transform.position, Quaternion.identity));
                break;
            case "GB":
                path.Enqueue(Instantiate(BD, new Vector3(0f, 1f, 0f) + transform.position, Quaternion.identity));
                sRenderer.sprite = Down;
                break;
            case "BB":
                path.Enqueue(Instantiate(V, new Vector3(0f, 1f, 0f) + transform.position, Quaternion.identity));
                sRenderer.sprite = Down;
                break;
            case "DB":
                path.Enqueue(Instantiate(BG, new Vector3(0f, 1f, 0f) + transform.position, Quaternion.identity));
                sRenderer.sprite = Down;
                break;
            case "DD":
                path.Enqueue(Instantiate(H, new Vector3(-1f, 0f, 0f) + transform.position, Quaternion.identity));
                sRenderer.sprite = Right;
                break;
            case "HD":
                path.Enqueue(Instantiate(BD, new Vector3(-1f, 0f, 0f) + transform.position, Quaternion.identity));
                sRenderer.sprite = Right;
                break;
            case "BD":
                path.Enqueue(Instantiate(HD, new Vector3(-1f, 0f, 0f) + transform.position, Quaternion.identity));
                sRenderer.sprite = Right;
                break;
            case "GG":
                path.Enqueue(Instantiate(H, new Vector3(1f, 0f, 0f) + transform.position, Quaternion.identity));
                sRenderer.sprite = Left;
                break;
            case "HG":
                path.Enqueue(Instantiate(BG, new Vector3(1f, 0f, 0f) + transform.position, Quaternion.identity));
                sRenderer.sprite = Left;
                break;
            case "BG":
                path.Enqueue(Instantiate(HG, new Vector3(1f, 0f, 0f) + transform.position, Quaternion.identity));
                sRenderer.sprite = Left;
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.layer);
        if (collision.gameObject.layer == 9) {
            Debug.Log("trigger");
            arrived = false;
            GameObject o = null;
            Queue<GameObject> temp = new Queue<GameObject>();
            while (collision.gameObject != path.Peek())
            {
                o = path.Dequeue();
                temp.Enqueue(o);
            }
            if(o != null)
            {
                Vector3 p = path.Peek().transform.position - o.transform.position;
                if (p == Vector3.left)
                {
                    actuel = "G";
                    sRenderer.sprite = Left;
                }
                else if (p == Vector3.right)
                {
                    actuel = "D";
                    sRenderer.sprite = Right;
                }
                else if (p == Vector3.up)
                {
                    actuel = "H";
                    sRenderer.sprite = Up;
                }
                else if (p == Vector3.down)
                {
                    actuel = "B";
                    sRenderer.sprite = Down;
                }
            }
            while (path.Count != 0)
            {
                Destroy(path.Dequeue());
            }
            path = temp;
        }
        
    }
    public void Init(Vector3 s)
    {
        start = s;
        
        targetPoint.position = s;
        transform.position = s;
        
       
        sRenderer.sprite = Right;
        precedent = "";
        actuel = "";
        ErasePath();
    }

    private void ErasePath()
    {
        while (path.Count != 0) Destroy(path.Dequeue());
    }

    public Queue<GameObject> getPath()
    {
        GameObject o = new GameObject();
        o.transform.position = transform.position;
        path.Enqueue(o);
        return path;
    }
}
