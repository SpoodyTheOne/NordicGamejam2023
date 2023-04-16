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
    public PlayerInput playerInput;

    public int characterClass; // 1 = musket, 2 = thunder, 3 = cave

    public GameObject musketeer;
    public GameObject thunderBird;
    public GameObject caveMan;
    public bool ready;
    public Transform virtualCursor;

    private ThunderBirdPlayer tbp = null;
    private MusketeerPlayer mp = null;
    private CavemanPlayer cp = null;

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
        playerInput.actions.FindActionMap("Player").Enable();
    }

    public void InstantiatePlayerCharacter(GameObject playerCharacter)
    {
        Debug.Log("Pog");
        GameObject instant = null;

        instant = Instantiate(playerCharacter, this.transform);

        player = instant.GetComponent<Player>();
        player.playerInput = playerInput;
        epa.uIElements.SetActive(true);
        virtualCursor = gc.cursorTransform;

        if (characterClass == 2)
        {
            weapon = player.GetComponentInChildren<WeaponScript>().gameObject;
            tbp = player.gameObject.GetComponent<ThunderBirdPlayer>();
            player.rosemary = epa.thunderbirdRosemaryBar;
            player.healthBar = epa.thunderbirdHealthBar;
            player.virtualCursor = gc.cursorTransform;
            tbp.img = epa.thunderbirdCooldown1;
            tbp.img2 = epa.thunderbirdCooldown2;
        }
        if (characterClass == 1)
        {
            weapon = player.GetComponentInChildren<MusketWeapon>().gameObject;
            mp = player.gameObject.GetComponent<MusketeerPlayer>();
            player.rosemary = epa.musketeerRosemaryBar;
            player.healthBar = epa.musketeerHealthBar;
            player.virtualCursor = gc.cursorTransform;
            mp.img = epa.musketeerCooldown;
        }
        if (characterClass == 3)
        {
            weapon = player.GetComponentInChildren<CavemanWeapon>().gameObject;
            cp = player.gameObject.GetComponent<CavemanPlayer>();
            player.rosemary = epa.cavemanRosemaryBar;
            player.healthBar = epa.cavemanHealthBar;
            player.virtualCursor = gc.cursorTransform;
            cp.img = epa.cavemanCooldown;
        }

        gc.menuCursor = false;

        ready = true;

        clicking = false;
    }

    public void Clickety(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            clicking = true;
        }
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        player.Move(ctx);
        Debug.Log("1");
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
        if (characterClass == 3)
            weapon.GetComponent<CavemanWeapon>().Attack(ctx);
    }

    public void RightClickAbility(InputAction.CallbackContext ctx)
    {
        if (characterClass == 1) //musket
        {
            weapon.GetComponent<MusketWeapon>().RightClickAbility(ctx);
        } else if (characterClass == 2) //thunder
        {
            tbp.RightClickAbility(ctx);
        } else if (characterClass == 3) //cave
        {
            cp.RightClickAbility(ctx);
        }
    }

    public void EAbility(InputAction.CallbackContext ctx)
    {
        if (characterClass == 1) //musket
        {
            mp.EAbility(ctx);
        }
        else if (characterClass == 2) //thunder
        {
            tbp.EAbility(ctx);
        }
        else if (characterClass == 3) //cave
        {
            cp.EAbility(ctx);
        }
    }
}
