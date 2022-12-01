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
            // IsOnClimb() 함수의 반환값이 true일 때 까지 반복
            yield return new WaitUntil(() => IsOnClimb());

            // 올라가거나 내려오는 행동
            yield return StartCoroutine(ClimbOrDescend());
        }
    }
    public bool IsOnClimb()
    {
        //현재 오브젝트의 위치가 OffMeshLink에 있는지 (t/f)
        if(navMeshAgent.isOnOffMeshLink)
        {
            //현재 위치에 있는 OffMeshLink 데이터
            OffMeshLinkData linkData = navMeshAgent.currentOffMeshLinkData;

            // 설명 : navMeshAgent.currentOffMeshLinkData.offMeshLink가
            // true면 수동 생성한 OffMeshLink
            // false면 자동 생성한 OffMeshLink

            // 현재 위치에 있는 OffMeshLink가 수동으로 생성한 OffMeshLink이고, 장소 정보가 Climb이면
            if(linkData.offMeshLink != null && linkData.offMeshLink.area == offMeshArea)
            {
                return true;
            }
        }

        return false;
    }

    private IEnumerator ClimbOrDescend()
    {
        // 내비게이션 이동 잠시 중지
        navMeshAgent.isStopped = true;

        // 현재 위치에 있는 OffMeshLink의 시작/종료 위치
        OffMeshLinkData linkData = navMeshAgent.currentOffMeshLinkData;
        Vector3 start = linkData.startPos;
        Vector3 end = linkData.endPos;

        // 오르내리는 시간 설정
        float climbTime = Mathf.Abs(end.y - start.y) / climbSpeed;
        float currentTime = 0.0f;
        float percent = 0.0f;

        while (percent < 1)
        {
            //단순 deltaTime만 더하면 무조건 1초후에 percent가 1이 되므로
            // climbTime 변수를 연산해 시간을 조절함
            currentTime += Time.deltaTime;
            percent = currentTime / climbTime;
            // 시간 경과(최대 1)에 따라 오브젝트 위치 바꿔줌
            transform.position = Vector3.Lerp(start, end, percent);

            yield return null;
        }

        // OffMeshLink 이용한 이동 완료
        navMeshAgent.CompleteOffMeshLink();
        // OffMeshLink 이동 완료 되었으니 네비게이션 이동 다시 시작
        navMeshAgent.isStopped = false;
    }
}
