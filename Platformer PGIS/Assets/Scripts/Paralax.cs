using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    [SerializeField] float paralaxEffect;

    public void Move(float direction)
    {
        transform.position += Vector3.right * direction * paralaxEffect;
    }
}
