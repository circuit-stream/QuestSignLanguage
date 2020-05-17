using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRRigManager : MonoBehaviour
{
    public CapsuleCollider m_RigCollider;
    public Transform m_camTransform;

    void Update()
    {
        Vector3 colliderPosition = m_camTransform.localPosition;
        m_RigCollider.height = m_camTransform.localPosition.y;
        colliderPosition.y = m_RigCollider.height/2f;
        m_RigCollider.center = colliderPosition;
    }
}
