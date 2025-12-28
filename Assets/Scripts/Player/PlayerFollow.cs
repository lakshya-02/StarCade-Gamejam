using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0f, 0f, -10f);
	[SerializeField] private float minX = -5f;

	private void LateUpdate()
	{
		if (target == null)
		{
			return;
		}

		Vector3 desiredPosition = transform.position;
		desiredPosition.x = Mathf.Max(target.position.x + offset.x, minX);
		transform.position = desiredPosition;
	}
}
