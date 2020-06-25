using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    public float speed = 5f;
    public Transform targetPoint;
    public Animator animator;
    public SpriteRenderer chara;
    public LayerMask stopMovement;
    public bool stop = true;
    // Start is called before the first frame update
    void Start()
    {
        targetPoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,targetPoint.position,speed*Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPoint.position) == 0)
        {
            stop = true;
            animator.SetBool("Move", false);
        }
        else 
        { 
            stop = false; animator.SetBool("Move", true);
            chara.flipX = (transform.position.x > targetPoint.position.x);
        }
    }
}
