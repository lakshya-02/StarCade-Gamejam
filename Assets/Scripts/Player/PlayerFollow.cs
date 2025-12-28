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

		Vector3 desiredPosition = target.position + offset;
		desiredPosition.x = Mathf.Max(desiredPosition.x, minX);
		transform.position = desiredPosition;
	}
}
