using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] shootablePrefabs;

    [SerializeField] Transform spawnPosition = null;
    [SerializeField] float initDelay = 3f;
    [SerializeField] float shootableSpawnDelay = 2f;
    [SerializeField] float moveSpeed = 5f;

    float nextSpawnTime = 0f;
	// Use this for initialization
	void Start ()
    {
     
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Time.time>=nextSpawnTime)
        {
            Spawn();
        }
		
	}

    public void Spawn()
    {
        nextSpawnTime = Time.time + shootableSpawnDelay;
        int randomPrefab = Random.Range(0, 3);
        IShootable shootable;
        shootable = Instantiate(shootablePrefabs[randomPrefab], spawnPosition.position, Quaternion.identity).GetComponent<IShootable>();
        shootable.Move(Vector3.down,moveSpeed, ForceMode.Impulse);
    

        
    }
}
