using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;//başlangıc sağlık miktarı
    public int currentHealth;//oyuncunun sağlık miktarı
    public Slider healthSlider;//kullanılan slider
    public Image damageImage;//ekrandaki image
    public AudioClip deathClip;//ölünce oynatılacak ses
    public float flashSpeed = 5f;//
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);


    Animator anim;//animasyon
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    bool isDead;
    bool damaged;


    void Awake ()
    {
        anim = GetComponent <Animator> ();//ölüm animasyonunu atıcagımız değişken
        playerAudio = GetComponent <AudioSource> ();//ölüm anında çıkacak ses
        playerMovement = GetComponent <PlayerMovement> ();//oyuncunun son hali
        playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth;//oyuncunun acına ilk başta başlangıç canını atarız
    }


    void Update ()
    {
        //bu fonksiyonun asıl amacı hasar gördüysek ekrandaki deleteImage kırmızıya döner tekrar seffaflaşır görmediysek sorun yok
        if(damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
            //lerp burda renkte değişim gerçekleştirdik
        }
        damaged = false;
    }


    public void TakeDamage (int amount)
    {
        damaged = true;

        currentHealth -= amount;
        //oyunucunun durumunu görteriyor
        healthSlider.value = currentHealth;
        //kalan canı slidere atarız ve slider kayar
        playerAudio.Play ();
        //burada oyuncu acı sesi çıkarır
        if(currentHealth <= 0 && !isDead)//slider 0a eşit ve isdead yanlış iseöo zaman oyunucunun canı bitmiş ve ölmüştür
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;
        //isdead tamamen öldü
        playerShooting.DisableEffects ();

        anim.SetTrigger ("Die");
        //Die animasyonu çalıştırılır
        playerAudio.clip = deathClip;//incinen ses duracak ölüm sesi devreye girer
        playerAudio.Play ();//ses oynatılmaya başlanır

        playerMovement.enabled = false;//hareketleri durdurur
        playerShooting.enabled = false;
    }

    /*
    public void RestartLevel ()
    {
        SceneManager.LoadScene (0);
    }*/
}
