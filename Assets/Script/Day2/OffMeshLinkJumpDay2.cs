using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class OffMeshLinkJumpDay2 : MonoBehaviour
{
    [SerializeField]
    private float jumpSpeed = 10.0f;
    [SerializeField]
    private float gravity = -9.81f;
    private NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    IEnumerator Start()
    {
        while(true)
        {
            // IsOnJump() 함수의 반환값이 true일 때 까지 반복호출
            yield return new WaitUntil(() => IsOnJump());

            // 점프 행동
            yield return StartCoroutine(JumpTO());
        }
    }

    public bool IsOnJump()
    {
        if (navMeshAgent.isOnOffMeshLink)
        {
            //현재 위치 OffMeshLink 데이터
            OffMeshLinkData linkData = navMeshAgent.currentOffMeshLinkData;

            // OffMeshLinkType은 Manual = 0, DropDown = 1, JumpAcross = 2로
            // 자동 생성한 OffMeshLink의 속성 구분 위해 사용(1, 2)

            // 현재 위치에 있는 OffMeshLink의 OffMeshLinkType이 JumpAcross이면
            if (linkData.linkType == OffMeshLinkType.LinkTypeJumpAcross ||
                linkData.linkType == OffMeshLinkType.LinkTypeDropDown)
            {
                return true;
            }
        }

        return false;
    }

    IEnumerator JumpTO()
    {
        // 내비게이션을 이용한 이동을 잠시 중지
        navMeshAgent.isStopped = true;

        // 현재 위치에 있는 OffMeshLink 시작/종료 위치
        OffMeshLinkData linkData = navMeshAgent.currentOffMeshLinkData;
        Vector3 start = transform.position;
        Vector3 end = linkData.endPos;

        // 뛰어서 이동하는 시간 설정
        float jumpTime = Mathf.Max(0.3f, Vector3.Distance(start, end) / jumpSpeed);
        float currentTime = 0.0f;
        float percent = 0.0f;

        // y방향의 초기 속도
        float v0 = (end - start).y - gravity;

        while(percent < 1)
        {
            // 단순히 deltaTime만 더하면 무조건 1초 후에 percent가 1이 되기 때문에
            // jumpTime 변수를 연산해서 시간을 조절한다
            currentTime += Time.deltaTime;
            percent = currentTime / jumpTime;
            // 시간 경과(최대 1)에 따라 오브젝트의 위치(x, z)를 바꿔준다.
            Vector3 position = Vector3.Lerp(start, end, percent);
            // 시간 경과에 따라 오브젝트의 위치(y)를 바꿔준다
            // 포물선 운동 : 시작 위치 + 초기 속도*시간 + 중력*시간제곱
            position.y = start.y + (v0 * percent) + (gravity * percent * percent);
            // 위에서 계산한 x, y, z 위치 값을 실제 오브젝트에 대입
            transform.position = position;

            yield return null;
        }
        // OffMeshLink 이동 완료
        navMeshAgent.CompleteOffMeshLink();
        // OffMeshLink 이동 완료 되었으니 네비게이션 이용한 이동 다시 시작
        navMeshAgent.isStopped = false;
    }
}
