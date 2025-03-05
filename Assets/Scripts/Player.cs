using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public int maxHP = 100;
    public int currentHP;
    public TMP_Text hpText;

    private Vector3 position;
    private bool isJumping = false;
    private float jumpStartY;

    private void Start()
    {
        currentHP = maxHP;
        UpdateHPUI();
    }

    private void Update()
    {
        HandleMovement();
        HandleJump();
        RegenerateHealth();
        UpdateHPUI();
    }

    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        position = transform.position;

        // Move left/right
        if (horizontalInput > 0 && position.x < 1f) // Move right
        {
            position.x += moveSpeed * Time.deltaTime;
        }
        else if (horizontalInput < 0 && position.x > -1f) // Move left
        {
            position.x -= moveSpeed * Time.deltaTime;
        }

        transform.position = position;
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            isJumping = true;
            jumpStartY = transform.position.y;
            position.y += jumpForce;
            transform.position = position;
        }

        if (isJumping)
        {
            position.y -= jumpForce * Time.deltaTime;
            if (transform.position.y <= jumpStartY)
            {
                position.y = jumpStartY;
                isJumping = false;
            }
            transform.position = position;
        }
    }

    private void RegenerateHealth()
    {
        if (currentHP < maxHP)
        {
            currentHP += Mathf.FloorToInt(1 * Time.deltaTime);
            currentHP = Mathf.Min(currentHP, maxHP);
        }
    }

    private void UpdateHPUI()
    {
        if (hpText != null)
        {
            hpText.text = "HP: " + currentHP;
        }
    }

    public void TakeDamage()
    {
        currentHP -= 5;
        if (currentHP < 0) currentHP = 0;
    }
}