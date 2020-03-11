using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    public float m_Speed = 12f;             // How fast the boat moves forward and back
    public float m_TurnSpeed = 180f;        // How fast the boat turns in degrees p/second

    private Rigidbody m_Rigidbody;
    private float m_MovementInputValue;     // The current value of the movement input
    private float m_TurnInputValue;         // The current value of the turn input

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        // When the boat is turned on, make sure it is not kinematic
        m_Rigidbody.isKinematic = false;

        // Also reset the input values
        m_MovementInputValue = 0f;
        m_TurnInputValue = 0f;
    }

    private void OnDisable()
    {
        // When the boat is turned off, set it to kinematic so it stops moving
        m_Rigidbody.isKinematic = true;
    }

    // Update is called once per frame
    private void Update()
    {
        m_MovementInputValue = Input.GetAxis("Vertical");
        m_TurnInputValue = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        Move();
        Turn();
    }

    private void Move()
    {
        // Create a vector in the direction the boat is facing with a magnitude
        // based on the input, speed and time between frames
        Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;

        // Apply the movement to the Rigidbody's position
        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
    }

    private void Turn()
    {
        // Determine the number of degrees to be turned based on the input,
        // speed and time between frames
        float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;

        // Make this into a rotation in the Y axis
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        // Apply this rotation to the Rigidbody's rotation
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
    }
}
