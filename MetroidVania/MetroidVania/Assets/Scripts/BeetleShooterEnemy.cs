using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleShooterEnemy : EnemyParent {

	public LayerMask rayCastLayerMask;

	public Mover controlledMover;
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
	public Transform beetleTransform;


	private float fireTimer;

	private JumperEnemy jumper;
	private SpriteVelocityFlipper svf;
	private Animator animatorController;
	private Rigidbody2D controlledRigidBody;

	public List<Vector2> patrolPoints;

	private int moveToIndex;


	public void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position,maxSeeDistance);
	}

	public void Start() {
		animatorController = GetComponent<Animator> ();
		controlledRigidBody = GetComponent<Rigidbody2D> ();
		jumper = GetComponent<JumperEnemy> ();

		patrolPoints [0] = new Vector2 (transform.position.x,transform.position.y);
		patrolPoints [1] = new Vector2 (transform.position.x-patrolOffset,transform.position.y);
		patrolPoints [2] = new Vector2 (transform.position.x+patrolOffset,transform.position.y);
		rigidBody = GetComponent<Rigidbody2D> ();
		svf = GetComponent<SpriteVelocityFlipper> ();
		moveToIndex = 0;
		if (patrolPoints.Count > 0) {
			SetMoveToPoint(patrolPoints [0]);
		}
	}

	public override void Update() {

		SpriteVelocityFlipper svf = GetComponent<SpriteVelocityFlipper> ();
		if (target) {




			SetMoveToPoint (target.position);
		}
		if (IsWithinDistance (maxSeeDistance)) { 

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
					turret.LookAt (target);
				}
			}
			if (CanJump() == true) {
				Debug.Log ("beetle jump");

				jumper.Jump ();
			}


			ContinueMoving ();

			base.Update ();
		} else if (IsWithinDistance (stopDistance)) {

			SetMoveToPoint (patrolPoints [0]);
			if (animator) {
				//animatorController.SetBool ("IsMoving", false);
			}
			IncrementMoveToPoint ();
			if (CanJump() == true) {
				Debug.Log ("beetle jump");

				jumper.Jump ();
			}
			//TODO-followingline, fixes a wierd bug

			GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, 0f);


		} else {
			if (CanJump() == true) {
				Debug.Log ("beetle jump");
				jumper.Jump ();
			}
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
		//forward and left
		Rigidbody2D bulletBody = bullet.GetComponent<Rigidbody2D> ();
		bulletBody.velocity = bullet.transform.forward * bulletSpeed;//maybe not needed
		//bullet.transform.LookAt(target);

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

	private bool CanJump() {
		float directionNormal = controlledRigidBody.velocity.x / Mathf.Abs (controlledRigidBody.velocity.x);
		//Vector2 rayCastOrigin = (Vector2)(transform.position + .5f * transform.right * directionNormal) + groundDetector.colliderCenter;
		Vector2 rayCastOrigin = (Vector2)(transform.right * directionNormal);// + groundDetector.colliderCenter;
		float rayCastDistance =  6.5f;
		if (transform.localScale.x < 0)
		{
			Debug.Log ("beetle can jump maybe not");
			return Physics2D.Raycast (transform.position, Vector2.left, rayCastDistance, rayCastLayerMask);
		}
		else 
		{
			Debug.Log ("beetle can jump maybe");

			return Physics2D.Raycast (transform.position, Vector2.right, rayCastDistance,rayCastLayerMask);
		}
	}


}