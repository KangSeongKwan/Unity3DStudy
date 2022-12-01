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
        //기존에 이동행동을 하고있었으면 코루틴 중지
        StopCoroutine("OnMove");
        // 이동 속도 설정
        navMeshAgent.speed = moveSpeed;
        // 목표지점 설정(경로 계산 후 알아서 움직임)
        navMeshAgent.SetDestination(goalPosition);
        StartCoroutine("OnMove");
    }

    IEnumerator OnMove()
    {
        while(true)
        {
            //목표 위치(navMeshAgent.destination)와 내 위치(transform.position) 의 거리가 0.1 미만일 때
            // 목표 위치에 거의 도착했을 때
            if (Vector3.Distance(navMeshAgent.destination, transform.position) < 0.1f)
            {
                transform.position = navMeshAgent.destination;
                // SetDestination() 에 의해 설정된 경로를 초기화, 이동을 멈춘다.
                navMeshAgent.ResetPath();

                break;
            }

            yield return null;
        }
    }
}
