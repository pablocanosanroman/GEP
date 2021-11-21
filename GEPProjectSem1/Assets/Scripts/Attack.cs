using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.root.CompareTag("Enemy"))
        {
            other.transform.root.GetComponent<Attack_Controller>().UpdateInteractObject(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.root.CompareTag("Enemy"))
        {
            other.transform.root.GetComponent<Attack_Controller>().UpdateInteractObject(null);
        }
    }
}
