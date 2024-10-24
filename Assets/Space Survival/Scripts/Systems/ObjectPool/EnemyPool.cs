using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
	public static EnemyPool pool;
	public Enemy enemyPrefab;

	private void Awake()
	{
		pool = this;
	}

	List<Enemy> poolList = new(); // 비활성 에너미 리스트

	public Enemy Pop()
	{
		if (poolList.Count <= 0)  // 재사용할 객체가 비어있을 경우
		{
			Push(Instantiate(enemyPrefab));
		}
		Enemy enemy = poolList[0];
		poolList.Remove(enemy);
		enemy.gameObject.SetActive(true);
		enemy.transform.SetParent(null);
		return enemy;
	}

	public void Push(Enemy enemy)
	{
		poolList.Add(enemy);
		enemy.gameObject.SetActive(false);
		enemy.transform.SetParent(transform, false);
	}
}
