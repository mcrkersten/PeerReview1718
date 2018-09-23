using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

	public Sprite[] sprites;
	private int scoreSize;
	private Rigidbody2D rigid;
	protected List<GameObject> localSprites = new List<GameObject>();

	
	private void FixedUpdate(){

	}

	public void GameEndLock(){

	}

	private void DestroySelf(){

	}

	public void IsPlayed(){

	}

	public int GetScore(){
		return 0;
	}
}
