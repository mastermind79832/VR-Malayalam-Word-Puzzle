using UnityEngine;

public class PathramScript : MonoBehaviour
{

	public ParticleSystem ParticleSystem;

	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.parent.TryGetComponent(out InteractableWord Letter))
		{
			GameManager.instance.DroppedWord(Letter.LetterSound);
			ParticleSystem.Play();
			Destroy(Letter.gameObject);
		}
	}
}
