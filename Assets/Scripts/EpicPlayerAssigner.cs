using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EpicPlayerAssigner : MonoBehaviour
{
    public int megaEpicNr;

    public GameObject uIElements;

    public Image musketeerHealthBar;
    public Image musketeerRosemaryBar;

    public Image thunderbirdHealthBar;
    public Image thunderbirdRosemaryBar;

    public Image cavemanHealthBar;
    public Image cavemanRosemaryBar;

    public Image thunderbirdCooldown1;
    public Image thunderbirdCooldown2;
    
    public Image musketeerCooldown;

    public Image cavemanCooldown;

    public void AssignNumberToEpic(int epicNr)
    {
        megaEpicNr = epicNr;
    }
}
