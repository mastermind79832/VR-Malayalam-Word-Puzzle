using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InteractableWord : MonoBehaviour
{
    [SerializeField] private MeshFilter m_MeshFilter;
	[SerializeField] private MeshCollider m_MeshCollider;
	[SerializeField] private Rigidbody m_Rigidbody;

	private bool m_IsGrabbed;
	private Vector3 m_Direction;
	private float m_Speed;

	private MalayalamLetterSound m_LetterSound;
	public MalayalamLetterSound LetterSound { get { return m_LetterSound; } }

	public void Setup(MalayalamLetter Letter, Vector3 forward, float speed)
	{
		m_LetterSound = Letter.LetterSound;

		m_MeshFilter.mesh = Letter.LetterMesh;
		m_MeshCollider.sharedMesh = Letter.LetterMesh;

		m_Direction = forward;
		m_Speed = speed;
		m_Rigidbody.useGravity = false;
		
		m_IsGrabbed = false;

	}

	private void Update()
	{

		if (m_IsGrabbed)
			return;

		m_Rigidbody.linearVelocity = m_Speed * m_Direction;
	}

	public void StartGrab(SelectEnterEventArgs args)
	{
		m_IsGrabbed = true;
	}

	public void StopGrab(SelectExitEventArgs args)
	{
		m_Rigidbody.useGravity = true;
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("End"))
		{
			Destroy(gameObject);
		}
	}
}
