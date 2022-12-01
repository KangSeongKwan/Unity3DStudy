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
        // 마우스 왼쪽버튼 클릭 시
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            // Camera.main : 태그가 "Camera"인 오브젝트 = Main Camera
            // 카메라로부터 마우스 좌표(Input.mousePosition) 위치를 관통하는 광선 생성
            // ray.origin : 광선 시작 위치
            // ray.direction : 광선의 진행 방향
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Physics.Raycast() : 광선을 발사해서 부딪힌 오브젝트 검출
            // 있으면 true 반환
            // ray.origin 위치로부터 ray.direction 방향으로 무한한 길이(Mathf.Infinity)의 광선 발사
            // 광선에 부딪히는 오브젝트의 정부를 hit에 저장
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                // hit.transform.position : 부딪힌 오브젝트 위치
                // hit.point : 광선과 오브젝트가 부딪힌 세부 위치

                // hit.point를 목표 위치로 이동
                movement3DDay2.MoveTO(hit.point);
            }
        }
    }
}
