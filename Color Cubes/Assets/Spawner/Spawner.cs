using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] shootablePrefabs;

    [SerializeField] Transform[] spawnPositions = null;
    [SerializeField] float initDelay = 3f;
    [SerializeField] float shootableSpawnDelay = 2f;
    [SerializeField] Vector2 moveSpeedMinMax = new Vector2(1,7);

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

        int randomPrefab = Random.Range(0, shootablePrefabs.Length);
        float randomSpeed = Random.Range(moveSpeedMinMax.x,moveSpeedMinMax.y);
        int randomTransform = Random.Range(0,spawnPositions.Length);
        Vector3 randomPosition = spawnPositions[randomTransform].position;

        IShootable shootable;
        shootable = Instantiate(shootablePrefabs[randomPrefab],randomPosition , Quaternion.identity).GetComponent<IShootable>();
        shootable.Move(Vector3.down,randomSpeed , ForceMode.Impulse);
    

        
    }
}
