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

    // �� LateUpdate �������������͹��Ǣͧ���ͧ�����ѧ����Ф�
    void LateUpdate()
    {
        Vector3 targetPos = new Vector3(
            LockX ? thisTransform.position.x : target.position.x,
            LockY ? thisTransform.position.y : target.position.y,
            LockZ ? thisTransform.position.z : target.position.z + offSetZ
        );

        if (useSmoothing)
        {
            // �� SmoothDamp ���������˹�����͹������ҧ�Һ���
            thisTransform.position = new Vector3(
                Mathf.SmoothDamp(thisTransform.position.x, targetPos.x, ref velocity.x, SMOOTH_TIME),
                Mathf.SmoothDamp(thisTransform.position.y, targetPos.y, ref velocity.y, SMOOTH_TIME),
                Mathf.SmoothDamp(thisTransform.position.z, targetPos.z, ref velocity.z, SMOOTH_TIME)
            );
        }
        else
        {
            // �ҡ����� smoothing ������͹���˹��µç
            thisTransform.position = targetPos;
        }
    }
}