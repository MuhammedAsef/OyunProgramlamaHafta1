using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 10f;  // Topun hareket hızı
    public float jumpForce = 7f; // Zıplama kuvveti
    private Rigidbody rb;
    private bool isGrounded; // Yerde olup olmadığını kontrol etmek için

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Rigidbody bileşeni yoksa hata mesajı ver
        if (rb == null)
        {
            Debug.LogError("HATA: Rigidbody bileşeni bulunamadı! Sphere nesnesine Rigidbody ekleyin.");
        }

        // Topun dönmesini önlemek için X ve Z ekseninde rotasyonu dondur
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        // Klavyeden giriş al
        float moveHorizontal = Input.GetAxis("Horizontal"); // A (-1) / D (1)
        float moveVertical = Input.GetAxis("Vertical"); // W (1) / S (-1)

        // Hareket yönü belirleme
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;

        // Rigidbody'nin velocity'sini direkt olarak değiştirerek daha stabil hareket sağla
        rb.linearVelocity = new Vector3(movement.x * speed, rb.linearVelocity.y, movement.z * speed);
    }

    void Update()
    {
        // Space tuşuna basıldığında ve top yerdeyse zıplat
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Eğer zeminle temas ettiyse isGrounded = true yap
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
