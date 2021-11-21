using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Controller : MonoBehaviour
{
    private Attack m_InteractObject;
    private Char_Phys m_PlayerPhysics;

    public void UpdateInteractObject(Attack obj)
    {
        if(m_PlayerPhysics.m_PlayerState == PlayerState.NORMALATTACK)
        {
            m_InteractObject = obj;

            if (m_InteractObject == null)
            {
                m_InteractObject = obj;
            }
        }
    }
}
