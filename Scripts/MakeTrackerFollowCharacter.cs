using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeTrackerFollowCharacter : MonoBehaviour
{
    public GameObject character;
    public GameObject tracker;
    public float speed = 5.0f;
    public float rotationSpeed = 1.5f;
    public float distanceLimit = 1.1f;
    bool isTracking;

    // Start is called before the first frame update
    void Start()
    {
        isTracking = false;
        character = GameObject.FindWithTag("Character");
        tracker = GameObject.FindWithTag("Tracker");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = character.transform.position - tracker.transform.position;      
        if (direction.magnitude >= distanceLimit && isTracking)
        {
            tracker.transform.rotation = Quaternion.Slerp(tracker.transform.rotation, Quaternion.LookRotation(direction.normalized), Time.deltaTime * rotationSpeed);
            tracker.transform.Translate(0, 0, speed * Time.deltaTime);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == character)
        {
            Debug.Log("Out of bounds");
            isTracking = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == character)
        {
            isTracking = false;
        }
    }
}
