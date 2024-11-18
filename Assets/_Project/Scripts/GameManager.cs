using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private MalayalamWordSO m_MalayalamWordSO;
    [SerializeField] private Spawnner m_Spawnner;
    [SerializeField] private MeshRenderer m_LetterVisual;
    [SerializeField] private Transform m_WordDisplayPosition;
    [SerializeField] private MalayalamLetterMapSO m_letterMap;
	[SerializeField] private float m_LetterOffset;
    [SerializeField] private FloorFallController m_FloorFallController;

    [SerializeField] private ScoreManager m_ScoreManager;

    [SerializeField] private Material m_NormalMat;
    [SerializeField] private Material m_GreenMat;
    [SerializeField] private Material m_RedMat;

    [SerializeField] private float m_ErrorTime;
    private Coroutine m_ShowWrongletter;
    private WaitForSeconds m_ErrorWait;

    private List<MalayalamLetterSound> m_SelectedWord;
    private List<MeshRenderer> m_LetterVisuals;
    private int CurrentIndex = 0;


	private void Awake()
	{
		if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        m_ErrorWait = new(m_ErrorTime);
	}

	[ContextMenu("Start Game")]
    public void StartGame()
	{
        m_ScoreManager.ResetScore();
        m_FloorFallController.StartTimer();
		GetNewWord();
	}

	private void GetNewWord()
	{
		m_SelectedWord = new();
		CurrentIndex = 0;
		foreach (MalayalamLetterSound letterSound in m_MalayalamWordSO.GetRandomMalayalamWord().letterSounds)
		{
			m_SelectedWord.Add(letterSound);
		}

		m_Spawnner.NewSpawn(m_SelectedWord);
		m_Spawnner.IsSpawnning = true;

		SpawnLetterVisual();
	}

	private void SpawnLetterVisual()
	{
        if (m_LetterVisuals != null && m_LetterVisuals.Count > 0)
        {
            foreach (MeshRenderer letterVisual in m_LetterVisuals)
            {
                Destroy(letterVisual.gameObject);
            }
        }
        else
            m_LetterVisuals = new();

        m_LetterVisuals.Clear();
        int i = 0;
        foreach (MalayalamLetterSound letter in m_SelectedWord)
        {
            i++;
            MeshRenderer letterVisual = Instantiate(m_LetterVisual, m_WordDisplayPosition);
            letterVisual.transform.localPosition = (m_SelectedWord.Count - i) * m_LetterOffset * Vector3.up;
            letterVisual.GetComponent<MeshFilter>().mesh = m_letterMap.GetSpecificLetter(letter).LetterMesh;
			m_LetterVisuals.Add(letterVisual);
        }
	}

	public void DroppedWord(MalayalamLetterSound letterSound)
    {
        if (letterSound == m_SelectedWord[CurrentIndex])
        {
            if (m_ShowWrongletter != null)
                StopCoroutine(m_ShowWrongletter);
            
            //Change material to green;
            m_LetterVisuals[CurrentIndex].sharedMaterial = m_GreenMat;
            CurrentIndex++;

            if (CurrentIndex == m_LetterVisuals.Count)
            {
				NextRound();
            }
        }
        else
        {
           m_ShowWrongletter = StartCoroutine(ShowWrongLetter());
        }

    }

	private void NextRound()
	{
		m_ScoreManager.IncrementScore();
        GetNewWord();
	}

	private IEnumerator ShowWrongLetter()
	{
		m_LetterVisuals[CurrentIndex].sharedMaterial = m_RedMat;
		yield return m_ErrorWait;
		m_LetterVisuals[CurrentIndex].sharedMaterial = m_NormalMat;
	}
}
