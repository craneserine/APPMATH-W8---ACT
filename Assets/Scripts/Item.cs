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
            spriteRenderer.color = GetRandomColor(); // Assign a random color to the item
        }
        player = FindObjectOfType<Player>(); // Find the player in the scene
    }

    private void Update()
    {
        // Check for collision with the player
        if (IsCollidingWithPlayer())
        {
            player.TakeDamage(); // Damage the player
            Destroy(gameObject); // Destroy the item after collision
        }
    }

    private bool IsCollidingWithPlayer()
    {
        // Check if the item is within a certain range of the player
        return Mathf.Abs(transform.position.x - player.transform.position.x) < 0.5f &&
               Mathf.Abs(transform.position.z - player.transform.position.z) < 0.5f; // Use z-axis for collision
    }

    private Color GetRandomColor()
    {
        // Generate a random color
        float rRand = Random.Range(0f, 1f);
        float gRand = Random.Range(0f, 1f);
        float bRand = Random.Range(0f, 1f);
        return new Color(rRand, gRand, bRand);
    }
}