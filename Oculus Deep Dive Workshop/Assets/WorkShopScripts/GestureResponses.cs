using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureResponses : MonoBehaviour
{
    
    public void Spawn(GameObject m_prefab)
    {
        Instantiate(m_prefab, transform.position, transform.rotation);
    }
}
