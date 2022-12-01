
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
        // 이동방향 설정 : (목표 위치 - 내 위치).정규화
        Vector3 direction = (paths[currentPath].position - transform.position).normalized;
        // 이동
        transform.position += direction * moveSpeed * Time.deltaTime;

        // 목표 위치에 거의 도달 했을 때
        if ((paths[currentPath].position - transform.position).sqrMagnitude < 0.1f )
        {
            // 목표 위치 변경(순찰 경로 순환)
            if (currentPath < paths.Length - 1) currentPath++;
            else currentPath = 0;
        }
    }
}
