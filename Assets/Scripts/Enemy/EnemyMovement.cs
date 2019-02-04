using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{//tavşanların playeri takibini sağlayan kod
    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;//navMeshAgent takibi sağladığı için bu tipde bir nesne oluşturulmalı


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player").transform;//takip edilecek nesnenin pozisyonu
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();//takip edicek karakter
    }


    void Update ()
    {
        if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            nav.SetDestination (player.position);//takibi sağlayan kod
        }
        else
        {
            nav.enabled = false;
        }
    }
}
