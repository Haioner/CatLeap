using System.Collections;
using UnityEngine;

public class SaveLastPositions : MonoBehaviour
{
    [SerializeField] private GameObject playerPos;
    [SerializeField] private GameObject sawMovement;
    private Vector3 m_lastPlayerPos;
    private Vector3 m_lastSawPos = new Vector3(-10.8f, 0, 0);

    public void SaveLastPlayerPosition()
    {
        m_lastPlayerPos = playerPos.transform.position;
        m_lastSawPos = sawMovement.transform.position;

    }

    public void PlayerContinue_ADRewards()
    {
        if (playerPos == null) return;
        playerPos.transform.position = m_lastPlayerPos;
        sawMovement.transform.position = m_lastSawPos;
        playerPos.GetComponent<InfinitePlayerController>().ResetFlip();
        PlayerHealth.canDeath = true;
        Options.instance.ContinueReward();
    }
}
