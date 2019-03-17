using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public class TargetSpawner : MonoBehaviour
{
	GameObject targetGOHolder;
	public List<GameObject> targets;

	public float timerDV;
	public float timer;

	public GameObject emptyTarget;

	// Use this for initialization
	void Start()
	{
		targetGOHolder = new GameObject( "Target Holder" );
		targets = new List<GameObject>();

		UnityEngine.Random.InitState( ( int )System.DateTime.Now.Ticks );

		timer = timerDV;
	}

	// Update is called once per frame
	void Update()
	{
		timer -= Time.deltaTime;

		if ( timer <= 0 )
		{
			timer = timerDV;
			SpawnTarget();
		}
	}

	void SpawnTarget()
	{
		Vector3 tempPos = new Vector3();

		string[] res = UnityStats.screenRes.Split( 'x' );
	//	Debug.Log( int.Parse( res[ 0 ] ) + "	" + int.Parse( res[ 1 ] ) );

		tempPos.x = UnityEngine.Random.Range( -10.0f, 10.0f );
		tempPos.y = UnityEngine.Random.Range( -5.0f, 5.0f );

	//	tempPos.x = int.Parse( res[ 0 ] );
	//	tempPos.y = int.Parse( res[ 1 ] );
		tempPos.z = 15;

		GameObject temp = Instantiate( emptyTarget, tempPos, Quaternion.identity );
		temp.transform.parent = targetGOHolder.transform;

		targets.Add( temp );
	}
}
