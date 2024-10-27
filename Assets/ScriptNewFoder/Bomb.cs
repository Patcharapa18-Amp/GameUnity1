using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public int scorePenalty = 1; // ค่าคะแนนที่จะลดเมื่อชนกับระเบิด

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // เรียก AddScore ที่ตัวละคร แต่ทำให้ลดคะแนน
            other.GetComponent<CharacterMovement>().ReduceScore(scorePenalty);

            // ทำลายระเบิดทันทีเมื่อชนกับผู้เล่น
            Destroy(gameObject);
        }
    }
}