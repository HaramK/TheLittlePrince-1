using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Asteroid : MonoBehaviour
{
    private IObjectPool<Asteroid> _objectPool;
    public Rigidbody2D rigidBody2D;
    
    [SerializeField] private Vector2 speedRangeX = new Vector2(-5, 5);
    [SerializeField] private Vector2 speedRangeY = new Vector2(-1, -5);
    [SerializeField] private Vector2Int rotationSpeedRange = new Vector2Int(-200, 200);
    [SerializeField] private Vector2 scaleRange = new Vector2(0.1f, 0.5f);

    public void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }
    
    public void SetPool(IObjectPool<Asteroid> objectPool)
    {
        _objectPool = objectPool;
    }
    
    public void ReturnToPool()
    {
        _objectPool.Release(this);
    }
    
    public void Init(Vector2 spawnRangeX, Vector2 spawnRangeY)
    {
        rigidBody2D.position = new Vector3(Random.Range(spawnRangeX.x, spawnRangeX.y), Random.Range(spawnRangeY.x, spawnRangeY.y), 0);
        rigidBody2D.velocity = new Vector2(Random.Range(speedRangeX.x, speedRangeX.y), -Random.Range(speedRangeY.x, speedRangeY.y));
        rigidBody2D.angularVelocity = Random.Range(rotationSpeedRange.x, rotationSpeedRange.y);
        var scale = Random.Range(scaleRange.x, scaleRange.y);
        transform.localScale = new Vector3(scale, scale, 1);
    }
}
