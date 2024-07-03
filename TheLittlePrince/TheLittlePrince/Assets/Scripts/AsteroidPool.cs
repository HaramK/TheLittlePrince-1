using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class AsteroidPool : MonoBehaviour
{
    public GameObject aesteroidPrefab;

    public IObjectPool<Asteroid> pool { get; private set; }

    public void InitPools()
    {
        pool = new ObjectPool<Asteroid>(CreateAsteroid, OnGetAsteroid, OnReleaseAsteroid, OnDestroyAsteroid);
    }

    private Asteroid CreateAsteroid()
    {
        var asteroid = Instantiate(aesteroidPrefab).GetComponent<Asteroid>();
        asteroid.SetPool(pool);
        return asteroid;
    }
    
    private void OnGetAsteroid(Asteroid asteroid)
    {
        asteroid.gameObject.SetActive(true);
    }
    
    private void OnReleaseAsteroid(Asteroid asteroid)
    {
        asteroid.gameObject.SetActive(false);
    }
    
    private void OnDestroyAsteroid(Asteroid asteroid)
    {
        Destroy(asteroid.gameObject);
    }
}
