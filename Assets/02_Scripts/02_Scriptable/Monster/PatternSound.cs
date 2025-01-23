using UnityEngine;

public class PatternSound : MonoBehaviour
{
	[SerializeField] AudioClip clip;
	[SerializeField] float volume;

	private void OnEnable()
	{
		Player.Instance.audioSource.PlayOneShot(clip,volume);
	}
}
