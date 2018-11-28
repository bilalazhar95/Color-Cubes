using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikerPool : MonoBehaviour
{
    [SerializeField] GameObject StrikerPrefab = null;
    [SerializeField] int initialPoolCount = 50;
    public static StrikerPool Instance = null;

   


    List<GameObject> pool = new List<GameObject>();


    // Use this for initialization
    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);

        }

        DontDestroyOnLoad(gameObject);
        InitializePool();
    }

    private void InitializePool()
    {
        for (int i =0; i< initialPoolCount;i++)
        {
            GameObject striker = Instantiate(StrikerPrefab);
            striker.SetActive(false);
            pool.Add(striker);
        }
    }

    public GameObject GetStrikerFromPool ()
    {
        foreach (GameObject striker in pool)
        {
            if (!striker.activeSelf)
            {
                return striker;
            }
        }
        GameObject newStriker = Instantiate(StrikerPrefab);
        newStriker.SetActive(false);
        pool.Add(newStriker);
        return newStriker;
    }
	

}
