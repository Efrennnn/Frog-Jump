using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; 
    public float smoothSpeed = 0.125f; 

    public Vector3 offset; 
    
    private void FixedUpdate() 
    {
        // Mengambil posisi kamera saat ini
        Vector3 desiredPosition = target.position + offset;

        // Menghitung posisi kamera yang diinginkan dengan smoothing
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Mengatur posisi kamera sesuai dengan yang diinginkan
        transform.position = new Vector3(smoothedPosition.x, transform.position.y, transform.position.z);
    }
}