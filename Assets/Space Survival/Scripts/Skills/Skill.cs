using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
	// target을 지정해주는 함수가 필요하겠다.
	public Enemy targetEnemy;            // 투사체가 향해야 할 방향에 있는 대상
	public bool isFiring;

	public Projectile projectilePrefab; // 투사체 프리팹
	public ProjectilePool projPool;     // Projectile Prefab으로 만들어진 게임 오브젝트를 관리하는 오브젝트풀

	public float damage;                // 데미지
	public float projectileSpeed;       // 투사체 속도
	public float projectileScale;       // 투사체 크기
	public float shotInterval;          // 공격 간격

	public int projectileCount;         // 투사체 개수 1~5
	public float innerInterval;
	[Tooltip("관통 횟수입니다\n무제한 관통을 원할 경우 0")]
	public int pierceCount;             // 관통 횟수

	protected virtual void Start()
	{
		StartCoroutine(FireCoroutine());
	}

	protected virtual void Update()
	{
		// 가장 가까운 적을 탐색하여 사격 방향을 정할 때
		targetEnemy = null;   // 대상으로 지정된 적
		float targetDistance = float.MaxValue;    // 대상과의 거리

		if (GameManager.Instance.enemies.Count == 0)
		{
			// 발사 절차를 생략
			isFiring = false;
		}
		else
		{
			isFiring = true;
		}

		foreach (Enemy enemy in GameManager.Instance.enemies)
		{
			float distance = Vector3.Distance(enemy.transform.position, transform.position);
			if (distance < targetDistance)    // 이전에 비교한 적보다 가까우면
			{
				targetDistance = distance;
				targetEnemy = enemy;
			}
		}

		if (targetEnemy == null)
		{
			return;
		}
		else
		{
			transform.up = targetEnemy.transform.position - transform.position;
		}
	}

	// 공격 코루틴
	protected virtual IEnumerator FireCoroutine()
	{
		while (true)
		{
			yield return new WaitForSeconds(shotInterval);
			if (isFiring)
			{
				// 1. 투사체 개수가 올라가면 0.05초 간격으로 투사체 개수만큼 발사 반복
				for (int i = 0; i < projectileCount; i++)
				{
					Fire();
					yield return new WaitForSeconds(innerInterval);
				}
			}
		}
	}

	protected virtual void Fire()
	{
		Projectile proj =
			//Instantiate(projectilePrefab, transform.position, transform.rotation);
			projPool.Pop();

		proj.transform.SetPositionAndRotation(transform.position, transform.rotation);

		proj.damage = damage;
		proj.moveSpeed = projectileSpeed;
		proj.transform.localScale *= projectileScale;
		proj.pierceCount = pierceCount;
	}
}
