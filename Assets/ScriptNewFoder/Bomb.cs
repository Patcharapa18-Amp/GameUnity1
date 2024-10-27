using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public int scorePenalty = 1; // ��Ҥ�ṹ����Ŵ����ͪ��Ѻ���Դ

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // ���¡ AddScore ������Ф� ������Ŵ��ṹ
            other.GetComponent<CharacterMovement>().ReduceScore(scorePenalty);

            // ��������Դ�ѹ������ͪ��Ѻ������
            Destroy(gameObject);
        }
    }
}