using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Credits : MonoBehaviour
{
    public TextMeshProUGUI TM;

    public void OnMouseDown()
    {
        TM.text = "HELL NO";
    }

    private void OnMouseExit()
    {
        TM.text = "Credits";
    }
}
