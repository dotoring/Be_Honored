using UnityEngine;

public class PatternSound : MonoBehaviour
{
	[SerializeField] AudioClip clip;

	private void OnEnable()
	{
		Player.Instance.audioSource.PlayOneShot(clip);
	}
}
