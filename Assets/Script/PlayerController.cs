using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private KeyCode jumpKeyCode = KeyCode.Space;
    [SerializeField]
    private CameraController cameraController;
    private Movement3D movement3D;

    private void Awake()
    {
        movement3D = GetComponent<Movement3D>();
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal"); //방향키 좌/우
        float z = Input.GetAxisRaw("Vertical"); //방향키 위/아래

        movement3D.MoveTo(new Vector3(x, 0, z));

        // 점프키 눌러 y축 방향으로 뛰어오르기
        if(Input.GetKeyDown(jumpKeyCode))
        {
            movement3D.JumpTo();
        }

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        cameraController.RotateTo(mouseX, mouseY);
    }
}
