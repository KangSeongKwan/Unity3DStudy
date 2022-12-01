
using UnityEngine;

public class SimplePatrol : MonoBehaviour
{
    [SerializeField]
    private Transform[] paths;
    private int currentPath = 0;
    private float moveSpeed = 3.0f;

    // Update is called once per frame
    void Update()
    {
        // �̵����� ���� : (��ǥ ��ġ - �� ��ġ).����ȭ
        Vector3 direction = (paths[currentPath].position - transform.position).normalized;
        // �̵�
        transform.position += direction * moveSpeed * Time.deltaTime;

        // ��ǥ ��ġ�� ���� ���� ���� ��
        if ((paths[currentPath].position - transform.position).sqrMagnitude < 0.1f )
        {
            // ��ǥ ��ġ ����(���� ��� ��ȯ)
            if (currentPath < paths.Length - 1) currentPath++;
            else currentPath = 0;
        }
    }
}
