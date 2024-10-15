using UnityEngine;
using System.Collections;
using System;

public class ArrowDirection : MonoBehaviour
{
 
    private float angle;

  
    private Vector2 velocity;

    
    private Vector2 topLeftScreenPoint;

 
    private Vector2 bottomRightScreenPoint;

  
    private Vector2 arrowPosition;

    
    private Vector2 offset = new Vector2(8, 8);

   
    private bool xIn, yIn;

    
      Rigidbody2D arrowRigidbody;

     GameManager gameManager;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Start()
    {
        // Ensure there is a main camera
        Camera cam = Camera.main;
        if (cam == null)
        {
            Debug.LogError("No camera found with the tag 'MainCamera'.");
            return;
        }

        // Calculate the top-left screen point
        topLeftScreenPoint = cam.ScreenToWorldPoint(new Vector2(0, Screen.height));
        // Calculate the bottom-right screen point
        bottomRightScreenPoint = cam.ScreenToWorldPoint(new Vector2(Screen.width, 0));

        // Get the Rigidbody2D of the arrow
        arrowRigidbody = GetComponent<Rigidbody2D>();
        if (arrowRigidbody == null)
        {
            Debug.LogError("Rigidbody2D is not attached to the GameObject.");
        }
    }
     
    void Update()
    {
        // Ensure the arrow's Rigidbody2D is assigned
        if (arrowRigidbody == null)
        {
            arrowRigidbody = GetComponent<Rigidbody2D>();
            if (arrowRigidbody == null)
            {
                Debug.LogError("Rigidbody2D is not attached to the GameObject.");
                return; // Exit the method to prevent further errors
            }
        }

        // Get the velocity of the arrow
        velocity = arrowRigidbody.velocity;
        if (velocity.magnitude != 0 && !arrowRigidbody.isKinematic)
        {
            // Calculate the angle of the arrow based on its velocity
            angle = Mathf.Atan2(velocity.x, -velocity.y) * Mathf.Rad2Deg + 180;
            // Rotate the arrow to align with its velocity
            transform.rotation = Quaternion.AngleAxis(angle, transform.forward);
            // Check if the arrow is still within the screen bounds
            CheckArrowBounds();
        }
    }

    /// <summary>
    /// Checks if the arrow is within the screen bounds.
    /// </summary>
    void CheckArrowBounds()
    {
        // Get the position of the arrow
        arrowPosition = transform.position;

        // Check if the arrow's x and y positions are within the screen bounds (with some offset)
        xIn = arrowPosition.x >= topLeftScreenPoint.x - offset.x && arrowPosition.x <= bottomRightScreenPoint.x + offset.x;
        yIn = arrowPosition.y >= bottomRightScreenPoint.y - offset.y && arrowPosition.y <= topLeftScreenPoint.y + offset.y;

        // If the arrow is out of bounds, create a new one and destroy the current arrow
        if (!(xIn && yIn))
        {
            // Create a new arrow if the BowController instance exists
            if (BowController.instance != null)
            {
                BowController.instance.CreateArrow();
            }
            else
            {
                Debug.LogError("BowController instance is not set.");
            }

            // Destroy the current arrow
            Destroy(gameObject);

            // Check the number of remaining arrows via GameManager
            if (gameManager != null)
            {
                gameManager.CheckArrowsNumber();
            }
            else
            {
                Debug.LogError("GameManager instance is not set.");
            }
        }
    }
}
