using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public Rigidbody m_RB;
    public BoxCollider m_Collider;
    public Transform m_Player;
    public Transform m_Weapon;

    public float pickUpRange;
    public float dropForwardForce, dropUpwardForce;

    public bool equipped;
    public static bool slotFull;

    private void Start()
    {
        //Setup
        if(!equipped)
        {
            m_RB.isKinematic = false;
            m_Collider.isTrigger = false;
        }
        if (equipped)
        {
            m_RB.isKinematic = true;
            m_Collider.isTrigger = true;
        }
    }
    private void Update()
    {
        //Check if player is in range and "E" is pressed
        Vector3 distanceToPlayer = m_Player.position - transform.position;
        if(!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull)
        {
            PickUp();
        }

        //Press "Q" to drop the gun
        if(equipped && Input.GetKeyDown(KeyCode.Q))
        {
            Drop();
        }

    }

    private void PickUp()
    {
        equipped = true;
        slotFull = true;

        //Make weapon a child of the camera and move it to default position
        transform.SetParent(m_Player);
        transform.position = new Vector3(m_Player.position.x + 0.7f, m_Player.position.y + 1.4f, m_Player.position.z);
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        
        
        
        //Make rigidbosy kinematic
        m_RB.isKinematic = true;
        //Make BoxCollider a trigger
        m_Collider.isTrigger = true;

        //Enable weapon script when I do it 

    }

    private void Drop()
    {
        equipped = false;
        slotFull = false;

        //Set parent to null
        transform.SetParent(null);

        //Make rigidbosy kinematic
        m_RB.isKinematic = false;
        //Make BoxCollider a trigger
        m_Collider.isTrigger = false;

        //Add Force
        m_RB.AddForce(m_Weapon.forward * dropForwardForce, ForceMode.Impulse);
        m_RB.AddForce(m_Weapon.up * dropUpwardForce, ForceMode.Impulse);

    }

}
