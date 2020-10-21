using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameDetails : MonoBehaviour
{
    //config params
    [SerializeField] int pointPerKill = 100;

    //static fields
    int currentScore = 0;

    private void Awake()
    {
        if(FindObjectsOfType<GameDetails>().Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
            Debug.Log("destroyed thinmsd");
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AddToScore()
    {
        currentScore += pointPerKill;
    }

    //resets score when restarting game
    public void ResetGame()
    {
        Destroy(gameObject);
    }

    public int GetScore()
    {
        return currentScore;
    }
}
