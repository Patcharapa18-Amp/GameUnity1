using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCoinRotation : MonoBehaviour
{
    public float rotateSpeed = 100f;
    public float disappearSpeed = 0.5f; // เวลาในการหายไป

    void Update()
    {
        // หมุนเหรียญรอบแกน Y
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // เรียก AddScore ที่ตัวละคร
            other.GetComponent<CharacterMovement>().AddScore(1);

            // ทำลายเหรียญทันทีเมื่อชนกับผู้เล่น
            Destroy(gameObject);

            // หยุดการเคลื่อนที่ของตัวละครชั่วคราว
            other.GetComponent<CharacterMovement>().DisableMovement();
        }
    }

    private IEnumerator Disappear()
    {
        // ทำให้เหรียญมีความโปร่งใส
        for (float t = 0; t < disappearSpeed; t += Time.deltaTime)
        {
            Color color = GetComponent<Renderer>().material.color;
            color.a = 1 - t / disappearSpeed; // ลดความเข้มข้นของสี
            GetComponent<Renderer>().material.color = color;
            yield return null;
        }

        // ทำลายเหรียญหลังจากที่มันโปร่งใส
        Destroy(gameObject);
    }
}