using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : Photon.PunBehaviour
{
    public float turnSpeed = 180f;
    //    public AudioSource m_MovementAudio;         
    //    public AudioClip m_EngineIdling;            
    //    public AudioClip m_EngineDriving;           
    //    public float m_PitchRange = 0.2f;           

    private Animator animator;
    private Rigidbody rigidBody;              
    private float movementInputValue;         
    private float turnInputValue;
    private float speed, walk, run;


    //private float m_OriginalPitch;          


    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }


    private void OnEnable()
    {
        // When the tank is turned on, make sure it's not kinematic.
        rigidBody.isKinematic = false;

        // Also reset the input values.
        movementInputValue = 0f;
        turnInputValue = 0f;
    }


    private void OnDisable()
    {
        // When the tank is turned off, set it to kinematic so it stops moving.
        rigidBody.isKinematic = true;
    }


    private void Start()
    {
        // The axes names are based on player number.


        // Store the original pitch of the audio source.
        //m_OriginalPitch = m_MovementAudio.pitch;
    }


    private void Update()
    {
        if (photonView.isMine)
        {
            // Store the value of both input axes.
            movementInputValue = SimpleInput.GetAxis("Vertical");
            turnInputValue = SimpleInput.GetAxis("Horizontal");

            if (Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetBool("isRun", true);
                speed = run;
            }
            else
            {
                animator.SetBool("isRun", false);
                speed = walk;
            }

            animator.SetFloat("movementInputValue", movementInputValue);
        }
        //EngineAudio();
    }

/*
    private void EngineAudio()
    {
        // If there is no input (the tank is stationary)...
        if (Mathf.Abs(movementInputValue) < 0.1f && Mathf.Abs(turnInputValue) < 0.1f)
        {
            // ... and if the audio source is currently playing the driving clip...
            if (m_MovementAudio.clip == m_EngineDriving)
            {
                // ... change the clip to idling and play it.
                m_MovementAudio.clip = m_EngineIdling;
                m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                m_MovementAudio.Play();
            }
        }
        else
        {
            // Otherwise if the tank is moving and if the idling clip is currently playing...
            if (m_MovementAudio.clip == m_EngineIdling)
            {
                // ... change the clip to driving and play.
                m_MovementAudio.clip = m_EngineDriving;
                m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                m_MovementAudio.Play();
            }
        }
    }
*/

    private void FixedUpdate()
    {
        // Adjust the rigidbodies position and orientation in FixedUpdate.
        Move();
        Turn();

        
    }


    private void Move()
    {
        // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
        Vector3 movement = transform.forward * movementInputValue * speed;// * Time.deltaTime;
        //Vector3 movement = new Vector3(movementInputValue, 0, turnInputValue) * (Time.deltaTime * speed);
        // Apply this movement to the rigidbody's position.
        //rigidBody.MovePosition(rigidBody.position + movement);
        //this.transform.Translate(movement, Space.World);
        rigidBody.AddForce(movement);
      
    }


    private void Turn()
    {
        // Determine the number of degrees to be turned based on the input, speed and time between frames.
        float turn = turnInputValue * turnSpeed * Time.deltaTime;

        // Make this into a rotation in the y axis.
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        // Apply this rotation to the rigidbody's rotation.
        rigidBody.MoveRotation(rigidBody.rotation * turnRotation);
    }

    public void SetMovement(float _walk, float _run)
    {
        walk = _walk;
        run = _run;
    }
}
