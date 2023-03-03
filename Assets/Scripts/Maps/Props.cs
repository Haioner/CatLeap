using UnityEngine;

public class Props : MonoBehaviour
{
    [SerializeField] private Prop_Scriptable propScriptable;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject coinsParticle;
    [SerializeField] private CoinsController coinsController;
    private int maxCoinType = 1;
    [SerializeField] private CoinGain coinGain;
    [SerializeField]private int index;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Game_Manager.instance.AddCoin(propScriptable.propType[index].coinValue);

            Instantiate(coinsParticle, transform.position, Quaternion.identity);

            CoinGain m_coinGain = Instantiate(coinGain, transform.position, Quaternion.identity);
            m_coinGain.SetCoinGainText(propScriptable.propType[index].coinValue);

            AudioManager.instance.Play("Coin");

            coinsController.SetAppear();

            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        EnablePropType();
    }

    private void EnablePropType()
    {
        index = Random.Range(0, maxCoinType);
        spriteRenderer.sprite = propScriptable.propType[index].propSprite;
    }

    public void SetMaxCoinType(int value)
    {
        maxCoinType = value;
    }
}
