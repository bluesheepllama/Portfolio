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


	public GameObject instantiatedEnemy;



	public SnailEnemyController snailController;
	public SnailEnemyController wormController;
	public FlyEnemyController flyController;
	public BeeEnemyController notTheBeesController;
	public ScorpionEnemyController scorpionController;
	public RhinoBeetleController rhinoBeetleController;
	public CaterpillarEnemyController caterpillarController;



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

	private float coolDown = -300;
	private float timer;
	private bool isTriggered = false;

	void Update () {
		timer = Time.time;
	}

	void Start() {
		maxSnails = snailPositions.Count;//~~~~~~~~~~~~~
	}
	//game object and number for every enemy
	// only problem is if they dont kill the enemies they will trigger more
	private void OnTriggerEnter2D(Collider2D collider) {

		if(timer - coolDown > 300) { 
			isTriggered = true;
			coolDown = Time.time;
			Debug.Log ("enemy trigger");
			//if(coolDown )
			if (collider.tag == "Player") {

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

				flyController.target = collider.transform;
				if (flyController && maxFlies > 0) {
					for (flyNumberIndex = 0; flyNumberIndex < maxFlies; flyNumberIndex++) {
						//flyController.maxSeeDistance = flySeeDistance;
						instantiatedEnemy = (GameObject)Instantiate (flyEnemy, flyPositions [flyNumberIndex], Quaternion.identity);
						Destroy (instantiatedEnemy, 300);
					}
					//		GameObject bulletObject = Instantiate (bulletPrefab[currentWeaponIndex], firePoint.position, firePoint.rotation);

				}
				//notTheBeesController.target = collider.transform;
				if (notTheBeesController && maxBees > 0) {
					for (beeNumberIndex = 0; beeNumberIndex < maxBees; beeNumberIndex++) {
						notTheBeesController.patrolOffset = beeoffset [beeNumberIndex];
						instantiatedEnemy = (GameObject)Instantiate (beeEnemy, beePositions [beeNumberIndex], Quaternion.identity);
						Destroy (instantiatedEnemy, 300);
					}
				}
				//scorpionController.target = collider.transform;
				if (scorpionController && maxScorpions > 0) {
					for (scorpionNumberIndex = 0; scorpionNumberIndex < maxScorpions; scorpionNumberIndex++) {
						scorpionController.patrolOffset = scorpionoffset [scorpionNumberIndex];
						instantiatedEnemy = (GameObject)Instantiate (scorpionEnemy, scorpionPositions [scorpionNumberIndex], Quaternion.identity);
						Destroy (instantiatedEnemy, 300);
					}
				}
				//rhinoBeetleController.target = collider.transform;
				if (rhinoBeetleController && maxRhinoBeetles > 0) {
					for (rhinoBeetleNumberIndex = 0; rhinoBeetleNumberIndex < maxRhinoBeetles; rhinoBeetleNumberIndex++) {
						rhinoBeetleController.patrolOffset = rhinoBettleoffset [rhinoBeetleNumberIndex];
						instantiatedEnemy = (GameObject)Instantiate (rhinoBeetleEnemy, rhinoBeetlePositions [rhinoBeetleNumberIndex], Quaternion.identity);
						Destroy (instantiatedEnemy, 300);
					}
				}
				//caterpillarController.target = collider.transform;
				if (caterpillarController && maxcaterpillars > 0) {
					for (caterpillarNumberIndex = 0; caterpillarNumberIndex < maxcaterpillars; caterpillarNumberIndex++) {
						caterpillarController.patrolOffset = caterpillaroffset [caterpillarNumberIndex];
						instantiatedEnemy = (GameObject)Instantiate (caterpillarEnemy, caterpillarPositions [caterpillarNumberIndex], Quaternion.identity);
						Destroy (instantiatedEnemy, 300);
					}
				}

			}
		}
	}

}
