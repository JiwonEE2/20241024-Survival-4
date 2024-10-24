using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
	// target�� �������ִ� �Լ��� �ʿ��ϰڴ�.
	public Enemy targetEnemy;            // ����ü�� ���ؾ� �� ���⿡ �ִ� ���
	public bool isFiring;

	public Projectile projectilePrefab; // ����ü ������
	public ProjectilePool projPool;     // Projectile Prefab���� ������� ���� ������Ʈ�� �����ϴ� ������ƮǮ

	public float damage;                // ������
	public float projectileSpeed;       // ����ü �ӵ�
	public float projectileScale;       // ����ü ũ��
	public float shotInterval;          // ���� ����

	public int projectileCount;         // ����ü ���� 1~5
	public float innerInterval;
	[Tooltip("���� Ƚ���Դϴ�\n������ ������ ���� ��� 0")]
	public int pierceCount;             // ���� Ƚ��

	protected virtual void Start()
	{
		StartCoroutine(FireCoroutine());
	}

	protected virtual void Update()
	{
		// ���� ����� ���� Ž���Ͽ� ��� ������ ���� ��
		targetEnemy = null;   // ������� ������ ��
		float targetDistance = float.MaxValue;    // ������ �Ÿ�

		if (GameManager.Instance.enemies.Count == 0)
		{
			// �߻� ������ ����
			isFiring = false;
		}
		else
		{
			isFiring = true;
		}

		foreach (Enemy enemy in GameManager.Instance.enemies)
		{
			float distance = Vector3.Distance(enemy.transform.position, transform.position);
			if (distance < targetDistance)    // ������ ���� ������ ������
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

	// ���� �ڷ�ƾ
	protected virtual IEnumerator FireCoroutine()
	{
		while (true)
		{
			yield return new WaitForSeconds(shotInterval);
			if (isFiring)
			{
				// 1. ����ü ������ �ö󰡸� 0.05�� �������� ����ü ������ŭ �߻� �ݺ�
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
