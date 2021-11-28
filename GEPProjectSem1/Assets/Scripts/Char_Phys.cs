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
    private float m_JumpForce = 1f;
    private bool m_IsGrounded;
    public bool m_IsAttacking = false;
    
    

    /// <summary>
    /// The attached Rigidbody
    /// </summary>
    private Rigidbody m_RB;

    public PlayerState m_PlayerState;
    private Animator m_Animator;
    private AnimationController m_PlayerAnimationController;
    private Char_Weapon_Controller m_Character_Weapon_Controller;
    private CapsuleCollider m_PlayerCollider;
    [SerializeField] private LayerMask m_Ground;
    
    

    private void Awake()
    {
        //Gets the attached rigidbody component
        m_RB = GetComponent<Rigidbody>();

        m_PlayerState = PlayerState.IDLE;

        m_Animator = GetComponent<Animator>();

        m_PlayerAnimationController = GetComponent<AnimationController>();

        m_Character_Weapon_Controller = GetComponent<Char_Weapon_Controller>();

        m_PlayerCollider = GetComponent<CapsuleCollider>();
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
            
             if (m_RB.velocity.y == 0f && !m_IsAttacking)
             {
                if(m_Character_Weapon_Controller.m_EquipedWeapon == PickUpWeaponType.HAMMER)
                {
                    m_PlayerAnimationController.ChangeAnimationState(m_PlayerState = PlayerState.HAMMER_RUN);
                }
                else if(m_Character_Weapon_Controller.m_EquipedWeapon == PickUpWeaponType.SPEAR)
                {
                    m_PlayerAnimationController.ChangeAnimationState(m_PlayerState = PlayerState.SPEAR_RUN);
                }
                else if (m_Character_Weapon_Controller.m_EquipedWeapon == PickUpWeaponType.STAFF)
                {
                    m_PlayerAnimationController.ChangeAnimationState(m_PlayerState = PlayerState.STAFF_RUN);
                }
                else
                {
                    m_PlayerAnimationController.ChangeAnimationState(m_PlayerState = PlayerState.RUN);
                }
                
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
        m_Animator.SetFloat("HammerRunSpeed", m_RB.velocity.magnitude / m_MaxSpeed);
        m_Animator.SetFloat("SpearRunSpeed", m_RB.velocity.magnitude / m_MaxSpeed);
        m_Animator.SetFloat("StaffRunSpeed", m_RB.velocity.magnitude / m_MaxSpeed);

        if (m_RB.velocity == Vector3.zero && !m_IsAttacking)
        {
            if(m_Character_Weapon_Controller.m_EquipedWeapon == PickUpWeaponType.HAMMER)
            {
                m_PlayerAnimationController.ChangeAnimationState(m_PlayerState = PlayerState.HAMMER_IDLE);
            }
            else if(m_Character_Weapon_Controller.m_EquipedWeapon == PickUpWeaponType.SPEAR)
            {
                m_PlayerAnimationController.ChangeAnimationState(m_PlayerState = PlayerState.SPEAR_IDLE);
            }
            else if (m_Character_Weapon_Controller.m_EquipedWeapon == PickUpWeaponType.STAFF)
            {
                m_PlayerAnimationController.ChangeAnimationState(m_PlayerState = PlayerState.STAFF_IDLE);
            }
            else
            {
                m_PlayerAnimationController.ChangeAnimationState(m_PlayerState = PlayerState.IDLE);
            }
        }


        //Jumping system
        
        if(IsGrounded() && Input.GetButton("Jump"))
        {
            m_PlayerAnimationController.ChangeAnimationState(m_PlayerState = PlayerState.JUMP);
            m_RB.AddForce(Vector3.up * m_JumpForce, ForceMode.Impulse);
        }


        //Velocity Cap
        if(m_RB.velocity.magnitude > m_MaxSpeed)
        {
            m_RB.velocity = m_RB.velocity.normalized * m_MaxSpeed;
        }

        //Attack animations
        NormalAttackAxeOrMace();
    }

    private bool IsGrounded()
    {
        m_IsGrounded = Physics.CapsuleCast(m_PlayerCollider.bounds.center, m_PlayerCollider.bounds.size, m_PlayerCollider.radius, Vector3.down, m_PlayerCollider.bounds.extents.y + 0.1f, m_Ground);
        return m_IsGrounded;
    }

    public void NormalAttackAxeOrMace()
    {
        if (m_Character_Weapon_Controller.m_EquipedWeapon == PickUpWeaponType.AXE || m_Character_Weapon_Controller.m_EquipedWeapon == PickUpWeaponType.MACE)
        {
            if (Input.GetButton("Fire1"))
            {

                m_Animator.SetTrigger("Normal_Attack_Axe_Mace");


            }
        }
        else if (m_Character_Weapon_Controller.m_EquipedWeapon == PickUpWeaponType.HAMMER)
        {
            if (Input.GetButton("Fire1"))
            {

                m_Animator.SetTrigger("Normal_Attack_Hammer");


            }
        }
        else if (m_Character_Weapon_Controller.m_EquipedWeapon == PickUpWeaponType.SPEAR)
        {
            if (Input.GetButton("Fire1"))
            {

                m_Animator.SetTrigger("Normal_Attack_Spear");


            }
        }
        else if (m_Character_Weapon_Controller.m_EquipedWeapon == PickUpWeaponType.STAFF)
        {
            if (Input.GetButton("Fire1"))
            {

                m_Animator.SetTrigger("Normal_Attack_Staff");


            }
        }
    }
}
 

public enum PlayerState
{
    IDLE,
    HAMMER_IDLE,
    SPEAR_IDLE,
    STAFF_IDLE,
    RUN,
    HAMMER_RUN,
    SPEAR_RUN,
    STAFF_RUN,
    JUMP,
    NORMAL_ATTACK_AXE_MACE,
    NORMAL_ATTACK_HAMMER,
    NORMAL_ATTACK_SPEAR,
    NORMAL_ATTACK_STAFF,
    DEATH
}