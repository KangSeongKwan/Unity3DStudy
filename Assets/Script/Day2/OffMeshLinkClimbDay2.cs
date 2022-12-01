using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class OffMeshLinkClimbDay2 : MonoBehaviour
{
    [SerializeField]
    private int offMeshArea = 3;
    [SerializeField]
    private float climbSpeed = 1.5f;
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    IEnumerator Start()
    {
        while(true)
        {
            // IsOnClimb() �Լ��� ��ȯ���� true�� �� ���� �ݺ�
            yield return new WaitUntil(() => IsOnClimb());

            // �ö󰡰ų� �������� �ൿ
            yield return StartCoroutine(ClimbOrDescend());
        }
    }
    public bool IsOnClimb()
    {
        //���� ������Ʈ�� ��ġ�� OffMeshLink�� �ִ��� (t/f)
        if(navMeshAgent.isOnOffMeshLink)
        {
            //���� ��ġ�� �ִ� OffMeshLink ������
            OffMeshLinkData linkData = navMeshAgent.currentOffMeshLinkData;

            // ���� : navMeshAgent.currentOffMeshLinkData.offMeshLink��
            // true�� ���� ������ OffMeshLink
            // false�� �ڵ� ������ OffMeshLink

            // ���� ��ġ�� �ִ� OffMeshLink�� �������� ������ OffMeshLink�̰�, ��� ������ Climb�̸�
            if(linkData.offMeshLink != null && linkData.offMeshLink.area == offMeshArea)
            {
                return true;
            }
        }

        return false;
    }

    private IEnumerator ClimbOrDescend()
    {
        // ������̼� �̵� ��� ����
        navMeshAgent.isStopped = true;

        // ���� ��ġ�� �ִ� OffMeshLink�� ����/���� ��ġ
        OffMeshLinkData linkData = navMeshAgent.currentOffMeshLinkData;
        Vector3 start = linkData.startPos;
        Vector3 end = linkData.endPos;

        // ���������� �ð� ����
        float climbTime = Mathf.Abs(end.y - start.y) / climbSpeed;
        float currentTime = 0.0f;
        float percent = 0.0f;

        while (percent < 1)
        {
            //�ܼ� deltaTime�� ���ϸ� ������ 1���Ŀ� percent�� 1�� �ǹǷ�
            // climbTime ������ ������ �ð��� ������
            currentTime += Time.deltaTime;
            percent = currentTime / climbTime;
            // �ð� ���(�ִ� 1)�� ���� ������Ʈ ��ġ �ٲ���
            transform.position = Vector3.Lerp(start, end, percent);

            yield return null;
        }

        // OffMeshLink �̿��� �̵� �Ϸ�
        navMeshAgent.CompleteOffMeshLink();
        // OffMeshLink �̵� �Ϸ� �Ǿ����� �׺���̼� �̵� �ٽ� ����
        navMeshAgent.isStopped = false;
    }
}
