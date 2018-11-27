using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Transform target = null;
    public GameObject[] shootablePrefabs;
    [SerializeField] Vector3 directionOffset = Vector3.zero;
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

        IMoveable moveable;
        Quaternion randomRotation = Quaternion.Euler(Random.Range(0,90), Random.Range(0, 90), Random.Range(0, 90));
        moveable = Instantiate(shootablePrefabs[randomPrefab],randomPosition , randomRotation).GetComponent<IMoveable>();

        Vector3 randomXDirection = new Vector3(Random.Range(-directionOffset.x, directionOffset.x), directionOffset.y, directionOffset.z);
        Vector3 direction = (target.position - randomPosition).normalized;
        direction += randomXDirection;

        moveable.Move(direction,randomSpeed , ForceMode.Impulse);
        Vector3 randomTorque = new Vector3(Random.Range(5, 10), Random.Range(5, 10), Random.Range(5, 10));
        float torqueMagnitude = Random.Range(2,10);
        moveable.Rotate(randomTorque,torqueMagnitude,ForceMode.Force);
    

        
    }
}
