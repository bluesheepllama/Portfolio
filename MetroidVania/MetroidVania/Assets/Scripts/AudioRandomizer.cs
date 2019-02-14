using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRandomizer : MonoBehaviour {

	private AudioSource audioSource;

	// Use this for initialization
	void Start () {

		audioSource = GetComponent<AudioSource> ();

	}
	
	public void playRandomSound(List<AudioClip> audioClips) {
		AudioClip clipToPlay = audioClips[Random.Range (0, audioClips.Count)];
		audioSource.clip = clipToPlay;
		audioSource.Play ();
	}


}
