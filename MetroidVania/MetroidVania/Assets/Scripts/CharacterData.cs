using System;

[Serializable]

public class CharacterData
{
	//save Vector 3 for location
	public string characterName;
	public float power;
	public int bullets;

	//in player controller
	public bool haveWebShot;
	public bool haveScatterShot;
	public bool haveWallClimb;
	public bool haveVenomShot;
	public bool haveGrapple;
	public bool haveShootThroughWalls;
	public bool haveGrenade;
	public bool haveDoubleJump ;
	public bool haveShrink;
	public bool shrinkEnabled;
	public int maxMissileCount;
	public int missileCount;
	//(max and count for all other weopons)
	public int weaponindex;
	public float playerPosistionX;
	public float playerPosistionY;
	public float playerPosistionZ;

	//in Destructable
	public float hitPoints;
	public float maxHitPoints;

}