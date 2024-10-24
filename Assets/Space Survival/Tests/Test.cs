using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Test : MonoBehaviour
{
	public Vector2 minMaxDist;
	public Vector2 spawnPos;
	public GameObject target;
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		//circleSize = Random.Range(minMaxDist.x, minMaxDist.y);
		spawnPos = Random.insideUnitCircle * Random.Range(minMaxDist.x, minMaxDist.y);
		Instantiate(target, spawnPos, Quaternion.identity);
	}
}
