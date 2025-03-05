using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Runtime.CompilerServices;

public class ItemSpawner : MonoBehaviour
{
    public Item itemToSpawn;
    public float speed = 0.5f;
    public Transform parent;
    public float spawnInterval = 1f;

    private CameraComponent cameraComponent;
    private List<Item> items = new List<Item>();

    private void Start()
    {
        cameraComponent = FindObjectOfType<CameraComponent>();
        InvokeRepeating(nameof(BeginSpawn), 1, spawnInterval);
    }

    private void BeginSpawn()
    {
        if (cameraComponent != null)
        {
            // Randomize the x position from -1, 0, or 1
            float randomX = GetRandomLanePosition();
            Vector2 spawnPosition = new Vector2(randomX, 5f); // Set a height above the player
            var spawnedItem = Instantiate(itemToSpawn, parent);
            spawnedItem.transform.position = new Vector3(spawnPosition.x, spawnPosition.y, 0); // Spawn at the random position
            items.Add(spawnedItem); // Add the spawned item to the list
        }
    }

    private float GetRandomLanePosition()
    {
        // Randomly choose between -1, 0, and 1
        int randomLane = Random.Range(0, 3); // Generates 0, 1, or 2
        return randomLane - 1; // Convert to -1, 0, or 1
    }

    private void Update()
    {
        // Use a for loop to avoid modifying the list while iterating
        for (int i = items.Count - 1; i >= 0; i--)
        {
            if (items[i] != null)
            {
                ItemMover(items[i]);
            }
            else
            {
                items.RemoveAt(i); // Remove the item if it has been destroyed
            }
        }
    }

    private void ItemMover(Item item)
    {
        // Convert the vanishing point to Vector3
        Vector3 vanishingPoint3D = new Vector3(cameraComponent.vanishingPoint.x, cameraComponent.vanishingPoint.y, 0);
        
        // Move the item towards the vanishing point
        Vector3 direction = (vanishingPoint3D - item.transform.position).normalized; // Calculate direction to vanishing point
        item.transform.position += direction * (speed * Time.deltaTime); // Move towards the vanishing point
    }
}