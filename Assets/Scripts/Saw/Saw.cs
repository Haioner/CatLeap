using System.Collections;
using UnityEngine;

public class Saw : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] private float rotSpeed = 45f;
    [SerializeField] private float desacelerateSpeed = 100f;
    [SerializeField] private ParticleSystem particleSaw;
    private float initialParticleEmission;
    private bool canDesacelerate;
    private float initRotSpeed;
    private bool rotateTrigger;

    [Header("Damage")]
    [SerializeField] private int damage = 1;
    [SerializeField] private CircleCollider2D circleCollider;

    private void Start()
    {
        initRotSpeed = rotSpeed;
        initialParticleEmission = particleSaw.emission.rateOverTime.constant;
    }

    public void Update()
    {
        if (!rotateTrigger) return;
        RotateSaw();
    }

    private void RotateSaw()
    {
        //Rotate the saw
        transform.RotateAround(transform.position, Vector3.forward, rotSpeed * Time.deltaTime * 10);

        //Desacelerate the saw
        if (canDesacelerate && rotSpeed > 0)
            rotSpeed -= desacelerateSpeed * Time.deltaTime;
    }

    private void OnBecameVisible()
    {
        rotateTrigger = true;
        var emission = particleSaw.emission;
        emission.rateOverTime = initialParticleEmission;
    }

    private void OnBecameInvisible()
    {
        rotateTrigger = false;
        var emission = particleSaw.emission;
        emission.rateOverTime = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Hit player
            canDesacelerate = true;
            StartCoroutine(nameof(CollisionState));
            Game_Manager.instance.playerHealth.TookDamage(damage);
            Game_Manager.instance.ADD_ADCount();
        }
    }

    IEnumerator CollisionState()
    {
        circleCollider.enabled = false;
        yield return new WaitForSeconds(3f);
        circleCollider.enabled = true;
        ResetRotation();
    }

    private void ResetRotation()
    {
        canDesacelerate = false;
        rotSpeed = initRotSpeed;
    }
    
}
