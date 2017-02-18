using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ToneManager : MonoBehaviour 
{
	private AudioSource audioSource;
	public AudioClip[] sounds;

	private int[] gameStartSounds = {4, 3, 2, 1};
	private int[] gameEndSounds  = {12, 13, 14, 15};
	
	void Awake()
	{
		audioSource = GetComponent<AudioSource> ();
	}

	public List<AudioClip> GetRandomSounds(int count)
	{
		var randomSounds = new List<AudioClip> ();
		
		//Debug.Log ("maxIndex:" + maxIndex + "startIndex:" + startIndex);
		
		for (var i = 0; i < count; ++i) 
		{
			var index = Random.Range (0, sounds.Length);
			randomSounds.Add(sounds[index]);
		}
		return randomSounds;
	}
	
	public void PlaySound(AudioClip audioClip)
	{
		//Debug.Log ("PlaySound" + Time.time);
		if(!UserPreferences.Instance.IsMute)
		{
			audioSource.PlayOneShot (audioClip);
		}
	}

	public void PlayGameStartSound()
	{
		PlaySoundArray(gameStartSounds);
	}

	public void PlayGameEndSound()
	{
		PlaySoundArray(gameEndSounds);
	}

	private void PlaySoundArray(int[] soundIndices)
	{
		var delayMultiplier = 0.128f;
		for(var i = 0; i < soundIndices.Length; ++i)
		{
			var delay = i * delayMultiplier;
			var index = soundIndices[i];
			//Debug.Log("PlayGameStartSound:index:" + index + " " + gameStartSounds[i]);
			LeanTween.delayedCall(delay, ()=>{
				AudioClip sound = sounds[index];
				PlaySound(sound);
			});
		}
	}

}
