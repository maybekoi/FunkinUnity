using UnityEngine;

public class CamFollow : MonoBehaviour
{
	public Transform targetBF;

	public Transform targetEnemy;

	public Transform targetCenter;

	public Transform target;

	public bool FollowBF;

	public float smoothSpeed = 0.125f;

	private void Update()
	{
		if (!FollowBF)
		{
			target = targetEnemy;
		}
		else if (FollowBF)
		{
			target = targetBF;
		}
		Vector2 b = target.position;
		Vector2 vector = Vector2.Lerp(base.transform.position, b, smoothSpeed * Time.deltaTime);
		base.transform.position = new Vector3(vector.x, vector.y, -10f);
	}
}
