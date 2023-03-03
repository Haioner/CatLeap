using UnityEngine;

public class SawMovement : MonoBehaviour
{
    [SerializeField] private float updateInterval = 0.5f;
    [SerializeField] private Transform playerPos;
    private float m_lastUpdateTime;
    private float m_currentSpeed;

    private void Start()
    {
        m_lastUpdateTime = Time.time;
    }

    void Update()
    {
        if (!Game_Manager.isPlaying) return;

        float currentTime = Time.time;
        // Checa se o intervalo de tempo definido foi atingido
        if (currentTime - m_lastUpdateTime >= updateInterval)
        {
            //Set the speed to: player distance divided by 4
            float dist = Vector2.Distance(playerPos.position, transform.position);
            m_currentSpeed = dist / 4;

            // Atualiza o tempo da última atualização
            m_lastUpdateTime = currentTime;
        }

        //Lerp to player y axis
        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, playerPos.position.y, transform.position.z), 0.5f);

        transform.position += Vector3.right * m_currentSpeed * Time.deltaTime;
    }
}
