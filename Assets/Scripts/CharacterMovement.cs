using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{
    Animator animator;
    CharacterController characterController;

    public float speed = 6.0f;
    public float rotationSpeed = 8.0f;
    public float jumpSpeed = 7.5f;
    public float gravity = 20.0f;
    public float raycastDistance = 0.5f;
    public int score;
    public UI ui;
    private bool canMove = true; // ��������ͤǺ����������͹���

    private Vector3 moveDirection = Vector3.zero;
    private Vector3 targetDirection;

    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    public void StopMovement()
    {
        canMove = false; // ��ش�������͹���
    }

    public void AllowMovement()
    {
        canMove = true; // ͹حҵ�������͹���
    }

    public void DisableMovement()
    {
        canMove = false; // ��ش�������͹���
        StartCoroutine(EnableMovementCoroutine()); // ���¡ Coroutine �������¹����
    }

    private IEnumerator EnableMovementCoroutine() // ����¹���Ϳѧ��ѹ�����
    {
        yield return new WaitForSeconds(0.5f); // �� 0.5 �Թҷ�
        canMove = true; // ͹حҵ������Ф�����͹����ա����
    }

    void Update()
    {
        if (canMove) // ��Ǩ�ͺ�������͹���
        {
            HandleInput();      // ��ҹ����Թ�ص�ҡ�����
            HandleMovement();   // ����͹������Ф�
            HandleRotation();   // ��ع��ǵ����ȷҧ�������͹���
        }
    }

    void HandleInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // �ӹǳ��ȷҧ�ҡ�Թ�ص
        targetDirection = new Vector3(horizontal, 0, vertical).normalized;
    }

    void HandleMovement()
    {
        if (characterController.isGrounded)
        {
            Vector3 targetVelocity = targetDirection * speed;
            moveDirection = targetVelocity;

            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }
        else
        {
            moveDirection.y -= gravity * Time.deltaTime; // �����ç�����ǧ�����������躹���
        }

        // ����͹������ CharacterController
        characterController.Move(moveDirection * Time.deltaTime);

        // �Ѿവ�͹����ѹ
        animator.SetFloat("Input X", targetDirection.x);
        animator.SetFloat("Input Z", targetDirection.z);
        animator.SetBool("Moving", targetDirection.magnitude > 0);
    }

    void HandleRotation()
    {
        if (targetDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    bool IsObstacleInFront()
    {
        Ray ray = new Ray(transform.position + Vector3.up * 0.5f, targetDirection);
        return Physics.Raycast(ray, raycastDistance);
    }

    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("Score: " + score);
        ui.SetScoreText(score);

        // ��ش�������͹���
        canMove = false;

        // ����� coroutine ����͹حҵ�������͹����ա����
        StartCoroutine(EnableMovementCoroutine()); // ���¡�� Coroutine ����ժ�������
    }
    public void ReduceScore(int amount)
    {
        score -= amount;
        // ��Ǩ�ͺ��Ҥ�ṹ����ӡ��� 0
        if (score < 0)
        {
            score = 0;
        }
        Debug.Log("Score: " + score);
        ui.SetScoreText(score);
    }
}