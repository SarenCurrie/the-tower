using UnityEngine;
using System.Collections;

public class BossBehaviour : Enemy
{

	public void RotateToFacePlayer()
	{
		Vector3 relativePlayerPos = GetRelativePlayerPosition();
		float angle = Mathf.Atan2(relativePlayerPos.y, relativePlayerPos.x) * Mathf.Rad2Deg + 270;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

	private Vector3 GetRelativePlayerPosition()
	{
		return GameManager.GetPlayer().GetComponent<Transform>().position - transform.position;
	}

}
