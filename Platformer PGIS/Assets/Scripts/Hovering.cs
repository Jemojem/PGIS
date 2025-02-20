using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hovering : MonoBehaviour
{
    [SerializeField] private float height = 2f;
    [SerializeField] private float loopDuration = 3f;
    private void Start()
    {
        transform.DOMoveY(transform.position.y + height, loopDuration)
            .SetEase(Ease.InOutCubic)
            .SetLoops(-1, LoopType.Yoyo)
            .SetLink(gameObject);
    }
}
