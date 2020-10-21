using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayHealth : MonoBehaviour
{
    TextMeshProUGUI health;

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("Player") != null)
        {
            health.text = "HP "+FindObjectOfType<Player>().GetHealth().ToString();
        }
        else
        {
            health.text = "HP 0";
        }

    }
}
