using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Player player;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = GetRandomColor();
        }
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (IsCollidingWithPlayer())
        {
            player.TakeDamage();
            Destroy(gameObject);
        }
    }

    private bool IsCollidingWithPlayer()
    {
        return Mathf.Abs(transform.position.x - player.transform.position.x) < 0.5f &&
               Mathf.Abs(transform.position.y - player.transform.position.y) < 0.5f;
    }
    private Color GetRandomColor()
    {
        var rRand = Random.Range(0f, 1f);
        var gRand = Random.Range(0f, 1f);
        var bRand = Random.Range(0f, 1f);
        return new Color(rRand, gRand, bRand);
    }
}