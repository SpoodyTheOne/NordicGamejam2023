using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterSelector : MonoBehaviour
{
    public PlayerInputManager pim;

    public void ChooseCharacter()
    {
        pim.JoinPlayer();
    }
}
