using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int currentHealth = 1;
    [SerializeField] private GameObject bloodParticle;
    public static bool canDeath = true;

    private void Start()
    {
        canDeath = true;
    }

    private void Update()
    {
        CheckPositionY();
    }

    private void CheckPositionY()
    {
        if (transform.position.y <= -8f && canDeath)
        {
            TookDamage(100);
            Game_Manager.instance.ADD_ADCount();
            canDeath = false;
        }
    }

    public void TookDamage(int damageValue)
    {
        AudioManager.instance.Play("Hit");
        currentHealth -= damageValue;
        CheckDeath();
        Instantiate(bloodParticle, transform.position, Quaternion.identity);
    }

    private void CheckDeath()
    {
        if(currentHealth <= 0)
            Options.instance.PauseDead();
    }
}
