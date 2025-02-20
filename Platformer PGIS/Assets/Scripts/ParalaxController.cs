using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxController : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float multipliyer = 0.001f;
    
    private List<Paralax> backgrounds = new();
    private Vector3 lastPlayerPos, movement;

    private void Awake()
    {
        backgrounds.AddRange(GetComponentsInChildren<Paralax>());
        lastPlayerPos = player.position;
    }

    private void Update()
    {
        movement = player.position - lastPlayerPos;

        foreach (var background in backgrounds)
        {
            background.Move(movement.x * multipliyer);
        }
        lastPlayerPos = player.position;
    }
}
