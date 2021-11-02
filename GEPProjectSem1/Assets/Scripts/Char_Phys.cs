using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Char_Phys : MonoBehaviour
{
    private float m_Speed = 1f;
    private float m_StopingForce = 1f;
    private float m_RotationSpeed;
    private float m_JumpForce = 7f;
    
   
    
    [SerializeField] [Min(0f)] private float m_TurnSmoothTime;
    
    [SerializeField] private Transform cam; //Reference to our camera
    /// <summary>
    /// The attached Rigidbody
    /// </summary>
    private Rigidbody m_RB;
    private void Awake()
    {
        //Gets the attached rigidbody component
        m_RB = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
       
        //gets the direction based on the input
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;

        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y; //Calculates the angle where the character is facing

        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward; //Forward change the rotation into a direction

        if (direction.magnitude >= 0.1f) //if the lenght of this vector is greater or equal to 0.1...
        {

           float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref m_RotationSpeed, m_TurnSmoothTime); // Function to smooth the angle movement
           transform.rotation = Quaternion.Euler(0f, angle, 0f); // Make the character actually rotate

           //Add force to the rigidbody
           m_RB.AddForce(moveDir.normalized * m_Speed, ForceMode.Impulse);
           
        }

        //Jumping system
        Vector3 jumpDirection = new Vector3(0f, Input.GetAxisRaw("Jump"), 0f).normalized;

        if(m_RB.velocity.y == 0)
        {
            m_RB.AddForce(jumpDirection * m_JumpForce, ForceMode.Impulse);
        }



        //else
        //{
        //    if (moveDir.normalized.magnitude == 0)
        //    {
        //        IsMoving = false;
        //    }
        //}


        //if (!IsMoving)
        //{
        //    m_RB.AddForce(-(moveDir.normalized * m_StopingForce * Time.deltaTime), ForceMode.Impulse);

        //    IsMoving = true;
        //}




    }

    

}
