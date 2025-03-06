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
    public float minHorizontal = -1f; // Minimum x position for spawning
    public float maxHorizontal = 1f;  // Maximum x position for spawning
    public float minVertical = 5f;     // Minimum y position for spawning
    public float maxVertical = 5f;     // Maximum y position for spawning
    public float removeAtDistance = -5f; // Distance at which to remove items
    public float spawnInterval = 1f;   // Time between spawns

    private List<Item> items = new List<Item>();
    private CameraComponent cameraComponent;

    private void Start()
    {
        cameraComponent = FindObjectOfType<CameraComponent>();
        InvokeRepeating(nameof(BeginSpawn), 1, spawnInterval);
    }

    private void BeginSpawn()
    {
        if (cameraComponent != null)
        {
            Vector3 spawnPosition = GetRandomLocation();
            var spawnedItem = Instantiate(itemToSpawn, parent);
            spawnedItem.transform.position = spawnPosition; // Spawn at the random position
            items.Add(spawnedItem); // Add the spawned item to the list
        }
    }

    private Vector3 GetRandomLocation()
    {
        float xRand = Random.Range(minHorizontal, maxHorizontal);
        float yRand = Random.Range(minVertical, maxVertical);
        float zRand = 10f; // Spawn items at a distance in the z-axis

        return new Vector3(xRand, yRand, zRand); // Return the random position
    }

    private void Update()
    {
        for (int i = items.Count - 1; i >= 0; i--)
        {
            Item item = items[i];

            if (item == null)
            {
                items.RemoveAt(i);
                continue;
            }

            ItemMover(item);

            if (item.transform.position.z < removeAtDistance)
            {
                RemoveItem(item);
            }
        }
    }

    private void RemoveItem(Item item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
        }

        if (item != null)
        {
            Destroy(item.gameObject);
        }
    }

    private void ItemMover(Item item)
    {
        // Move the item towards the player
        Vector3 direction = new Vector3(0, 0, -speed * Time.deltaTime); // Move towards the player
        item.transform.position += direction; // Update the item's position
    }
}