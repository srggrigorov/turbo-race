using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class ObjectSpawner : MonoBehaviour
{
    private Transform _spawnerTransform;
    public GameObject RoadPrefab;
    public GameObject FuelPrefab;
    public float minFuelSpawnTime;
    public float maxFuelSpawnTime;
    public List<GameObject> CarsPrefabs;
    public List<GameObject> DesertPrefabs;
    public float[] CarsSpawnsPositionsX;
    public float minCarSpawnTime;
    public float maxCarSpawnTime;
    public float CarsSpeed;
    public float RoadSpeed;

    [Space(10)] public GameObject EndGameMenu;

    [HideInInspector] public bool _gameEnded;

    [HideInInspector] public static ObjectSpawner Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        _spawnerTransform = transform;
        InvokeRepeating("SpawnRoad", 0, 2.89f);
        StartCoroutine(SpawnCar());
        StartCoroutine(SpawnDesertPrefabs());
        StartCoroutine(SpawnFuel());
        
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
            EndGameMenu.SetActive(true);
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
    private IEnumerator SpawnFuel() 
    {
        yield return new WaitForSeconds(Random.Range(minFuelSpawnTime, maxFuelSpawnTime));
        float spawnPosX = Random.Range(1, 5);
        Instantiate(FuelPrefab, new Vector3(Random.Range(0, 2) == 0 ? spawnPosX : -spawnPosX, _spawnerTransform.position.y + 0.45f,
        _spawnerTransform.position.z), Quaternion.Euler(0, 180, 0))
        .GetComponent<ObjectMover>().SetSpeed(45f);
        StartCoroutine(SpawnFuel());

    }
}