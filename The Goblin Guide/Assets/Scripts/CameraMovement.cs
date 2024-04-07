using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CameraMovement : MonoBehaviour
{
    Transform player;
    [SerializeField] Vector3 CameraOffset;
    [SerializeField] float lookHeight;
    float currentX, currentY;
    Vector3 lookAtPos;

    [SerializeField] float y_max_value = 0.0f;
    [SerializeField] float y_min_value = 0.0f;

    [SerializeField] float distance = -5;
    void Start()
    {
        player = FindFirstObjectByType<PlayerMovement>().transform;
    }

    void Update()
    {
        currentY += Input.GetAxis("Mouse X");
        currentX += Input.GetAxis("Mouse Y");
        currentX = Mathf.Clamp(currentX, y_min_value, y_max_value);

        Quaternion rotation = Quaternion.Euler(currentX, currentY, 0.0f);
        Vector3 dir = new Vector3(0.0f, 0.0f, distance) + CameraOffset;
        transform.position = player.position + rotation * dir;
        lookAtPos = player.position;
        lookAtPos.y += lookHeight;
        transform.LookAt(lookAtPos);
    }
}