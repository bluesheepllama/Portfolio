using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3 : EnemyParent {

	public Mover controlledMover;
	//public Jumper controlledJumper;
	public float count;
	public float patrolOffset = 20;

	private Animator animatorController;

	public List<Vector2> patrolPoints;

	private int moveToIndex;

	//navmesh to move around walls
	private float timer = 0;
	private float shootTimer;
	//private float dropTi
	private bool move = false;
	public float bulletSpeed = 50f;
	private int posCount = 0;
	public GameObject instantiatedFallingEnemiesPrefab;
	public GameObject instantiatedBulletsPrefab;
	public GameObject instantiatedEnemiesPrefab;

	private Rigidbody2D rb;
	private Destructable destructable;
	private Transform target;
	private Transform fallingSpawnPoint;
	public Transform bulletSpawnPoint;
	public Transform enemiesSpawnPoint;

	private bool moveAgain = false;


	public void Start() {
		animatorController = GetComponent<Animator> ();
		destructable = GetComponent<Destructable> ();
		rb = GetComponent<Rigidbody2D> ();

		//as of now make up point mush higher than the top point
		//move fire point to bottom of fly
		patrolPoints [0] = new Vector2 (181.5f,265f);// right to position

		patrolPoints [1] = new Vector2 (181.5f,316f);// up 
		patrolPoints [2] = new Vector2 (285.6f,316f);// right to position

		patrolPoints [3] = new Vector2 (285.6f,342f);// up
		patrolPoints [4] = new Vector2 (376.9f,342f);// right to position

		patrolPoints [5] = new Vector2 (181.5f,342f);// up
		patrolPoints [6] = new Vector2 (181.5f,285f);// right to position

		patrolPoints [7] = new Vector2 (376.9f,342f);// up
		patrolPoints [8] = new Vector2 (376.9f,342f);// right to position



		timer = Time.deltaTime;
		shootTimer = Time.deltaTime;

		fallingSpawnPoint = GameObject.Find ("BossFallingSpawnPoint").GetComponent<Transform> ();
		target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();

		moveToIndex = 0;
		if (patrolPoints.Count > 0) {
			SetMoveToPoint(patrolPoints [0]);
		}
		isFlying = true;// not sure if needed
	}

	public override void Update ()
	{

		/*
		 * choose when to set move to true
		 * 
		 * */
		timer += Time.deltaTime;
		//Debug.Log ("boss timer " + timer + "move " + move);
		shootTimer += Time.deltaTime;
		//if (posCount == 0) {
		if (destructable.hitPoints < destructable.maximumHitpoints / 6 * 5 && destructable.hitPoints > destructable.maximumHitpoints / 6 * 5 - 5 && posCount == 0) {
			//SetMoveToPoint (patrolPoints [0]);
			//Instantiate (instantiatedFallingEnemiesPrefab, fallingSpawnPoint.position, fallingSpawnPoint.rotation);
			Debug.Log ("health pos1");
			posCount = 1;
			//bulletSpawnPoint.LookAt (target);
			//SetMoveToPoint (1);//see if it runs into wall
			if(move == true) {
				SetMoveToPoint(patrolPoints [1]);
				//instantiate enemies
				Instantiate (instantiatedEnemiesPrefab, enemiesSpawnPoint.position, enemiesSpawnPoint.rotation);
				Instantiate (instantiatedEnemiesPrefab, enemiesSpawnPoint.position, enemiesSpawnPoint.rotation);
				Instantiate (instantiatedEnemiesPrefab, enemiesSpawnPoint.position, enemiesSpawnPoint.rotation);
				moveAgain = true;
			}
		} else if (destructable.hitPoints < destructable.maximumHitpoints / 6 * 4 && destructable.hitPoints > destructable.maximumHitpoints / 6 * 4 - 5 && posCount == 1) {
			
			Debug.Log ("posCount =  1- 2");
			if(move == true) {
				//IncrementMoveToPoint(); 
				SetMoveToPoint(patrolPoints [3]);

				moveAgain = true;

			}
			posCount = 2;
			//SetMoveToPoint (patrolPoints [3]);//see if it runs into wall

		} else if (destructable.hitPoints < destructable.maximumHitpoints / 6 * 3 && destructable.hitPoints > destructable.maximumHitpoints / 6 * 3 - 5 && posCount == 2) {
			Debug.Log ("posCount = 2-3");
			//Instantiate (instantiatedEnemiesPrefab, enemiesSpawnPoint.position, enemiesSpawnPoint.rotation);
			if(move == true) {
				//IncrementMoveToPoint(); 
				SetMoveToPoint(patrolPoints [5]);

				moveAgain = true;

			}
			posCount = 0;
			//SetMoveToPoint (patrolPoints [5]);//see if it runs into wall

		} else if (destructable.hitPoints < destructable.maximumHitpoints / 6 * 2 && destructable.hitPoints > destructable.maximumHitpoints / 6 * 2 - 5 && posCount == 0) {
			Debug.Log ("posCount = 2");
			//Instantiate (instantiatedEnemiesPrefab, enemiesSpawnPoint.position, enemiesSpawnPoint.rotation);
			if(move == true) {
				//IncrementMoveToPoint(); 
				SetMoveToPoint(patrolPoints [7]);

				moveAgain = true;

			}
			posCount = 1;
			//SetMoveToPoint (patrolPoints [6]);//see if it runs into wall

		} else if (destructable.hitPoints < destructable.maximumHitpoints / 6 * 1 && destructable.hitPoints > destructable.maximumHitpoints / 6 * 1 - 5 && posCount == 1) {
			Debug.Log ("posCount = 2");
			//Instantiate (instantiatedEnemiesPrefab, enemiesSpawnPoint.position, enemiesSpawnPoint.rotation);
			if(move == true) {
				IncrementMoveToPoint(); 
				moveAgain = true;

			}
			posCount = 2;
			//SetMoveToPoint (patrolPoints [5]);//see if it runs into wall
		}
		if (posCount == 0) {
			if (shootTimer % 2 < .035f) {  // spawns 6 at.35
				Fire ();
			}
		} else if (posCount == 1) {
			if (shootTimer % 2 < .1f) {  // spawns about 10
				Fire ();
			}
		} else if (posCount == 2) {
			if (shootTimer % 2 < .01f) { //shoot
				Fire ();
			}
		}

		if (rb.velocity.y < 3) {
			Vector2 direction = (transform.position);// - collider.transform.position);
			rb.AddForce (direction * 1000);
		}

					/*} else {
			Debug.Log ("reset posCount");
			shootTimer = 0;
			posCount = 0;

		}*/


		if (IsWithinDistance (stopDistance)) {
			//if (timer > 12f) {
				move = true;
				ContinueMoving ();
				Debug.Log ("make move true");
			if (moveAgain) {
				if (posCount == 1) {
					SetMoveToPoint(patrolPoints [2]);
				} else if (posCount == 2) {
					SetMoveToPoint(patrolPoints [4]);
				} else {
					SetMoveToPoint(patrolPoints [6]);
				}
				moveAgain = false;
			}

			//}
			 /*if (move) {
				IncrementMoveToPoint ();
			}*/
		}
		//base.Update ();

		mover.AccelerateInDirection (GetDirection ());
		}

	private void IncrementMoveToPoint() {

		if (patrolPoints.Count == 0) {
			return;
		}
		move = false;
		//StopMoving ();
		moveToIndex++;
		posCount++;
		timer = 0;
		if (moveToIndex >= patrolPoints.Count) {
			moveToIndex = 0;
		}
		SetMoveToPoint (patrolPoints [moveToIndex]);
	}

	private void Fire() {
		GameObject bullet = Instantiate (instantiatedBulletsPrefab,bulletSpawnPoint.position ,bulletSpawnPoint.rotation) as GameObject;
		//bullet.SendMessage ("PassedValue", svf);
		//bullet.SendMessage ("PassedValue2", true);

		//forward and left
		Rigidbody2D bulletBody = bullet.GetComponent<Rigidbody2D> ();
		bulletBody.velocity = bullet.transform.forward * bulletSpeed;//maybe not needed
		bulletSpawnPoint.LookAt (target);
		Destroy (bullet, 4f);

	}

	public override void SetMoveToPoint( Vector2 moveTo )
	{
		if (isFlying == false) {
			currentMoveToPoint = moveTo;
		} else {
			Vector2 temp = moveTo;
			temp = new Vector2 (moveTo.x, moveTo.y);
			currentMoveToPoint = temp;
		}
		//navMeshAgent.destination = moveTo;

	}


	public override Vector2 GetDirection()
	{
		//Debug.Log (transform.position);
		//try to fix flying here~~~~~~~~~~~~~~~~~~~~
		//if (isFlying == true) {
			return currentMoveToPoint - (Vector2)transform.position;
		//} else {
			//Vector2 temp = currentMoveToPoint - (Vector2)transform.position;
			//temp = new Vector2 (temp.x, 0);
			//return temp;
		//}
	}


}