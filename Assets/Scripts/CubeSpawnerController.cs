using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeSpawnerController : MonoBehaviour
{
    public static CubeSpawnerController instance;

    Queue<CubeController> cubesQueue = new Queue<CubeController>();

    [SerializeField] private int cubesQueueCapacity = 20;
    [SerializeField] private bool autoQueueGrow = true;

    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private Color[] cubeColors;

    [HideInInspector] public int maxCubeNumber; //4096 (2^12)

    private int maxPower = 12;

    private Vector3 defaultSpawnPos;

    private void Awake()
    {
        instance = this;

        defaultSpawnPos = transform.position;

        maxCubeNumber = (int)Mathf.Pow(2, maxPower);

        InitializeCubesQueue();
    }

    private void InitializeCubesQueue()
    {
        for (int i = 0; i < cubesQueueCapacity; i++)
        {
            AddCubeToQueue();
        }
    }
    private void AddCubeToQueue()
    {
        CubeController cube = Instantiate(cubePrefab, defaultSpawnPos, Quaternion.identity, transform)
            .GetComponent<CubeController>();
        
        cube.gameObject.SetActive(false);
        cube.isMainCube = false;
        cubesQueue.Enqueue(cube);
    }

    public CubeController Spawn(int number, Vector3 position)
    {
        if (cubesQueue.Count == 0)
        {
            if (autoQueueGrow)
            {
                cubesQueueCapacity++;
                AddCubeToQueue();
            }

            else
            {
                Debug.LogError("[Cubes Queue]: No more cubes available in the pool");
                return null;
            }
        }

        CubeController cube = cubesQueue.Dequeue();
        cube.transform.position = position;
        cube.SetCubeNumber(number);
        cube.SetCubeColor(GetCubeColor(number));
        cube.gameObject.SetActive(true);

        return cube;
    }

    public CubeController SpawnRandom()
    {
        return Spawn(GenerateRandomCubeNumber(), defaultSpawnPos);
    }

    public void DestroyCube(CubeController cube)
    {
        cube.cubeRb.velocity = Vector3.zero;
        cube.cubeRb.angularVelocity = Vector3.zero;
        cube.transform.rotation = Quaternion.identity;
        cube.isMainCube = false;
        cube.gameObject.SetActive(false);
        cubesQueue.Enqueue(cube);
    }

    public int GenerateRandomCubeNumber()
    {
        return (int) Mathf.Pow(2, Random.Range(1, 6));
    }

    private Color GetCubeColor(int number)
    {
        return cubeColors[(int) (Mathf.Log(number) / Mathf.Log(2)) -1];
    }

}
