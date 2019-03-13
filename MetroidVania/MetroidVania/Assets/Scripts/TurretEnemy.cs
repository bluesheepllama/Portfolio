using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : EnemyParent {


	//public Jumper controlledJumper;
	public float count;

	public Transform target;
	public Animator animator;
	public float maxSeeDistance = 50f;

	public GameObject bulletPreFab;
	public float fireRate = .5f;
	private float bulletSpeed = 80f;
	public Transform bulletSpawnPoint;
	public Transform turret;
	//public Transform scorpioTransform;

	private float fireTimer;

	//private SpriteVelocityFlipper svf;
	private Animator animatorController;




	public void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position,maxSeeDistance);
	}

	public void Start() {
		animatorController = GetComponent<Animator> ();
		target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();

		//rigidBody = GetComponent<Rigidbody2D> ();
		//svf = GetComponent<SpriteVelocityFlipper> ();

	}

	public override void Update() {

		SpriteVelocityFlipper svf = GetComponent<SpriteVelocityFlipper> ();
		//Debug.Log ("svf in scorpio" + svf.flip);
		if (target) {



			//Transform tempTransform = scorpioTransform;// = new Vector3 (target.transform.position.x,transform.position.y,target.transform.position.z);
			//tempTransform.position = new Vector3 (target.transform.position.x,transform.position.y,target.transform.position.z);

		}
		if (IsWithinDistance (maxSeeDistance)) { 
			//Debug.Log ("Scorpion following");

			if (animatorController) {
				//animatorController.SetBool ("IsMoving", true);

			}

			if (target && bulletPreFab && bulletSpawnPoint) {
				if (IsWithinDistance (maxSeeDistance)) {
					fireTimer += Time.deltaTime;
					if (fireTimer >= fireRate) {
						Fire ();
					}
					turret.LookAt (target);
				}
			}



			//base.Update ();
			/*} else if (IsWithinDistance (stopDistance)) {
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

			base.Update ();



		}
		//}
*/
			//base.Update ();
		}
	}

	/*
	 * fireTimer = 0f;
		GameObject bullet = Instantiate (bulletPreFab,bulletSpawnPoint.position ,bulletSpawnPoint.rotation) as GameObject;

		Rigidbody bulletBody = bullet.GetComponent<Rigidbody> ();
		bulletBody.velocity = bullet.transform.forward * bulletSpeed;
	}
	 * */

	private void Fire() {
		fireTimer = 0f;
		GameObject bullet = Instantiate (bulletPreFab,bulletSpawnPoint.position ,bulletSpawnPoint.rotation) as GameObject;
		//bullet.SendMessage ("PassedValue", svf);
		//forward and left
		Rigidbody2D bulletBody = bullet.GetComponent<Rigidbody2D> ();
		bulletBody.velocity = bullet.transform.forward * bulletSpeed;//maybe not needed
		//bullet.transform.LookAt(target);

	}

	private Vector3 GetDirection() {
		return target.position - transform.position;
	}
		

}
