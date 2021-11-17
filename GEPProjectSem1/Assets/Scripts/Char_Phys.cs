using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Char_Phys : MonoBehaviour
{
    
    [SerializeField] [Min(0f)] private float m_TurnSmoothTime;
    
    [SerializeField] private Transform cam; //Reference to our camera

    private float m_Speed = 1f;
    private float m_StopingForce = 10f;
    private float m_MaxSpeed = 8f;
    private float m_RotationSpeed;
    private float m_JumpForce = 10f;
    private bool m_IsGrounded;
    private bool m_IsAttacking = false;
    

    /// <summary>
    /// The attached Rigidbody
    /// </summary>
    private Rigidbody m_RB;

    public PlayerState m_PlayerState;
    private Animator m_Animator;
    private AnimationController m_PlayerAnimationController;
    private Char_Weapon_Controller m_Character_Weapon_Controller;
    
    

    private void Awake()
    {
        //Gets the attached rigidbody component
        m_RB = GetComponent<Rigidbody>();

        m_PlayerState = PlayerState.IDLE;

        m_Animator = GetComponent<Animator>();

        m_PlayerAnimationController = GetComponent<AnimationController>();

        m_Character_Weapon_Controller = GetComponent<Char_Weapon_Controller>();

        
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

            if(m_RB.velocity.y == 0f )
            {
                m_PlayerAnimationController.ChangeAnimationState(m_PlayerState = PlayerState.RUN);
            }

            

        }
        else 
        {
            Vector3 lateralVel = Vector3.ProjectOnPlane(m_RB.velocity, Vector3.up);
            if (lateralVel.magnitude > 0.1f)
            {
                
                m_RB.AddForce(-(lateralVel.normalized * m_StopingForce * Time.fixedDeltaTime), ForceMode.Impulse);
                
            }
            else
            {
                m_RB.velocity = m_RB.velocity.y * Vector3.up;
            }

        }

        m_Animator.SetFloat("RunSpeed", m_RB.velocity.magnitude / m_MaxSpeed);

        if (m_RB.velocity == Vector3.zero && !m_IsAttacking)
        {
            m_PlayerAnimationController.ChangeAnimationState(m_PlayerState = PlayerState.IDLE);
        }


        //Jumping system
        
        if(IsGrounded() && Input.GetButtonDown("Jump"))
        {
            
            m_RB.AddForce(Vector3.up * m_JumpForce, ForceMode.Impulse);
            m_PlayerAnimationController.ChangeAnimationState(m_PlayerState = PlayerState.JUMP);

        }


        //Velocity Cap
        if(m_RB.velocity.magnitude > m_MaxSpeed)
        {
            m_RB.velocity = m_RB.velocity.normalized * m_MaxSpeed;
        }

        //Attack animations

        if (m_Character_Weapon_Controller.m_EquipedWeapon == WeaponType.AXE || m_Character_Weapon_Controller.m_EquipedWeapon == WeaponType.MACE)
        {
            if (Input.GetButton("Fire1"))
            {

                if (!m_IsAttacking)
                {
                    m_IsAttacking = true;
                    StartCoroutine(Attack());

                }
                
            }
        }

    }

    private void AttackComplete()
    {
        m_IsAttacking = false;
    }

    private bool IsGrounded()
    {
        m_IsGrounded = Physics.Raycast(gameObject.transform.position, gameObject.transform.up, gameObject.GetComponent<CapsuleCollider>().bounds.extents.y + 0.1f);
        return m_IsGrounded;
    }

    private IEnumerator Attack()
    {
        m_PlayerAnimationController.ChangeAnimationState(m_PlayerState = PlayerState.ATTACK);
        yield return new WaitForSeconds(1.4f);
        m_IsAttacking = false;
    }

}
 

public enum PlayerState
{
    IDLE,
    RUN,
    JUMP,
    ATTACK
}