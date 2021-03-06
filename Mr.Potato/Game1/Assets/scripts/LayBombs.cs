using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayBombs : MonoBehaviour
{
	[HideInInspector]
	public bool bombLaid = false;       // Whether or not a bomb has currently been laid.
	public int bombCount = 0;           // How many bombs the player has.
	public AudioClip bombsAway;         // Sound for when the player lays a bomb.
	public GameObject bomb;             // Prefab of the bomb.


	void Update()
	{
		// If the bomb laying button is pressed, the bomb hasn't been laid and there's a bomb to lay...
		if (Input.GetButtonDown("Fire2") && !bombLaid && bombCount > 0)
		{
			// Decrement the number of bombs.
			bombCount--;

			// Set bombLaid to true.
			bombLaid = true;

			// Play the bomb laying sound.
			AudioSource.PlayClipAtPoint(bombsAway, transform.position);

			// Instantiate the bomb prefab.
			Instantiate(bomb, transform.position, transform.rotation);
		}
	}
}
