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
        // ���콺�� ��/��� �����̴� mouseX�� Y�࿡ �����ϴ� ������
        // ���츦 ��/��� ������ �� ī�޶� ��/�츦 ������ ī�޶� ������Ʈ��
        // Y���� ȸ���ؾ� �ϱ� ����
        eulerAngleY += mouseX * rotateSpeedX;
        // ���� �������� ��/�Ʒ��� ������ ī�޶��� x���� ȸ���ؾ� ��
        eulerAngleX -= mouseY * rotateSpeedY;

        // x�� ȸ������ ��� �Ʒ�, ���� ���� ���� ���� ����
        eulerAngleX = ClampAngle(eulerAngleX, limitMinX, limitMaxX);

        //���� ������Ʈ�� ���ʹϿ� ȸ���� ����
        transform.rotation = Quaternion.Euler(eulerAngleX, eulerAngleY, 0);
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) angle += 360;
        if (angle > 360) angle -= 360;

        // Mathf.Clamp �Լ��� ����� angle�� min <= angle <= max�� �����ϵ��� �Ѵ�
        return Mathf.Clamp(angle, min, max);
    }
}
