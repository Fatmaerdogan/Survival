using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour {
    public Transform target;   //kameranın takip edeceği nesne
    public float smoothing = 5f;//takip etme hızı
    Vector3 offset; //baslangıc pozisyonu
    private void Start()
    {
        offset= transform.position - target.position;//başlangıc pozisyonu hesaplanır
    }
    private void FixedUpdate()
    {
        //cameraya göre hedef pozisyon oluşturulur
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}
