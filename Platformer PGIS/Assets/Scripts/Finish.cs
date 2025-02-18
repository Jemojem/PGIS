using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Finish : MonoBehaviour
{
    [SerializeField] UnityEvent onFinish;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        onFinish.Invoke();
    }
}
