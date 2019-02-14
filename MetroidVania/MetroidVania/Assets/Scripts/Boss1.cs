using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : EnemyParent {

	public Mover controlledMover;
	//public Jumper controlledJumper;
	public float count;
	public float patrolOffset = 20;

	public Transform target;
	public Animator animator;
	public float maxSeeDistance = 50f;
	public float offset;

	public GameObject bulletPreFab;
	public float fireRate = 1f;
	public float bulletSpeed = 40f;
	public Transform bulletSpawnPoint;
	public Transform turret;
	public Transform enemyTransform;
	public GameObject intantiatedEnemiesPrefab;

	private float timer = 0;
	private float chargeTimer;
	private float timeToMove = 4f;

	private float fireTimer;

	private SpriteVelocityFlipper svf;
	private Animator animatorController;

	public List<Vector2> patrolPoints;

	private int moveToIndex;

	public bool followPlayer = true;


	public void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position,maxSeeDistance);
	}

	public void Start() {
		animatorController = GetComponent<Animator> ();
		timer = Time.deltaTime;
		patrolPoints [0] = new Vector2 (transform.position.x,transform.position.y);
		patrolPoints [1] = new Vector2 (transform.position.x-patrolOffset,transform.position.y-patrolOffset);
		patrolPoints [2] = new Vector2 (transform.position.x+patrolOffset,transform.position.y);
		patrolPoints [3] = new Vector2 (transform.position.x-patrolOffset,transform.position.y-patrolOffset);

		rigidBody = GetComponent<Rigidbody2D> ();
		svf = GetComponent<SpriteVelocityFlipper> ();
		moveToIndex = 0;
		if (patrolPoints.Count > 0) {
			SetMoveToPoint(patrolPoints [0]);
		}
		isoffset = true;//~~~~~~~~
	}

	public override void Update() {
		timer += Time.deltaTime;
		SpriteVelocityFlipper svf = GetComponent<SpriteVelocityFlipper> ();
		//Debug.Log ("svf in scorpio" + svf.flip);
		if (target) {
			//Debug.Log ("boss timer " + timer);
			if (IsWithinDistance (stopDistance)) {

				if (timer < timeToMove) {
					//SetMoveToPoint (patrolPoints [0]);
					IncrementMoveToPoint ();


				}
				if (timer < timeToMove * 2 && timer > timeToMove) {
					//SetMoveToPoint (patrolPoints [1]);
					IncrementMoveToPoint ();
					Instantiate (intantiatedEnemiesPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

				}
				if (timer < timeToMove * 3 && timer > timeToMove * 2) {
					//SetMoveToPoint (patrolPoints [2]);
					IncrementMoveToPoint ();


				}
				if (timer < timeToMove * 4 && timer > timeToMove * 3) {
					//SetMoveToPoint (patrolPoints [3]);
					IncrementMoveToPoint ();
				    Instantiate (intantiatedEnemiesPrefab,bulletSpawnPoint.position ,bulletSpawnPoint.rotation);
					Instantiate (intantiatedEnemiesPrefab,bulletSpawnPoint.position ,bulletSpawnPoint.rotation);

					timer = 0;
				}
			}
			if (target && bulletPreFab && bulletSpawnPoint) {
				if (IsWithinDistance (maxSeeDistance)) {
					fireTimer += Time.deltaTime;
					if (fireTimer >= fireRate) {
						Fire ();
					}
				}
			}
			base.Update ();


			//Transform tempTransform = scorpioTransform;// = new Vector3 (target.transform.position.x,transform.position.y,target.transform.position.z);
			//tempTransform.position = new Vector3 (target.transform.position.x,transform.position.y,target.transform.position.z);

			//SetMoveToPoint (target.position);
		}
		/*
		if (IsWithinDistance (maxSeeDistance)) { 
			//Debug.Log ("Scorpion following");

			if (animatorController) {
				//animatorController.SetBool ("IsMoving", true);

			}
		}//~~~~~~~~~~

			if (target && bulletPreFab && bulletSpawnPoint) {
				if (IsWithinDistance (maxSeeDistance)) {
					fireTimer += Time.deltaTime;
					if (fireTimer >= fireRate) {
						Fire ();
					}
					//turret.transform.LookAt (target);
				}
			}


			ContinueMoving ();

			base.Update ();
		//} else if (IsWithinDistance (stopDistance)) {
			//Debug.Log ("Scorpion increment patolling");

			SetMoveToPoint (patrolPoints [0]);
			if (animator) {
				//animatorController.SetBool ("IsMoving", false);
			}
			IncrementMoveToPoint ();

			//TODO-followingline, fixes a wierd bug

			GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, 0f);


		//} else {
			//Debug.Log ("Scorpion  patolling");
			SetMoveToPoint (patrolPoints [1]);

			base.Update ();



		//}~~~~~~~~~~~
		//}

		//base.Update ();
*/
	}

	private void Fire() {
		fireTimer = 0f;
		GameObject bullet = Instantiate (bulletPreFab,bulletSpawnPoint.position ,bulletSpawnPoint.rotation) as GameObject;
		bullet.SendMessage ("PassedValue", svf);
		bullet.SendMessage ("PassedValue2", followPlayer);
		Destroy (bullet, 10);

		//forward and left
		Rigidbody2D bulletBody = bullet.GetComponent<Rigidbody2D> ();
		bulletBody.velocity = bullet.transform.forward * bulletSpeed;//maybe not needed
		bullet.transform.LookAt(target);
	}

	private Vector3 GetDirection() {
		return target.position - transform.position;
	}

	private void IncrementMoveToPoint() {

		if (patrolPoints.Count == 0) {
			return;
		}

		moveToIndex++;

		if (moveToIndex >= patrolPoints.Count) {
			moveToIndex = 0;
		}
		SetMoveToPoint (patrolPoints [moveToIndex]);
	}


}
