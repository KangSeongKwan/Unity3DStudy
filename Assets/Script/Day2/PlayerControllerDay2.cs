using UnityEngine;

public class PlayerControllerDay2 : MonoBehaviour
{
    private Movement3DDay2 movement3DDay2;
    // Start is called before the first frame update
    void Awake()
    {
        movement3DDay2 = GetComponent<Movement3DDay2>();
    }

    // Update is called once per frame
    void Update()
    {
        // ���콺 ���ʹ�ư Ŭ�� ��
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            // Camera.main : �±װ� "Camera"�� ������Ʈ = Main Camera
            // ī�޶�κ��� ���콺 ��ǥ(Input.mousePosition) ��ġ�� �����ϴ� ���� ����
            // ray.origin : ���� ���� ��ġ
            // ray.direction : ������ ���� ����
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Physics.Raycast() : ������ �߻��ؼ� �ε��� ������Ʈ ����
            // ������ true ��ȯ
            // ray.origin ��ġ�κ��� ray.direction �������� ������ ����(Mathf.Infinity)�� ���� �߻�
            // ������ �ε����� ������Ʈ�� ���θ� hit�� ����
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                // hit.transform.position : �ε��� ������Ʈ ��ġ
                // hit.point : ������ ������Ʈ�� �ε��� ���� ��ġ

                // hit.point�� ��ǥ ��ġ�� �̵�
                movement3DDay2.MoveTO(hit.point);
            }
        }
    }
}
