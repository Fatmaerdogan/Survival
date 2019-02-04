using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;//oyuncunun canının referansı
    public GameObject enemy;//duşman
    public float spawnTime = 3f;//her çoğalma öncesi geçicek süre
    public Transform[] spawnPoints;//düşmanların ortaya çıkabileceği noktaların dizisi


    void Start ()
    {
        InvokeRepeating ("Spawn", spawnTime, spawnTime);
    }


    void Spawn ()
    {
        if(playerHealth.currentHealth <= 0f)
        {//playerin canı bittiyse üretmeye gerek yok
            return;
        }

        int spawnPointIndex = Random.Range (0, spawnPoints.Length);
        //düşmanın yaratılacağı rast gele bir nokta
        Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}