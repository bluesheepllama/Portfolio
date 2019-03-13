using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : EnemyParent {
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


	private Transform target;
	private Transform fallingSpawnPoint;
	public Transform bulletSpawnPoint;
	public Transform enemiesSpawnPoint;



	public void Start() {
		animatorController = GetComponent<Animator> ();
		//patrolPoints [0] = new Vector2 (transform.position.x,transform.position.y);
		//patrolPoints [1] = new Vector2 (transform.position.x-patrolOffset,transform.position.y);
		//patrolPoints [2] = new Vector2 (transform.position.x+patrolOffset,transform.position.y);
		//patrolpoints.add instead
		patrolPoints [0] = new Vector2 (274f,228f);
		patrolPoints [1] = new Vector2 (295f,228f);
		patrolPoints [2] = new Vector2 (315f,228f);
		timer = Time.deltaTime;
		shootTimer = Time.deltaTime;

		fallingSpawnPoint = GameObject.Find ("BossFallingSpawnPoint").GetComponent<Transform> ();
		target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();

		moveToIndex = 0;
		if (patrolPoints.Count > 0) {
			SetMoveToPoint(patrolPoints [0]);
		}
	}

	public override void Update() {
		timer += Time.deltaTime;
		//Debug.Log ("boss timer " + timer + "move " + move);
		shootTimer += Time.deltaTime;
		if (posCount == 0) {
			if (shootTimer % 2 < .035f) {  // spawns 6 at.35
				Fire();

				Instantiate (instantiatedFallingEnemiesPrefab, fallingSpawnPoint.position, fallingSpawnPoint.rotation);
				Debug.Log ("posCount = 0");
				//bulletSpawnPoint.LookAt (target);

			}
		} else if (posCount == 1) {
			//shootTimer = 0;
			if (shootTimer % 2 < .1f) {  // spawns about 10
				//Instantiate (instantiatedBulletsPrefab, enemiesSpawnPoint.position, enemiesSpawnPoint.rotation);
				Fire();
				Debug.Log ("posCount = 1");
			}

		} else if (posCount == 2) {
			if (shootTimer % 2 < .01f) { //shoot
				Debug.Log ("posCount = 2");
				Instantiate (instantiatedEnemiesPrefab, enemiesSpawnPoint.position, enemiesSpawnPoint.rotation);

			}
		} else {
			Debug.Log ("reset posCount");
			shootTimer = 0;
			posCount = 0;

		}

		if (IsWithinDistance (stopDistance)) {
			//timer += Time.deltaTime;

			//Debug.Log ("boss timer " + timer + "move " + move);
			if (timer > 12f) {
				move = true;
				ContinueMoving ();
				Debug.Log ("make move true");
			}
			if(move) {
				IncrementMoveToPoint ();
				//mover.AccelerateInDirection (GetDirection ());
				//base.Update ();

				//timer = 0;
				//move = true;
			}
		}
		//base.Update ();
		mover.AccelerateInDirection (GetDirection ());

	}

	private void IncrementMoveToPoint() {

		if (patrolPoints.Count == 0) {
			return;
		}
		move = false;
		StopMoving ();
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
		//Destroy (bullet, 6f);

	}


}
