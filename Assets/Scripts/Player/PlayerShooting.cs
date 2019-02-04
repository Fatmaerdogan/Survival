using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;//her merminin zarar sayısı
    public float timeBetweenBullets = 0.15f;//ateş süresinin arası
    public float range = 100f;//ateş edebileceği max uzaklık


    float timer;//ne zaman ateşleneceğini belirleyen zamanlayıcı
    Ray shootRay = new Ray();//gönderilen ışın
    RaycastHit shootHit;//gönderilen ışının neye çarptığı
    int shootableMask;
    ParticleSystem gunParticles;//ışının ps referans
    LineRenderer gunLine;//referansı
    AudioSource gunAudio;//ışının sesi
    Light gunLight;//gönderilen ışının referansı
    float effectsDisplayTime = 0.2f;//etki göstereceği oran (timebetweenbullet)
    public GameObject gun;
    public GameObject GunStart;

    void Awake ()
    {
        shootableMask = LayerMask.GetMask ("Shootable");
        gunParticles = this.transform.GetChild(1).GetComponent<ParticleSystem> ();
        gunLine = this.transform.GetChild(1).GetComponent <LineRenderer> ();
        gunAudio = this.transform.GetChild(1).GetComponent<AudioSource> ();
        gunLight = this.transform.GetChild(1).GetComponent<Light> ();
    }


    void Update ()
    {
        timer += Time.deltaTime;//en son gönderilen ışını ekleyin

		if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)//fire1 butonuna basılıyor ve time aralığı aşılmadıysa ateş et
        {
            Shoot ();
        }

        if(timer >= timeBetweenBullets * effectsDisplayTime)//ışın gönderiliyorsa efectleri çalıştırma
        {
            DisableEffects ();
        }
    }


    public void DisableEffects ()//ışık ve düz çizgi durdurulsun
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }


    void Shoot ()
    {
        timer = 0f;//zaman sıfırlanır

        gunAudio.Play ();//silahın sesi

        gunLight.enabled = true;//ışık etkinleştildi

        gunParticles.Stop ();//ps durduruldu başlatıldı
        gunParticles.Play ();

        gunLine.enabled = true;//düz çizgi etkinleştirildi
        gunLine.SetPosition (0, GunStart.transform.position);//ilk konum ayarlanır =tabancanın sonu

        shootRay.origin = GunStart.transform.position;//ışın tabancanın ucunden oluşturularak ileri doğru 
        shootRay.direction = gun.transform.position - GunStart.transform.position;

        if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))//ışının çarptığı nokta var mı
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();//ışığın çarptığı nokta da bir düşman var mı
            if(enemyHealth != null)
            {
                enemyHealth.TakeDamage (damagePerShot, shootHit.point);//düşman hasar alır
            }
            gunLine.SetPosition (1, shootHit.point);//ışının gidebildiği yer ayarlanır
        }
        else//ışının sonu olarak gidebileceği son nokta ayarlanır
        {
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        }
    }
}
