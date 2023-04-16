using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject[] players;
    public TMP_Text[] texts;

    public void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    public void Update()
    {
        //for (int i = 0; i < players.Length; i++)
        //{
        //    texts[i].text = "HP = " + players[i].GetComponent<Player>().Health;
        //}
    }
}
