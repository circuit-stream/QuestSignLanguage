using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public struct Sign
{
    public string name;
    public List<Vector3> boneInfo;
    public UnityEvent onSignRecognized;
}

public class SignRecognizer : MonoBehaviour
{
    public bool m_recording;
    public string m_hand;
    public float m_threshold = 0.05f;
    public List<Sign> m_Signs = new List<Sign>();

    private OVRSkeleton m_skeleton;
    private List<OVRBone> m_bonePoint = new List<OVRBone>();
    private Sign m_lastSign;

    // Start is called before the first frame update
    void Start()
    {
        m_skeleton = GetComponentInChildren<OVRSkeleton>();
        m_bonePoint = new List<OVRBone>(m_skeleton.Bones);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && m_recording)
        {
            SaveSign();
        }

        Sign currentSign = Recognized();

        bool hasRecognized = !currentSign.Equals(new Sign());

        if (hasRecognized && !currentSign.Equals(m_lastSign))
        {
            m_lastSign = currentSign;
            currentSign.onSignRecognized.Invoke();
        }

    }

    void SaveSign()
    {
        Sign g = new Sign();
        g.name = "NewSign";
        List<Vector3> info = new List<Vector3>();

        foreach (var bone in m_bonePoint)
        {
            info.Add(m_skeleton.transform.InverseTransformPoint(bone.Transform.position));
        }

        g.boneInfo = info;
        m_Signs.Add(g);
    }

    Sign Recognized()
    {
        Sign currentSign = new Sign();
        float currentmin = Mathf.Infinity;

        foreach (var Sign in m_Signs)
        {
            float sumDistance = 0;
            bool isDiscarded = false;
            for (int i = 0; i < m_bonePoint.Count; i++)
            {
                Vector3 currentData = m_skeleton.transform.InverseTransformPoint(m_bonePoint[i].Transform.position);
                float distance = Vector3.Distance(currentData, Sign.boneInfo[i]);
                if (distance > m_threshold)
                {
                    isDiscarded = true;
                    break;
                }
                sumDistance += distance;
            }

            if (!isDiscarded && sumDistance < currentmin)
            {
                currentmin = sumDistance;
                currentSign = Sign;
            }
        }

        return currentSign;
    }
}
