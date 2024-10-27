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
    private bool canMove = true; // ตัวแปรเพื่อควบคุมการเคลื่อนที่

    private Vector3 moveDirection = Vector3.zero;
    private Vector3 targetDirection;

    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    public void StopMovement()
    {
        canMove = false; // หยุดการเคลื่อนที่
    }

    public void AllowMovement()
    {
        canMove = true; // อนุญาตให้เคลื่อนที่
    }

    public void DisableMovement()
    {
        canMove = false; // หยุดการเคลื่อนที่
        StartCoroutine(EnableMovementCoroutine()); // เรียก Coroutine ที่เปลี่ยนชื่อ
    }

    private IEnumerator EnableMovementCoroutine() // เปลี่ยนชื่อฟังก์ชันที่นี่
    {
        yield return new WaitForSeconds(0.5f); // รอ 0.5 วินาที
        canMove = true; // อนุญาตให้ตัวละครเคลื่อนที่อีกครั้ง
    }

    void Update()
    {
        if (canMove) // ตรวจสอบการเคลื่อนที่
        {
            HandleInput();      // อ่านค่าอินพุตจากผู้ใช้
            HandleMovement();   // เคลื่อนที่ตัวละคร
            HandleRotation();   // หมุนตัวตามทิศทางการเคลื่อนที่
        }
    }

    void HandleInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // คำนวณทิศทางจากอินพุต
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
            moveDirection.y -= gravity * Time.deltaTime; // เพิ่มแรงโน้มถ่วงเมื่อไม่อยู่บนพื้น
        }

        // เคลื่อนที่ด้วย CharacterController
        characterController.Move(moveDirection * Time.deltaTime);

        // อัพเดตแอนิเมชัน
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

        // หยุดการเคลื่อนที่
        canMove = false;

        // เริ่ม coroutine เพื่ออนุญาตให้เคลื่อนที่อีกครั้ง
        StartCoroutine(EnableMovementCoroutine()); // เรียกใช้ Coroutine ที่มีชื่อใหม่
    }
    public void ReduceScore(int amount)
    {
        score -= amount;
        // ตรวจสอบว่าคะแนนไม่ต่ำกว่า 0
        if (score < 0)
        {
            score = 0;
        }
        Debug.Log("Score: " + score);
        ui.SetScoreText(score);
    }
}