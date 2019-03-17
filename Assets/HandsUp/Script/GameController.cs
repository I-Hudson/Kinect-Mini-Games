using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

	public float timer;
	public GameObject text;

	public float huTimer;

	bool playerHandsUp;

	bool playerhandsUpToEarly;
	float playerHandsUpToEarlyTimer;

	bool playerDead;
	public bool gameover;

	float fDeltaTime;

	// Use this for initialization
	void Start()
	{
		playerHandsUp = false;
		playerhandsUpToEarly = false;

		playerHandsUpToEarlyTimer = 2.5f;

		playerDead = true;
		gameover = false;

		Camera.main.clearFlags = CameraClearFlags.SolidColor;
	}

	// Update is called once per frame
	void Update()
	{
		fDeltaTime = Time.deltaTime;

		if ( !KinectManager.Instance.IsUserDetected())
		{
			Reset();
			return;
		}
		
		if ( Input.GetKeyDown( "e" ) )
		{
		//	playerhandsUpToEarly = true;
			HandsUp();
		}

		if ( !playerDead && gameover == false )
		{
			text.GetComponent<Text>().text = "PLAYER HAS WON!!!";

			Reset();

			return;
		}
		else if( playerDead == true && gameover == true )
		{
			Reset();

			return;
		}

		if ( playerhandsUpToEarly )
		{
			playerHandsUpToEarlyTimer -= fDeltaTime;

			if ( playerHandsUpToEarlyTimer <= 0 )
			{
				playerHandsUpToEarlyTimer = 2.5f;

				playerhandsUpToEarly = false;
				text.SetActive( false );
			}

			return;
		}

		if ( !playerHandsUp )
		{
			if ( timer <= 0 )
			{
				if ( !text.activeInHierarchy )
				{
					text.GetComponent<Text>().text = "HANDS UP";

					text.SetActive( true );
				}

				CheckHandsUp();

			}
			else
			{
				timer -= fDeltaTime;
			}
		}
	}

	void Reset()
	{
	//	Debug.Log(KinectManagerDOL.Instance.GetJointPosition( 0, ( int )KinectWrapper.NuiSkeletonPositionIndex.HandRight ));
	//	text.GetComponent<Text>().text = GameObject.Find( "Sphere" ).transform.position.ToString();

		if(Input.GetKeyDown("space"))
		{
			SceneManager.LoadScene( 0 );
		}
	}

	void CheckHandsUp()
	{
		if ( huTimer <= 0 )
		{
			// game over
			GameOver();
		}
		else
		{
			huTimer -= fDeltaTime;
		}

	}

	public void HandsUp()
	{
		if ( !playerhandsUpToEarly && timer <= 0 && gameover == false )
		{
			playerDead = false;

			playerHandsUp = true;
		}
		else
		{
			if ( gameover == false )
			{
				Debug.Log( "Player hands are raised, wait for the 'HANDS UP' text" );

				text.SetActive( true );
				text.GetComponent<Text>().text = "Player hands are raised, wait for the 'HANDS UP' text";

				playerhandsUpToEarly = true;
			}
			else
			{
				// gameover
			}
		}
	}

	void GameOver()
	{
		Camera.main.GetComponent<Camera>().backgroundColor = Color.red;

		playerDead = true;
		gameover = true;

		// push a new button to reset
		text.GetComponent<Text>().text = "Game Over";
	}
}
