using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Sheep sheep;
    [SerializeField] private AsteroidPool asteroidPool;
    
    [SerializeField] private List<Asteroid> asteroids;
    [SerializeField] private int asteroidCount = 10;
    [SerializeField] private Vector2 asteroidSpawnRangeX = new Vector2Int(-20, 20);
    [SerializeField] private Vector2 asteroidSpawnRangeY = new Vector2Int(6, 12);

    private void Awake()
    {
        asteroids = new List<Asteroid>();
        asteroidPool.InitPools();
    }

    private void FixedUpdate()
    {
        var input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        sheep.AddForceByInput(input);
        // 소행성
        var playerPos = player.transform.position;
        for(int i = asteroids.Count - 1; i >= 0; i--)
        {
            // 소행성 좌우 위치보정
            var asteroid = asteroids[i];
            var pos = asteroid.rigidBody2D.position;
            if (pos.x < playerPos.x - 20)
            {
                pos.x = playerPos.x + 20;
            }
            else if (pos.x > playerPos.x + 20)
            {
                pos.x = playerPos.x - 20;
            }
            // 소행성 제거
            if(asteroids[i].transform.position.y < playerPos.y - 10)
            {
                asteroids[i].ReturnToPool();
                asteroids.RemoveAt(i);
            }
        }
        // 소행성 생성
        var spawnOffsetX = new Vector2(playerPos.x, playerPos.x);
        var spawnOffsetY = new Vector2(playerPos.y, playerPos.y);
        for(int i = asteroids.Count; i < asteroidCount; i++)
        {
            var asteroid = asteroidPool.pool.Get();
            asteroid.Init(asteroidSpawnRangeX + spawnOffsetX, asteroidSpawnRangeY + spawnOffsetY);
            asteroids.Add(asteroid);
        }
    }
}
