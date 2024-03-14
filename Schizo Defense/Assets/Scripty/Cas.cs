using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Cas : MonoBehaviour
{
    [SerializeField] TMP_Text m_Text;
    void Update()
    {
       m_Text.text = DateTime.Now.ToString();
    }
}
