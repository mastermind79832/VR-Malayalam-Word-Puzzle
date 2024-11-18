using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorFallController : MonoBehaviour
{
    [SerializeField] private List<MeshRenderer> m_FloorList;
    [SerializeField] private Material m_WarningMateraial;
    [SerializeField] private float m_FloorDropRate;
    [SerializeField] private float m_WarningTime;
    [SerializeField] private float m_RevealTime;

    private float m_Timer;
    private bool m_IsRunning;
    private Material m_NormalMat;

    public void StartTimer()
    {
        m_IsRunning = true;
    }

	private void Start()
	{
        m_Timer = 0;
        m_NormalMat = m_FloorList[0].sharedMaterial;
	}

	private void Update()
	{
		if (!m_IsRunning)
            return;

        m_Timer += Time.deltaTime;

        if (m_Timer >= m_FloorDropRate || m_FloorList.Count == 0)
        {
            m_IsRunning = false;
            m_Timer = 0;

            MeshRenderer floor = m_FloorList[Random.Range(0,m_FloorList.Count)];

            StartCoroutine(DropFloor(floor));
        }
	}

	private IEnumerator DropFloor(MeshRenderer mesh)
	{
        m_FloorList.Remove(mesh);
		mesh.sharedMaterial = m_WarningMateraial;
        yield return new WaitForSeconds(m_WarningTime);
        mesh.gameObject.SetActive(false);
		yield return new WaitForSeconds(m_RevealTime);
        mesh.gameObject.SetActive(true);
        mesh.sharedMaterial = m_NormalMat;
        m_FloorList.Add(mesh);

		m_IsRunning = true;
	}
} 
