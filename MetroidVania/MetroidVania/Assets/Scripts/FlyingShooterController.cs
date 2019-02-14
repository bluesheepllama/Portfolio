using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingShooterController : EnemyParent {


	public Mover controlledMover;
	//public Jumper controlledJumper;
	public float count;
	public float patrolOffset = 20;

	public Transform target;
	public Animator animator;
	public float maxSeeDistance = 10f;
	public float offset;

	public GameObject bulletPreFab;
	public float fireRate = 2f;
	public float bulletSpeed = 20f;
	public Transform bulletSpawnPoint;
	public Transform turret;
	public Transform enemyTransform;

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

		patrolPoints [0] = new Vector2 (transform.position.x,transform.position.y);
		patrolPoints [1] = new Vector2 (transform.position.x-patrolOffset,transform.position.y);
		patrolPoints [2] = new Vector2 (transform.position.x+patrolOffset,transform.position.y);
		rigidBody = GetComponent<Rigidbody2D> ();
		svf = GetComponent<SpriteVelocityFlipper> ();
		moveToIndex = 0;
		if (patrolPoints.Count > 0) {
			SetMoveToPoint(patrolPoints [0]);
		}
		isoffset = true;
	}

	public override void Update() {

		SpriteVelocityFlipper svf = GetComponent<SpriteVelocityFlipper> ();
		//Debug.Log ("svf in scorpio" + svf.flip);
		if (target) {



			//Transform tempTransform = scorpioTransform;// = new Vector3 (target.transform.position.x,transform.position.y,target.transform.position.z);
			//tempTransform.position = new Vector3 (target.transform.position.x,transform.position.y,target.transform.position.z);

			SetMoveToPoint (target.position);
		}
		if (IsWithinDistance (maxSeeDistance)) { 
			//Debug.Log ("Scorpion following");

			if (animatorController) {
				//animatorController.SetBool ("IsMoving", true);

			}
			patrolPoints [0] = new Vector2 (transform.position.x,transform.position.y);
			patrolPoints [1] = new Vector2 (transform.position.x-patrolOffset,transform.position.y);
			patrolPoints [2] = new Vector2 (transform.position.x+patrolOffset,transform.position.y);

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
		} else if (IsWithinDistance (stopDistance)) {
			//Debug.Log ("Scorpion increment patolling");

			SetMoveToPoint (patrolPoints [0]);
			if (animator) {
				//animatorController.SetBool ("IsMoving", false);
			}
			IncrementMoveToPoint ();

			//TODO-followingline, fixes a wierd bug

			GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, 0f);


		} else {
			//Debug.Log ("Scorpion  patolling");
			SetMoveToPoint (patrolPoints [1]);

			base.Update ();



		}
		//}

		//base.Update ();

	}

	private void Fire() {
		fireTimer = 0f;
		GameObject bullet = Instantiate (bulletPreFab,bulletSpawnPoint.position ,bulletSpawnPoint.rotation) as GameObject;
		bullet.SendMessage ("PassedValue", svf);
		bullet.SendMessage ("PassedValue2", followPlayer);

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
