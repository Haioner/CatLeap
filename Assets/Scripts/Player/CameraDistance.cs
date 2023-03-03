using UnityEngine;
using Cinemachine;

public class CameraDistance : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cm;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float lerpSpeed = 1f;
    private float m_initialOrthographicSize;

    private void Start()
    {
        m_initialOrthographicSize = cm.m_Lens.OrthographicSize;
    }

    private void Update()
    {
        CameraDistanceVelocity();
    }

    private void CameraDistanceVelocity()
    {
        float cameraMultiplier = m_initialOrthographicSize + (rb.velocity.magnitude / 5f);
        if (cameraMultiplier > 5f && cameraMultiplier < 10f)
            cm.m_Lens.OrthographicSize = Mathf.Lerp(cm.m_Lens.OrthographicSize, cameraMultiplier, Time.deltaTime * lerpSpeed);
    }
}
