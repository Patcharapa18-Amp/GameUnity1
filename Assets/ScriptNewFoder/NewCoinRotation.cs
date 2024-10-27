using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCoinRotation : MonoBehaviour
{
    public float rotateSpeed = 100f;
    public float disappearSpeed = 0.5f; // ����㹡������

    void Update()
    {
        // ��ع����­�ͺ᡹ Y
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // ���¡ AddScore ������Ф�
            other.GetComponent<CharacterMovement>().AddScore(1);

            // ���������­�ѹ������ͪ��Ѻ������
            Destroy(gameObject);

            // ��ش�������͹���ͧ����Фê��Ǥ���
            other.GetComponent<CharacterMovement>().DisableMovement();
        }
    }

    private IEnumerator Disappear()
    {
        // ���������­�դ��������
        for (float t = 0; t < disappearSpeed; t += Time.deltaTime)
        {
            Color color = GetComponent<Renderer>().material.color;
            color.a = 1 - t / disappearSpeed; // Ŵ��������鹢ͧ��
            GetComponent<Renderer>().material.color = color;
            yield return null;
        }

        // ���������­��ѧ�ҡ����ѹ�����
        Destroy(gameObject);
    }
}