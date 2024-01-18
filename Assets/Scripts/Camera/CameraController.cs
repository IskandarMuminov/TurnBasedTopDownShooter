using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float cameraMoveSpeed = 10f;
    [SerializeField] private float cameraRotationSpeed = 10f;
    [SerializeField] private float zoomSpeed = 10f;

    [SerializeField] private float followOffsetMax = 50f;
    [SerializeField] private float followOffsetMin = 10f;
    private Vector3 followOffset;

    private Vector3 inputRotation;
    [SerializeField] private CinemachineVirtualCamera cinemachineCamera;
    private CinemachineTransposer cinemachineTransposer;

    private void Awake()
    {
        cinemachineTransposer = cinemachineCamera.GetCinemachineComponent<CinemachineTransposer>();
        followOffset = cinemachineTransposer.m_FollowOffset;
    }
    void Update()
    {
        
        HandleCameraMovement();
        HandleCamerRotation();
        HandleCameraZoom();
    }
    private void HandleCameraMovement() 
    {
        Vector3 inputMoveDir = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            inputMoveDir.z = +1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputMoveDir.z = -1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputMoveDir.x = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputMoveDir.x = +1f;
        }
        Vector3 moveVector = transform.forward * inputMoveDir.z + transform.right * inputMoveDir.x;
        transform.position += moveVector * cameraMoveSpeed * Time.deltaTime;
    }

    private void HandleCamerRotation() {

        if (Input.GetKey(KeyCode.Mouse2))
        {
            inputRotation.x += Input.GetAxis("Mouse X") ;
        }
        transform.localRotation = Quaternion.Euler(0, inputRotation.x * cameraRotationSpeed, 0);
    }
    private void HandleCameraZoom()
    {
        
        if (Input.mouseScrollDelta.y > 0) {
            followOffset.y -= zoomSpeed;
        }

        if (Input.mouseScrollDelta.y < 0) {
            followOffset.y += zoomSpeed;
        }
        followOffset.y = Mathf.Clamp(followOffset.y, followOffsetMin, followOffsetMax);

        cinemachineTransposer.m_FollowOffset =
        Vector3.Lerp(cinemachineCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset,
            followOffset, zoomSpeed * Time.deltaTime);
    }
}
