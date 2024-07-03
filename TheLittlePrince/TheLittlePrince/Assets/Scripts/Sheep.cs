using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    [SerializeField] private float horizontalForceFactor = 200;
    [SerializeField] private float verticalForceFactor = 200;
    [SerializeField] private float defaultVerticalForce = 150;
    [SerializeField] private float maxSpeed = 5;
    
    Rigidbody2D _rigidBody2D;

    void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }
    
    public void AddForceByInput(Vector2 input)
    {
        var horizontalInput = input.x;
        var verticalInput = input.y;
        
        var horizontalForce = horizontalInput * horizontalForceFactor;
        var verticalForce = verticalInput * verticalForceFactor + defaultVerticalForce;
        
        _rigidBody2D.AddForce(new Vector2(horizontalForce, verticalForce));
        
        var velocity = _rigidBody2D.velocity;
        velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);
        velocity.y = Mathf.Clamp(velocity.y, -maxSpeed, maxSpeed);
        _rigidBody2D.velocity = velocity;
    }
}
