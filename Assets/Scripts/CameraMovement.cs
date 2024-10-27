using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private const float SMOOTH_TIME = 0.3f;
    public bool LockX, LockY, LockZ;
    public float offSetZ = -3f;
    public bool useSmoothing = true;
    public Transform target;

    private Transform thisTransform;
    private Vector3 velocity = Vector3.zero;

    void Awake()
    {
        thisTransform = transform;
    }

    // ใช้ LateUpdate เพื่อให้การเคลื่อนไหวของกล้องตามหลังตัวละคร
    void LateUpdate()
    {
        Vector3 targetPos = new Vector3(
            LockX ? thisTransform.position.x : target.position.x,
            LockY ? thisTransform.position.y : target.position.y,
            LockZ ? thisTransform.position.z : target.position.z + offSetZ
        );

        if (useSmoothing)
        {
            // ใช้ SmoothDamp เพื่อให้ตำแหน่งเคลื่อนที่อย่างราบรื่น
            thisTransform.position = new Vector3(
                Mathf.SmoothDamp(thisTransform.position.x, targetPos.x, ref velocity.x, SMOOTH_TIME),
                Mathf.SmoothDamp(thisTransform.position.y, targetPos.y, ref velocity.y, SMOOTH_TIME),
                Mathf.SmoothDamp(thisTransform.position.z, targetPos.z, ref velocity.z, SMOOTH_TIME)
            );
        }
        else
        {
            // หากไม่ใช้ smoothing จะเคลื่อนตำแหน่งโดยตรง
            thisTransform.position = targetPos;
        }
    }
}