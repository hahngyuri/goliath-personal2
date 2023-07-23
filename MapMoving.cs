using UnityEngine;

public class MapMoving : MonoBehaviour
{
    public GameObject scrollView; // ScrollView 게임 오브젝트 연결
    public RectTransform mapTransform; // 지도 이미지의 RectTransform 연결
    public GameObject buttonNext;
    public GameObject buttonPrevious;
    public GameObject stage0Button;
    public GameObject stage1Button;
    public GameObject stage2Button;

    public Vector3 targetPosition0;
    public Vector3 targetPosition1;
    public Vector3 targetPosition2;
    public float animationSpeed = 2f; // 애니메이션 속도

    private bool isAnimating = false; // 애니메이션이 진행 중인지 여부를 나타내는 변수
    private int targetIndex; // 다음 목표 위치의 인덱스
    private Vector3 initialPosition; // 초기 위치 저장 변수

    private void Start()
    {
        targetPosition0 = new Vector3(0f, 0f, 0f);
        targetPosition1 = new Vector3(-964f, 52f, 0f);
        targetPosition2 = new Vector3(-1154f, 372f, 0f);
        initialPosition = targetPosition1;
        targetIndex = 0;
    }

    private void Update()
    {
        // 애니메이션 진행
        if (isAnimating)
        {
            // Lerp를 사용하여 부드럽게 이동
            if (targetIndex == 0)
            {
                mapTransform.position = Vector3.Lerp(mapTransform.position, targetPosition0, Time.deltaTime * animationSpeed);

                // 이동한 거리가 일정 값 이하로 남으면 애니메이션 종료
                if (Vector3.Distance(mapTransform.position, targetPosition0) < 0.1f)
                {
                    isAnimating = false;
                }
            }
            else if (targetIndex == 1)
            {
                mapTransform.position = Vector3.Lerp(mapTransform.position, targetPosition1, Time.deltaTime * animationSpeed);

                // 이동한 거리가 일정 값 이하로 남으면 애니메이션 종료
                if (Vector3.Distance(mapTransform.position, targetPosition1) < 0.1f)
                {
                    isAnimating = false;
                }
            }
            else if (targetIndex == 2)
            {
                mapTransform.position = Vector3.Lerp(mapTransform.position, targetPosition2, Time.deltaTime * animationSpeed);

                // 이동한 거리가 일정 값 이하로 남으면 애니메이션 종료
                if (Vector3.Distance(mapTransform.position, targetPosition2) < 0.1f)
                {
                    isAnimating = false;
                }
            }
        }
    }

    // 화살표 버튼 클릭 시 호출될 함수
    public void MoveToNextPosition()
    {
        if (!isAnimating)
        {
            targetIndex++;
            if (targetIndex >= 3)
                targetIndex = 0;

            isAnimating = true;
            
        }
    }

    public void MoveToPreviousPosition()
    {
        if (!isAnimating)
        {
            targetIndex--;
            if (targetIndex < 0)
                targetIndex = 2;

            isAnimating = true;
        }
    }
}
