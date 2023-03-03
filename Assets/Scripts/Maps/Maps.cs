using System.Collections.Generic;
using UnityEngine;

public class Maps : MonoBehaviour
{
    [Header("Maps")]
    public List<GameObject> mapsList = new List<GameObject>();
    private Vector2 minMaxMap = new Vector2(1, 4);
    private int mapPoolCount;
    private int lastNumber;
    private int lastMap = -1;
    private int currentMap;
    public int maxMap;

    private void Awake()
    {
        LoadMaxMap();
        SetMapsPropsTypeByIndex();
    }

    //Maps
    public void DisablePreviousMap()
    {
        if (lastMap != -1)
            mapsList[lastMap].SetActive(false);
    }

    public void EnableNewMap(Transform finalPos, float addedDistance)
    {
        DisablePreviousMap();
        lastMap = lastNumber;
        AddMinMaxMap();
        //Enable and move random ground to finalPos.x + distance
        currentMap = GetRandom((int)minMaxMap.x, (int)minMaxMap.y);
        mapsList[currentMap].transform.position = new Vector3(finalPos.position.x + addedDistance, 0, 0);
        mapsList[currentMap].SetActive(true);
    }

    private int GetRandom(int min, int max)
    {
        //Set unique random number (non-repetitive)
        int rand = Random.Range(min, max);
        while (rand == lastNumber)
            rand = Random.Range(min, max);
        lastNumber = rand;
        return rand;
    }

    private void AddMinMaxMap()
    {
        mapPoolCount++;
        //If player collides with more than 6 signs
        if (mapPoolCount >= 6)
        {
            mapPoolCount = 0;

            //Add max map index
            if (minMaxMap.y < mapsList.Count && minMaxMap.y < maxMap)
                minMaxMap.y++;

            //Add min map index
            if (minMaxMap.x < minMaxMap.y - 5)
                minMaxMap.x++;
        }
    }

    public void AddSaveMaxMap(int addValue)
    {
        if (maxMap < mapsList.Count )
            maxMap += addValue;
        PlayerPrefs.SetInt("maxmap", maxMap);
    }

    public void LoadMaxMap()
    {
        if (PlayerPrefs.HasKey("maxmap"))
            maxMap = PlayerPrefs.GetInt("maxmap");
        else
            maxMap = (int)minMaxMap.y;
    }

    //Set prop type by map index
    private void SetMapsPropsTypeByIndex()
    {
        int m_count = 0;
        int coinMaxType = 6;
        for (int i = mapsList.Count - 1; i >= 1; i--)
        {
            m_count++;
            mapsList[i].GetComponent<GroundController>().maxCoinType = coinMaxType;
            if (m_count >= mapsList.Count/6)
            {
                if (coinMaxType >= 1)
                    coinMaxType--;
                m_count = 0;
            }

        }
    }
}
