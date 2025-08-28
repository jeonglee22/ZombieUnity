using UnityEngine;
using UnityEngine.AI;

public class ItemSpawner : MonoBehaviour
{
	public GameObject[] items;

	private float itemSpawnTime;
	public float itemSpawnIntervalMax = 7f;
	public float itemSpawnIntervalMin = 5f;
	private float itemSpawnInterval;

	public float itemDespawnTime;

	public Transform playerPos;

	public float spawnDistanceMax = 5f;
	public float spawnDistanceMin = 3f;

	private void Start()
	{
		itemSpawnInterval = Random.Range(itemSpawnIntervalMin, itemSpawnIntervalMax);
		itemSpawnTime = Time.time;
	}

	private void Update()
	{
		if (Time.time - itemSpawnTime > itemSpawnInterval)
		{
			var distance = Random.Range(spawnDistanceMin, spawnDistanceMax);
			ItemSpawn(distance);

			itemSpawnTime = Time.time;
			itemSpawnInterval = Random.Range(itemSpawnIntervalMin, itemSpawnIntervalMax);
		}
	}

	private void ItemSpawn(float distance)
	{
		NavMeshHit hit;
		while(true)
		{
			var circlePoint = Random.onUnitSphere * distance;
			if (NavMesh.SamplePosition(playerPos.position + circlePoint, out hit, 1f, NavMesh.AllAreas))
				break;
		}
		var item = items[Random.Range(0, items.Length)];
		var itemSpawned = Instantiate(item, hit.position, Quaternion.identity);
		Destroy(itemSpawned, itemDespawnTime);
	}
}
