using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public struct MalayalamLetter
{
    public MalayalamLetterSound LetterSound;
    public Mesh LetterMesh;
}

[CreateAssetMenu(fileName = "MalayalamLetterMap", menuName = "Scriptable Objects/Malayalam Letter Map")]
public class MalayalamLetterMapSO : ScriptableObject
{
    [SerializeField] private MalayalamLetter[] m_MalayalamLetters;

	public MalayalamLetter GetRandomMalayalamLetter()
	{
		return m_MalayalamLetters[UnityEngine.Random.Range(0, m_MalayalamLetters.Length)];
	}

	public MalayalamLetter GetSpecificLetter(MalayalamLetterSound letterSound)
	{
		return Array.Find(m_MalayalamLetters, x => x.LetterSound == letterSound);
	}
}
