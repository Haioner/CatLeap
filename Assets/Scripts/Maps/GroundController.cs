using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    [Header("Map Generator")]
    [SerializeField] private Transform finalMapPos;
    [SerializeField] private int distanceBetweenNewMap = 7;

    [Header("Distance")]
    [SerializeField] private Distance signDistance;
    private bool _canCallNewMap = true;
    private Maps maps;

    [Header("Props")]
    [SerializeField] private List<Props> allMapProps = new List<Props>();
    public int maxCoinType = 1;

    private void OnEnable()
    {
        if (signDistance != null)
            signDistance.SetNewDistance();

        _canCallNewMap = true;
        PropActivator();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && _canCallNewMap)
        {
            if (maps == null)
                maps = FindObjectOfType<Maps>();
            maps.EnableNewMap(finalMapPos, distanceBetweenNewMap);
            _canCallNewMap = false;
        }
    }

    public void PropActivator()
    {
        for (int i = 0; i < allMapProps.Count; i++)
        {
            int randNumber = Random.Range(0, 10);
            
            if (randNumber >= 5)
            {
                allMapProps[i].gameObject.SetActive(true);
                allMapProps[i].SetMaxCoinType(maxCoinType);
            }
            else
                allMapProps[i].gameObject.SetActive(false);
        }
    }
}
