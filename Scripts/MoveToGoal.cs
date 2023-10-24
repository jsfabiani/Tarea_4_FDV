using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MoveToGoal : MonoBehaviour
{

    public GameObject goal;
    public float speed = 4.0f;
    bool movementActivated;
    public float threshold = 0.1f;
    Rigidbody rb;


    public void StartMovement()
    {
        movementActivated = true;   
    }

    // Start is called before the first frame update
    void Start()
    {
        movementActivated = false;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Vector3 direction = goal.transform.position - this.transform.position;

        //Para que se detenga cuando llegue al objeto
        if (direction.x < threshold)
        {
            movementActivated = false;
        }

        if (movementActivated)
        {
            Debug.Log(direction);
            rb.MovePosition(this.transform.position + direction.normalized * speed * Time.fixedDeltaTime);
        }
    }
}
