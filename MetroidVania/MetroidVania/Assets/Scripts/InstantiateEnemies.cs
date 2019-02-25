using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateEnemies : MonoBehaviour {

	public List<Vector2> snailPositions;
	public List<Vector2> wormPositions;
	public List<Vector3> flyPositions;
	public List<Vector3> beePositions;
	public List<Vector3> scorpionPositions;
	public List<Vector3> rhinoBeetlePositions;
	public List<Vector3> caterpillarPositions;
	public Vector3 boss1Position;



	//temp
	//public Transform flyTemp;

	//

	public GameObject snailEnemy;
	public GameObject wormEnemy;
	public GameObject flyEnemy;
	public GameObject beeEnemy;
	public GameObject scorpionEnemy;
	public GameObject rhinoBeetleEnemy;
	public GameObject caterpillarEnemy;
	public GameObject boss1Enemy;


	public GameObject instantiatedEnemy;



	public SnailEnemyController snailController;
	public SnailEnemyController wormController;
	public FlyEnemyController flyController;
	public BeeEnemyController notTheBeesController;
	public ScorpionEnemyController scorpionController;
	public RhinoBeetleController rhinoBeetleController;
	public CaterpillarEnemyController caterpillarController;

	public Boss1 boss1Controller;


	//snails values
	public List<float> snailoffset;
	public List<float> wormoffset;
	public List<float> beeoffset;
	public List<float> scorpionoffset;
	public List<float> rhinoBettleoffset;
	public List<float> caterpillaroffset;


	//public List<List<Vector2>> snailPatrolPoints; //[i][3]

	//fly values
	public float flySeeDistance;



	private int snailNumberIndex;
	public int maxSnails = 0;
	private int wormNumberIndex;
	public int maxWorms = 0;
	private int flyNumberIndex;
	public int maxFlies = 0;
	private int beeNumberIndex;
	public int maxBees = 0;
	private int scorpionNumberIndex;
	public int maxScorpions = 0;
	private int rhinoBeetleNumberIndex;
	public int maxRhinoBeetles = 0;
	private int caterpillarNumberIndex;
	public int maxcaterpillars = 0;

	private float coolDown = 300;
	private float timer = 300;
	private bool isTriggered = false;

	void Update () {
		timer += Time.deltaTime;//change to time.deltatime and account for the first trigger
	}

	void Start() {
		maxSnails = snailPositions.Count;//~~~~~~~~~~~~~
	}
	//game object and number for every enemy
	// only problem is if they dont kill the enemies they will trigger more
	private void OnTriggerEnter2D(Collider2D collider) {
		//Debug.Log ("instantiate enemies trigger");
		//Debug.Log ("timer: "+timer);
		if(timer  > coolDown) { 
			//timer = 0; // this was bug enemies were causing this
			isTriggered = true;
			//coolDown = Time.time;//~~~~~~~
			//Debug.Log ("enemy trigger: timer > cooldown");
			//if(coolDown )
			if (collider.tag == "Player") {
				timer = 0; // moved to here to fix 


				if (snailController && maxSnails > 0) {
					for (snailNumberIndex = 0; snailNumberIndex < maxSnails; snailNumberIndex++) {
						snailController.patrolOffset = snailoffset [snailNumberIndex];
						Debug.Log ("instatiate snail :" + snailNumberIndex);
						instantiatedEnemy = (GameObject)Instantiate (snailEnemy, snailPositions [snailNumberIndex], Quaternion.identity);
						Destroy (instantiatedEnemy, 300);
					}
				}

				if (wormController && maxWorms > 0) {
					for (wormNumberIndex = 0; wormNumberIndex < maxWorms; wormNumberIndex++) {
						wormController.patrolOffset = wormoffset [wormNumberIndex];
						Debug.Log ("instatiate snail :" + snailNumberIndex);
						instantiatedEnemy = (GameObject)Instantiate (wormEnemy, wormPositions [wormNumberIndex], Quaternion.identity);
						Destroy (instantiatedEnemy, 300);
					}
				}

				if (flyController && maxFlies > 0) {
					flyController.target = collider.transform;

					for (flyNumberIndex = 0; flyNumberIndex < maxFlies; flyNumberIndex++) {
						//flyController.maxSeeDistance = flySeeDistance;
						instantiatedEnemy = (GameObject)Instantiate (flyEnemy, flyPositions [flyNumberIndex], Quaternion.identity);
						Destroy (instantiatedEnemy, 300);
					}
					//		GameObject bulletObject = Instantiate (bulletPrefab[currentWeaponIndex], firePoint.position, firePoint.rotation);

				}
				if (notTheBeesController && maxBees > 0) {
					//notTheBeesController.target = collider.transform;
					Debug.Log ("instatiate bees :" + maxBees);

					for (beeNumberIndex = 0; beeNumberIndex < maxBees; beeNumberIndex++) {
						Debug.Log ("instatiate bees :" + beeNumberIndex);

						notTheBeesController.patrolOffset = beeoffset [beeNumberIndex];
						instantiatedEnemy = (GameObject)Instantiate (beeEnemy, beePositions [beeNumberIndex], Quaternion.identity);
						Destroy (instantiatedEnemy, 300);
					}
				}
				if (scorpionController && maxScorpions > 0) {
					scorpionController.target = collider.transform;
					Debug.Log ("instatiate scorpion max :" + maxScorpions);

					for (scorpionNumberIndex = 0; scorpionNumberIndex < maxScorpions; scorpionNumberIndex++) {
						Debug.Log ("instatiate scorpion :" + scorpionNumberIndex);
						scorpionController.patrolOffset = scorpionoffset [scorpionNumberIndex];
						instantiatedEnemy = (GameObject)Instantiate (scorpionEnemy, scorpionPositions [scorpionNumberIndex], Quaternion.identity);
						Destroy (instantiatedEnemy, 300);
					}
				}
				if (rhinoBeetleController && maxRhinoBeetles > 0) {
					rhinoBeetleController.target = collider.transform;
					Debug.Log ("instatiate rhino max :" + maxRhinoBeetles);

					for (rhinoBeetleNumberIndex = 0; rhinoBeetleNumberIndex < maxRhinoBeetles; rhinoBeetleNumberIndex++) {
						Debug.Log ("instatiate rhino :" + rhinoBeetleNumberIndex);

						rhinoBeetleController.patrolOffset = rhinoBettleoffset [rhinoBeetleNumberIndex];
						instantiatedEnemy = (GameObject)Instantiate (rhinoBeetleEnemy, rhinoBeetlePositions [rhinoBeetleNumberIndex], Quaternion.identity);
						Destroy (instantiatedEnemy, 300);
					}
				}
				if (caterpillarController && maxcaterpillars > 0) {
					caterpillarController.target = collider.transform;
					Debug.Log ("instatiate catarpillar max :" + maxcaterpillars);

					for (caterpillarNumberIndex = 0; caterpillarNumberIndex < maxcaterpillars; caterpillarNumberIndex++) {
						Debug.Log ("instatiate catarpillar :" + caterpillarNumberIndex);

						caterpillarController.patrolOffset = caterpillaroffset [caterpillarNumberIndex];
						instantiatedEnemy = (GameObject)Instantiate (caterpillarEnemy, caterpillarPositions [caterpillarNumberIndex], Quaternion.identity);
						Destroy (instantiatedEnemy, 300);
					}
				}
				if (boss1Controller) {
					//boss1.target = collider.transform;
					Debug.Log ("instatiate boss");

					//for (caterpillarNumberIndex = 0; caterpillarNumberIndex < maxcaterpillars; caterpillarNumberIndex++) {
						//Debug.Log ("instatiate boss :" + caterpillarNumberIndex);

					//boss1Controller.patrolOffset = caterpillaroffset [caterpillarNumberIndex];
					instantiatedEnemy = (GameObject)Instantiate (boss1Enemy, boss1Position, Quaternion.identity);
					Destroy (instantiatedEnemy, 300);
					}

			}
		}
	}

}
