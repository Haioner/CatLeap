using UnityEngine;
using TMPro;

public class Distance : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI meterText;
    [SerializeField] private GameObject meterParticle;
    [SerializeField] private float _meters;
    private bool _canGetDistance;

    public void SetNewDistance()
    {
        //Called in GroundController, onEnable
        _meters = transform.position.x / 5;
        meterText.text = FormaterNumber.FormatDistance(_meters);
        _canGetDistance = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && _canGetDistance)
        {
            Game_Manager.instance.SaveBestDistance(_meters);
            AudioManager.instance.Play("Confetti");
            Instantiate(meterParticle, transform.position, Quaternion.identity);
            _canGetDistance = false;
        }
    }
}
