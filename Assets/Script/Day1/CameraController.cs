using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float rotateSpeedX = 3;
    private float rotateSpeedY = 3;
    // private float rotateSpeedZ = 3;
    private float limitMinX = -80;
    private float limitMaxX = 50;
    private float eulerAngleX;
    private float eulerAngleY;

    public void RotateTo(float mouseX, float mouseY)
    {
        // 마우스를 좌/우로 움직이는 mouseX를 Y축에 대입하는 이유는
        // 마우를 좌/우로 움직일 때 카메라도 좌/우를 보려면 카메라 오브젝트의
        // Y축이 회전해야 하기 때문
        eulerAngleY += mouseX * rotateSpeedX;
        // 같은 개념으로 위/아래를 보려면 카메라의 x축이 회전해야 함
        eulerAngleX -= mouseY * rotateSpeedY;

        // x축 회전값의 경우 아래, 위를 보는 제한 각도 설정
        eulerAngleX = ClampAngle(eulerAngleX, limitMinX, limitMaxX);

        //실제 오브젝트의 쿼터니온 회전에 적용
        transform.rotation = Quaternion.Euler(eulerAngleX, eulerAngleY, 0);
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) angle += 360;
        if (angle > 360) angle -= 360;

        // Mathf.Clamp 함수를 사용해 angle이 min <= angle <= max를 유지하도록 한다
        return Mathf.Clamp(angle, min, max);
    }
}
