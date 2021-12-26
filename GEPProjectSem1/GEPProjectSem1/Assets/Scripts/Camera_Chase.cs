using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Chase : MonoBehaviour
{
    [SerializeField] private Vector3 m_LocalCamOffset; //Distance that the camera is gonna stay in 3 dimensions away from the object that is following
    [SerializeField] private Transform m_TrackedObjectTransform; //Transform of the object that is being followed
    [SerializeField] private float m_AutoCamSpeed = 3f;
    [SerializeField] private float m_CamSpeed = 10f;

    private void LateUpdate()
    {
        if(m_TrackedObjectTransform != null)
        {
            //Intent Cam
            if (Input.GetKey(KeyCode.Mouse2))
            {
                if (Input.GetAxis("Mouse X") > 0)
                {
                    transform.position -= new Vector3(Input.GetAxis("Mouse X") * Time.deltaTime * m_CamSpeed, 0.0f, 0.0f);
                }
                else if (Input.GetAxis("Mouse X") < 0)
                {
                    transform.position -= new Vector3(Input.GetAxis("Mouse X") * Time.deltaTime * m_CamSpeed, 0.0f, 0.0f);
                }

            }
            //AutoCam
            Vector3 toTarget = m_TrackedObjectTransform.position - transform.position; //First we calculate the distance between the camera and the player

            transform.rotation = Quaternion.LookRotation(toTarget, Vector3.up); // We set the rotation of the camera depending on what the distance of the object from the cam is

            float worldYRotInRad = transform.rotation.eulerAngles.y * Mathf.Deg2Rad; //The y rotation of the camera in radians

            Vector3 worldOrbitPoint = transform.position - new Vector3(m_LocalCamOffset.z * Mathf.Sin(worldYRotInRad) + m_LocalCamOffset.x * Mathf.Cos(worldYRotInRad),
                                                                        m_LocalCamOffset.y,
                                                                        m_LocalCamOffset.z * Mathf.Cos(worldYRotInRad) - m_LocalCamOffset.x * Mathf.Sin(worldYRotInRad));
            /*world coordinates version of the x and what an x and z components of the local one turned into world coordinates plus the local coordinates y*/
            Vector3 worldCamOffset = transform.position - worldOrbitPoint;

            //Distance from that world orbit point to the target
            float distToTargetOffset = (m_TrackedObjectTransform.position - worldOrbitPoint).magnitude;

            transform.position = Vector3.MoveTowards(worldOrbitPoint, m_TrackedObjectTransform.position, distToTargetOffset * m_AutoCamSpeed * Time.deltaTime) + worldCamOffset;
        }
    }
}
