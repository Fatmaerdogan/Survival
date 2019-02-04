using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject button;
    public GameObject OAbutton;
    Animator anim;
    public static bool isPaused;

    void Awake()
    {
        anim = GetComponent<Animator>();
        button.SetActive (false);
        OAbutton.SetActive(false);
    }


    void Update()
    {
        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("GameOver");
            button.SetActive(true);
            OAbutton.SetActive(true);
            Time.timeScale = 1.2f;
        }
       
    }
   
}
