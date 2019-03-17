using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void Shoot()
	{
		Debug.Log( "Shot has been made" );

		foreach(GameObject go in GameObject.FindObjectOfType<TargetSpawner>().targets)
		{
			float distanceToCamera = 25f;

			Vector3 posJoint = KinectManager.Instance.GetRawSkeletonJointPos( KinectManager.Instance.GetPlayer1ID(), (int)KinectWrapper.NuiSkeletonPositionIndex.HandRight );

			// 3d position to depth
			Vector2 posDepth = KinectManager.Instance.GetDepthMapPosForJointPos( posJoint );

			// depth pos to color pos
			Vector2 posColor = KinectManager.Instance.GetColorMapPosForDepthPos( posDepth );

			float scaleX = ( float )posColor.x / KinectWrapper.Constants.ColorImageWidth;
			float scaleY = 1.0f - ( float )posColor.y / KinectWrapper.Constants.ColorImageHeight;

			Vector3 vPosOverlay = Camera.main.ViewportToWorldPoint( new Vector3( scaleX, scaleY, distanceToCamera ) );

			if ( Vector3.Distance( go.transform.position, vPosOverlay ) <  4 )
			{
				Debug.Log( "Target has been hit!!!" );

				GameObject.FindObjectOfType<TargetSpawner>().targets.Remove( go );
				Destroy( go );

				break;
			}
		}
	}
}
