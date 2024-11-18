using System.Collections.Generic;
using UnityEngine;

public class Spawnner : MonoBehaviour
{
    [SerializeField] private InteractableWord m_InteractableWordPrefab;
	[SerializeField] private Transform[] m_SpawnPoints;
	[SerializeField] private MalayalamLetterMapSO m_LetterMapSO;

	[SerializeField] private float m_SpawnRate;
	[SerializeField] private float m_Speed;
	[SerializeField] private int m_AdditionalRandomLetterCount;
	[Range(0,1)]
	[SerializeField] private float m_ProbabliltyForSelectedLetterToSpawn;
	public bool IsSpawnning;

	private float m_Timer;
	private Transform m_SpawnPosition;
	private InteractableWord m_NewWord;
	private List<MalayalamLetterSound> m_LetterSoundsToSpawn;

	private void Start()
	{
		Restart();
		IsSpawnning = false ;
	}

	private void Restart()
	{
		m_Timer = 0;
	}

	private void Update()
	{

		if (!IsSpawnning)
			return;

        RunTimer();
		if (m_Timer >= m_SpawnRate)
		{
			Spawn();
			Restart();
		}
	}

	private void Spawn()
	{
		m_SpawnPosition = GetRandomSpawnLocation();
		m_NewWord = Instantiate(m_InteractableWordPrefab, m_SpawnPosition.position, Quaternion.identity);
		m_NewWord.Setup(GetRandomLetter(), m_SpawnPosition.forward, m_Speed);
		//Debug.Log("Spawnned");
	}

	private MalayalamLetter GetRandomLetter()
	{
		if (Random.value <= m_ProbabliltyForSelectedLetterToSpawn)
			return m_LetterMapSO.GetSpecificLetter(m_LetterSoundsToSpawn[Random.Range(0, m_LetterSoundsToSpawn.Count)]);

		return m_LetterMapSO.GetRandomMalayalamLetter();
	}

	private Transform GetRandomSpawnLocation()
	{
		return m_SpawnPoints[Random.Range(0,m_SpawnPoints.Length)];
	}

	private void RunTimer()
	{
		m_Timer += Time.deltaTime;
	}

	public void NewSpawn(List<MalayalamLetterSound> letters)
	{
		m_LetterSoundsToSpawn = letters;
	}

}
