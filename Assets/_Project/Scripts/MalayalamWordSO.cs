using System;
using UnityEngine;

[Serializable]
public struct MalayalamWord
{
	public MalayalamLetterSound[] letterSounds;
}

[CreateAssetMenu(fileName = "MalayalamWordSO", menuName = "Scriptable Objects/Malayalam Word")]
public class MalayalamWordSO : ScriptableObject
{
	public MalayalamWord[] MalayalamWords;

	public MalayalamWord GetRandomMalayalamWord()
	{
		return MalayalamWords[UnityEngine.Random.Range(0, MalayalamWords.Length)];
	}
}
