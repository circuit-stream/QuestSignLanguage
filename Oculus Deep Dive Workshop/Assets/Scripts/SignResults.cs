using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SignResults : MonoBehaviour
{
    public Text m_displayText;

    public void AddToDisplay(string x)
    {
        m_displayText.text += x;
    }
}
