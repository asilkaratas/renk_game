using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour 
{
	private AudioSource audioSource;
	private List<AudioClip> sounds;

	public AudioClip clickSound;

	public AudioClip gameStartSound;
	public AudioClip placedSound;
	public AudioClip notPlacedSound;
	public AudioClip newRoundSound;
	public AudioClip newBestScoreSound;
	public AudioClip gameEndSound;

	void Awake()
	{
		audioSource = GetComponent<AudioSource> ();

	}

	void Start()
	{
		//GameModel.Instance.NewBestScoreCallback += HandleNewBestScoreCallback;
	}

	private void HandleNewBestScoreCallback ()
	{
		PlayNewBestScoreSound();
	}

	public void PlaySound(AudioClip audioClip)
	{
		if(!UserPreferences.Instance.IsMute)
		{
			audioSource.PlayOneShot(audioClip, 0.2f);
		}
	}

	public void PlayNewRoundSound()
	{
		PlaySound(newRoundSound);
	}

	public void PlayGameStartSound()
	{
		PlaySound(gameStartSound);
	}

	public void PlayPlacedSound()
	{
		PlaySound(placedSound);
	}

	public void PlayNotPlacedSound()
	{
		PlaySound(notPlacedSound);
	}

	public void PlayNewBestScoreSound()
	{
		PlaySound(newBestScoreSound);
	}

	public void PlayGameEndSound()
	{
		PlaySound(gameEndSound);
	}

	public void PlayClickSound()
	{
		PlaySound(clickSound);
	}
}
