using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PhysicsController : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    public float normalSpeed = 3.0f;
    public float turboSpeed = 10.0f;
    public float slowedSpeed = 1.0f;
    float m_Speed;

    //Score counter
    public GameObject ScoreCounter;
    public int scoreValue;
    TextMeshProUGUI scoreText;



    public void NormalSpeed()
    {
        Debug.Log("Normal Speed");
        m_Speed = normalSpeed;        
    }

    public void TurboSpeed()
    {
        Debug.Log("Turbo Speed");
        m_Speed = turboSpeed;                
    }

    public void CharacterSlowed ()
    {
        Debug.Log("You've been slowed!");
        m_Speed = slowedSpeed;
    }

    public void IncreaseScore()
    {
        scoreValue++;
        scoreText.text = "Collected: " + scoreValue;
    }

    public void RemoveObstacle(GameObject trigger)
    {
        float pushForce = 10f;
        RaycastHit obstacle;
        Vector3 rayDirection = trigger.transform.position - this.transform.position;
        if (Physics.Raycast(trigger.transform.position, rayDirection , out obstacle) && obstacle.collider.gameObject.tag == ("Obstacle")) ;
        {
            Debug.Log("Obstacle Hit");
            obstacle.rigidbody.AddForce(this.transform.right * pushForce, ForceMode.VelocityChange);
        }
    }

    public void Teleport()
    {
        GameObject teleportZone = GameObject.FindWithTag("TeleportZone");
        this.transform.position = teleportZone.transform.position;
    }


    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        NormalSpeed();

        //Score Counter
        ScoreCounter = GameObject.FindWithTag("Counter");
        scoreText = ScoreCounter.GetComponent<TextMeshProUGUI>();
        scoreValue = 0;
        scoreText.text = "Collected: " + scoreValue;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 m_Input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        this.transform.LookAt(transform.position + m_Input * Time.deltaTime);
    }

    private void FixedUpdate()
    {

        Vector3 m_Input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        m_Rigidbody.MovePosition(transform.position + m_Input * Time.deltaTime * m_Speed);

    }

    private void OnTriggerEnter(Collider other)
    {
        string m_Tag = other.gameObject.tag;
        Debug.Log(m_Tag);

        if (m_Tag == "Slow")
        {
            CharacterSlowed();
        }

        if (m_Tag == "Point")
        {
            IncreaseScore();
        }

        if (m_Tag == "ObstacleRemover")
        {
            RemoveObstacle(other.gameObject);
        }

        if (m_Tag == "Teleporter")
        {
            Teleport();
        }
    }
}