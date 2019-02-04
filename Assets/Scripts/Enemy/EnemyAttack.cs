using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;//saldırı arasındaki süre
    public int attackDamage = 10;//saldırırnın zarar miktarı


    Animator anim;
    GameObject player;//oyuncu
    PlayerHealth playerHealth;//oyuncunun sağlığı
    EnemyHealth enemyHealth;//düşmanın canı
    bool playerInRange;//oyuncu saldırıya uğradımı
    float timer;


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");//oyuncunun referansı
        playerHealth = player.GetComponent <PlayerHealth> ();//oyuncunun canının referansı
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator> ();//animasyonun referansı
    }


    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject == player)//duşmanın çarptığı şey oyuncumu tamam o zaman saldırabilir
        {
            playerInRange = true;
        }
    }
    void OnTriggerExit (Collider other)
    {
        if(other.gameObject == player)//tetikleyen oyuncumu
        {
            playerInRange = false;
        }
    }


    void Update ()
    {
        timer += Time.deltaTime;

        if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            Attack ();//oyuncu menzilde oyunca düşman saldıracak
        }

        if(playerHealth.currentHealth <= 0)//düşman öldüyse öldü animasyonunu çalıştır
        {
            anim.SetTrigger ("EnemyDead");
        }
    }

    void Attack ()
    {
        timer = 0f;//zaman sıfırlanır

        if(playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage (attackDamage);//oyunucunun sağlığına zarar verir
        }
    }
}
