using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectorScript : MonoBehaviour
{
    public int idChosen;
    private Animator anim;

    public Player[] player;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void NextScene(int id)
    {
        idChosen = id;

        Invoke("LoadNextScene", 1.2f);
        anim.SetTrigger("BlackScreen");
    }
    private void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        anim.SetTrigger("endBlackScreen");

        Invoke("CheckCharacter", .01f);
    }
    private void CheckCharacter()
    {
        Player[] player = FindObjectsOfType<Player>();

        for (int i = 0; i < player.Length; i++)
        {
            player[i].gameObject.SetActive(false);

            if (player[i].id == idChosen)
                player[i].gameObject.SetActive(true);
        }
    }
}
