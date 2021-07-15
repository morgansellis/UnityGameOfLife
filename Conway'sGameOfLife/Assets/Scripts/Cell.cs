using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
	public bool bIsAlive;
	public int iNumOfNeighbours = 0;

	public void SetAlive(bool a_IsAlive) {
		bIsAlive = a_IsAlive;

		if (a_IsAlive) {
			GetComponent<SpriteRenderer>().enabled = true;
		} else {
			GetComponent<SpriteRenderer>().enabled = false;
		}
	}
}
