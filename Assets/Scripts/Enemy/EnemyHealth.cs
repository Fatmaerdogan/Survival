using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;//başlangıc canı
    public int currentHealth;//kalan canı
    public float sinkSpeed = 2.5f;//ölen düşmanın zemine batma hızı
    public int scoreValue = 10;//oyuncun puanına eklenen miktar
    public AudioClip deathClip;//öldüğünde çıkacak ses


    Animator anim;//ölüm animasyonu
    AudioSource enemyAudio;//öldüğünde çıkan ses
    ParticleSystem hitParticles;//düşman hasar gördüğünde ouşan particle system
    CapsuleCollider capsuleCollider;//kurşunla çarpışan kapsül
    bool isDead;//ölüp ölmediğine 
    bool isSinking;//zeminde batmaya başlayıp başlamadığı


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        hitParticles = GetComponentInChildren <ParticleSystem> ();//burdaki atamanın getcomponent In Children olmasının sebebi systemin objenin altında yer alması
        capsuleCollider = GetComponent <CapsuleCollider> ();

        currentHealth = startingHealth;//canı eşitliyor
    }


    void Update ()
    {
        if(isSinking)
        {
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
            //eğer düşman düşüyorsa belirli bir zaman diliminde konumu azalarak batar
        }
    }


    public void TakeDamage (int amount, Vector3 hitPoint)
    {
        if(isDead)// öldüyse bu işlemlere hiç girmesin
            return;
        //ölmediyse 
        enemyAudio.Play ();//yaralandığı ses çıkar

        currentHealth -= amount;//can sayısı istenildiği kadar azalir
            
        hitParticles.transform.position = hitPoint;//karakterin pozisyonu eşitlensin particle systeme
        hitParticles.Play();//systemi çalıştır

        if(currentHealth <= 0)//can sayısı az ise ölsün
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;//true yapıyoruz çünkü fizik işlemlerine tabi kalmasın diye

        anim.SetTrigger ("dead");//dead animasyonu çalışssın

        enemyAudio.clip = deathClip;//çıkan sese ölüm sesi atanır ve çalıştırılır
        enemyAudio.Play ();
    }


    public void StartSinking ()
    {
        GetComponent <UnityEngine.AI.NavMeshAgent> ().enabled = false;//navMeshAgent bul ve kapat
        GetComponent <Rigidbody> ().isKinematic = true;//true  yap ve düşmanın batabilmesi için
        isSinking = true;
        ScoreManager.score += scoreValue;
        Destroy (gameObject, 2f);//nesne 2 saniye görünüp yom olacak
    }
}
