using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jump = 100f;
    public float Speed = 6f; //player hareket edeceği hız
    Vector3 movement;        //oyuncunun hareket yönünü saklamak için vektor
    Animator Anim;          //Animatör bileşeni referansı
    Rigidbody PlayerRigidbody;//oyuncunun ridigboy referans
    int floorMask;            //tabakanın maskelene bilmesi için ışın,sadece zemintabakasındaki objelere atılabilir
    float camRayLength = 100f;//kameranın olay yerine ışın uzunluğu
    void Awake()
    {
        //zemin tabanı için katman mask oluşturduk
        floorMask = LayerMask.GetMask("Floor");
        Anim = GetComponent<Animator>();
        PlayerRigidbody = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        // Debug.Log("ilk h"+h);
        // Debug.Log("ilk v"+v);
       /* if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerRigidbody.AddForce(Vector3.up * jump);
            Debug.Log(jump);
        }*/
        Move(h, v);
        Turning();
        Animating(h, v);
    }
    void Move(float he, float ve)
    {
        movement.Set(he, 0f, ve); //eksen girişine göre hareket vektörünü ayarlar
        Debug.Log("1 movement :" + movement);
        movement = movement.normalized * Speed * Time.deltaTime; //hareket vektörünü normale döndürür ve saniyedeki hızla hareketi sağlar
        PlayerRigidbody.MovePosition(transform.position + movement);

        // movement.y -= jump * Time.deltaTime;// Player mevcut pozisyona hareket etsin
        Debug.Log("2 movement :" + movement);
    }
    void Turning()
    {
        Ray CamRay = Camera.main.ScreenPointToRay(Input.mousePosition); // ekrandaki fare imlecinden kamera yönünde ışın oluşturur
        RaycastHit floorHit; // Ray tarafından neyin çarptığını tutun değişken
        if (Physics.Raycast(CamRay, out floorHit, camRayLength, floorMask))//zemin katına bir isabet ederse
        {
            Vector3 playerToMouse = floorHit.point - transform.position; //mouse un rotasyonunu alır 
            playerToMouse.y = 0f;//yukarı hareket etmemesi için y yi 0 lar
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);// onu yeni rotasyon değişkenıne atar
            PlayerRigidbody.MoveRotation(newRotation);//yeni rotation olayerin rotasyonuna atar
        }
    }
    void Animating(float he, float ve)
    {
        bool walking = he != 0f || ve != 0f;
        Anim.SetBool("IsWalking", walking);//karakterin yürüyüp yürümediğini kontrol eder
    }
    private void OnCollisionStay(Collision collision)
    {
        if((collision.gameObject.tag == "floor")&& Input.GetKeyDown(KeyCode.Space))
        {
            PlayerRigidbody.AddForce(Vector3.up * jump);
            Debug.Log(jump);
        }
    }
}

    