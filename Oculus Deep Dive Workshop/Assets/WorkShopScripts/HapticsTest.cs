using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HapticsTest : MonoBehaviour
{
    public string m_buttonName;
    public XRNode m_node;
    private InputDevice m_VRController;

    // Start is called before the first frame update
    void Start()
    {
        m_VRController = InputDevices.GetDeviceAtXRNode(m_node);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown(m_buttonName))
        {
            Vibrate(m_VRController, 1, 0.25f);
        }
    }

    void Vibrate(InputDevice controller, float amplitude, float time)
    {
        if (controller.isValid)
        {
            HapticCapabilities hapCap = new HapticCapabilities();
            controller.TryGetHapticCapabilities(out hapCap);

            if(hapCap.supportsImpulse)
            {
                controller.SendHapticImpulse(0, amplitude, time);
            }
        }
    }
}
