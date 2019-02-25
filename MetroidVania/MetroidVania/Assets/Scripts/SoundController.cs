using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {

	// define the audio clips
	public AudioClip clip1;
	public AudioClip clip2;
	public AudioClip clip3; 
	public AudioClip clip4;

	public AudioSource audio1;
	public AudioSource audio2;
	public AudioSource audio3;
	public AudioSource audio4;

	public AudioSource AddAudio (AudioClip clip, bool loop, bool playAwake, float vol) {

		AudioSource newAudio = gameObject.AddComponent<AudioSource>();

		newAudio.clip = clip;
		newAudio.loop = loop;
		newAudio.playOnAwake = playAwake;
		newAudio.volume = vol;

		return newAudio;
		 
	}

	public void Awake(){
		// add the necessary AudioSources:
		audio1 = AddAudio(clip1, false, false, 0.2f);
		audio2 = AddAudio(clip2, false, false, 0.4f);
		audio3 = AddAudio(clip3, false, false, 0.8f);
		audio4 = AddAudio(clip4, false, false, 0.8f); 
	} 
}
