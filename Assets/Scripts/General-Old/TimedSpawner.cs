using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSpawner : MonoBehaviour 
{
	[Tooltip("Drag in the prefab to spawn")]
	public GameObject spawnPrefab; 

	[Tooltip("True if spawning is allowed")]
	public bool activated; 

	[Tooltip("How long, in seconds, to wait between spawning each object")]
	public float spawnDelay = 3;

	[Tooltip("The current value of the spawn delay timer. A new object is spawned when spawnTimer==0")]
	public float spawnTimer; 

	[Tooltip("The max number of objects this spawner can have created at any time.")]
	public int maxChildCount; 

	[Tooltip("If true, the spawned object will have a script that destroys the object when it interacts with a SpawnDestroyer")]
	public bool attachSpawnDestroyScript; 

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (spawnTimer > 0)
		{
			spawnTimer -= Time.deltaTime; 
			if (spawnTimer < 0)
			{
				spawnTimer = 0; 

				if (activated && transform.childCount < maxChildCount)
				{
					SpawnObject(); 
					spawnTimer = spawnDelay; 
				}
			}
		}
	}

	void SpawnObject()
	{
		GameObject newObj = Instantiate(spawnPrefab, transform.position, Quaternion.identity);
		newObj.transform.SetParent(transform); 

		// Add the SpawnDestroy component
		if (attachSpawnDestroyScript)
		{
			newObj.AddComponent<SpawnDestroy>(); 
		}
	}
}
