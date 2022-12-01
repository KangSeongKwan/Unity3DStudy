using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Movement3DDay2 : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5.0f;
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void MoveTO(Vector3 goalPosition)
    {
        //������ �̵��ൿ�� �ϰ��־����� �ڷ�ƾ ����
        StopCoroutine("OnMove");
        // �̵� �ӵ� ����
        navMeshAgent.speed = moveSpeed;
        // ��ǥ���� ����(��� ��� �� �˾Ƽ� ������)
        navMeshAgent.SetDestination(goalPosition);
        StartCoroutine("OnMove");
    }

    IEnumerator OnMove()
    {
        while(true)
        {
            //��ǥ ��ġ(navMeshAgent.destination)�� �� ��ġ(transform.position) �� �Ÿ��� 0.1 �̸��� ��
            // ��ǥ ��ġ�� ���� �������� ��
            if (Vector3.Distance(navMeshAgent.destination, transform.position) < 0.1f)
            {
                transform.position = navMeshAgent.destination;
                // SetDestination() �� ���� ������ ��θ� �ʱ�ȭ, �̵��� �����.
                navMeshAgent.ResetPath();

                break;
            }

            yield return null;
        }
    }
}
