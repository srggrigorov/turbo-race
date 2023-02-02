using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectSpawner : MonoBehaviour
{
    private Transform _spawnerTransform;
    public GameObject RoadPrefab;
    public List<GameObject> CarsPrefabs;
    public List<GameObject> DesertPrefabs;
    public float[] CarsSpawnsPositionsX;
    public float minCarSpawnTime;
    public float maxCarSpawnTime;
    public float CarsSpeed;
    public float RoadSpeed;

    [HideInInspector] public bool _gameEnded;

    private void Start()
    {
        _spawnerTransform = transform;
        InvokeRepeating("SpawnRoad", 0, 2.89f);
        StartCoroutine(SpawnCar());
        StartCoroutine(SpawnDesertPrefabs());
    }

    private void SpawnRoad()
    {
        Instantiate(RoadPrefab, _spawnerTransform.position, Quaternion.Euler(0, 180, 0));
    }

    private void Update()
    {
        if (_gameEnded)
        {
            StopAllCoroutines();
            CancelInvoke("SpawnRoad");
        }
    }

    private IEnumerator SpawnCar()
    {
        yield return new WaitForSeconds(Random.Range(minCarSpawnTime, maxCarSpawnTime));
        float spawnPosX = CarsSpawnsPositionsX[Random.Range(0, CarsSpawnsPositionsX.Length)];
        Instantiate(CarsPrefabs[Random.Range(0, CarsPrefabs.Capacity)],
                new Vector3(spawnPosX, _spawnerTransform.position.y + 0.45f, _spawnerTransform.position.z),
                Quaternion.Euler(0, spawnPosX > 0 ? 0 : 180, 0)).GetComponent<ObjectMover>()
            .SetSpeed(spawnPosX > 0 ? CarsSpeed - RoadSpeed : RoadSpeed + CarsSpeed);
        StartCoroutine(SpawnCar());
    }

    private IEnumerator SpawnDesertPrefabs()
    {
        yield return new WaitForSeconds(Random.Range(0.1f, 0.7f));
        float spawnPosX = Random.Range(12, 25);
        Instantiate(DesertPrefabs[Random.Range(0, DesertPrefabs.Capacity)],
            new Vector3(Random.Range(0, 2) == 0 ? spawnPosX : -spawnPosX, _spawnerTransform.position.y,
                _spawnerTransform.position.z),
            Quaternion.Euler(0, 180, 0));
        StartCoroutine(SpawnDesertPrefabs());
    }
}