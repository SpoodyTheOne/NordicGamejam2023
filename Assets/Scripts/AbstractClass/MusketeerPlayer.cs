using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusketeerPlayer : Player
{

    public void StartPFX()
    {
        pfx.Play();
        pfx.loop = true;
    }
}
