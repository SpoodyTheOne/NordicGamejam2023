using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThePlayerManager : MonoBehaviour
{
    public Player player = null;
    public GameObject weapon;
    public EpicPlayerAssigner epa;
    private bool clicking;
    public GamepadCursor gc;

    public int characterClass; // 1 = musket, 2 = thunder, 3 = cave

    public GameObject musketeer;
    public GameObject thunderBird;
    public GameObject caveMan;
    public bool ready;

    public void Start()
    {
        epa = GameObject.Find("EpicPlayerAssigner").GetComponent<EpicPlayerAssigner>();
    }

    public void Update()
    {
        if (player == null)
        {
            if (clicking)
            {
                characterClass = epa.megaEpicNr;
                if (characterClass == 1)
                    InstantiatePlayerCharacter(musketeer);
                if(characterClass == 2)
                    InstantiatePlayerCharacter(thunderBird);
                if (characterClass == 3)
                    InstantiatePlayerCharacter(caveMan);
            }
        }
    }

    public void ReadyToGoOn(InputAction.CallbackContext ctx)
    {
        GameObject.Find("CharacterChoicePanel").SetActive(false);
    }

    public void InstantiatePlayerCharacter(GameObject playerCharacter)
    {
        GameObject instant = null;

        instant = Instantiate(playerCharacter, this.transform);

        player = instant.GetComponent<Player>();

        if (characterClass == 2)
            weapon = player.GetComponentInChildren<WeaponScript>().gameObject;
        if (characterClass == 1)
            weapon = player.GetComponentInChildren<MusketWeapon>().gameObject;

        gc.menuCursor = false;

        ready = true;

        clicking = false;
    }

    public void Clickety(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Debug.Log("HEJKSHGIJSE");
            clicking = true;
        }
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        player.Move(ctx);
    }
    
    public void AssignPlayerCharacter(int characterNr)
    {
        characterClass = characterNr;
    }

    public void Attack(InputAction.CallbackContext ctx)
    {
        if (characterClass == 2)
            weapon.GetComponent<WeaponScript>().Attack(ctx);
        if (characterClass == 1)
            weapon.GetComponent<MusketWeapon>().Attack(ctx);
    }

    public void RightClickAbility(InputAction.CallbackContext ctx)
    {
        if (characterClass == 1) //musket
        {

        } else if (characterClass == 2) //thunder
        {
            player.gameObject.GetComponent<ThunderBirdPlayer>().RightClickAbility(ctx);
        } else if (characterClass == 3) //cave
        {

        }
        //player.RightClickAbility(ctx);
    }

    public void EAbility(InputAction.CallbackContext ctx)
    {
        if (characterClass == 1) //musket
        {

        }
        else if (characterClass == 2) //thunder
        {
            player.gameObject.GetComponent<ThunderBirdPlayer>().EAbility(ctx);
        }
        else if (characterClass == 3) //cave
        {

        }
        //player.EAbility(ctx);
    }
}
