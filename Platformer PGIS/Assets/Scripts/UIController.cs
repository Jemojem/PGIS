using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject finishPanel;
    public void ShowRestartScreen()
    {
        finishPanel.SetActive(true);
    }
}
