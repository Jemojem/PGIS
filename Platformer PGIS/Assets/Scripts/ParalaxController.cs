using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxController : MonoBehaviour
{
    [SerializeField] Rigidbody2D playerRB;
    
    private List<Paralax> backgrounds = new();

    private void Awake()
    {
        backgrounds.AddRange(GetComponentsInChildren<Paralax>());
    }

    private void Update()
    {
        foreach (var background in backgrounds)
        {
            background.Move(playerRB.velocity.x * 0.001f);
        }
    }
}
