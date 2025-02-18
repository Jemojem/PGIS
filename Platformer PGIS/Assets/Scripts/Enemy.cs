using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Collider2D platform;

    SpriteRenderer sprite;
    new Collider2D collider;

    private Vector3 leftPoint, rightPoint;
    private bool isGoingRight = true;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
        leftPoint = new Vector3 (platform.bounds.min.x, platform.bounds.max.y, 0);
        rightPoint = new Vector3(platform.bounds.max.x, platform.bounds.max.y, 0);
    }

    private void Start()
    {
        transform.position = new Vector3(platform.bounds.center.x, platform.bounds.max.y + (transform.position.y - collider.bounds.min.y), 0);
    }

    private void Update()
    {
        if (isGoingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        if ((collider.bounds.max.x >= rightPoint.x && isGoingRight)|| (collider.bounds.min.x <= leftPoint.x && !isGoingRight))
        {
            isGoingRight = !isGoingRight;
            transform.rotation = isGoingRight ? Quaternion.identity : Quaternion.Euler(0, 180, 0);
        }
    }
}
