using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    private List<GameObject> m_InInteractionRange;

    private void Start()
    {
        m_InInteractionRange = new List<GameObject>();
    }

    private void Update()
    {
       //if input for interacting is pressed
         //foreach(GameObject go in m_InInteractionRange)
            //Debug.Log(go.name);
            //TODO - work out how to do the interaction    
            //Interactable interact = go.GetComponent<Interactable>();
            //if(interact != null)
                //call the interact function on the object
                //break;

        if(Input.GetKeyDown(KeyCode.E))
        {
            foreach(GameObject go in m_InInteractionRange)
            {
                Debug.Log(go.name);
                
            }
        }

        

    }

    private void OnTriggerEnter(Collider other)
    {
        if(!m_InInteractionRange.Contains(other.gameObject))
        {
            m_InInteractionRange.Add(other.gameObject);


        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (m_InInteractionRange.Contains(other.gameObject))
        {
            m_InInteractionRange.Remove(other.gameObject);


        }

    }
}
