using UnityEngine;

public class MapMoving : MonoBehaviour
{
    public GameObject scrollView; // ScrollView ���� ������Ʈ ����
    public RectTransform mapTransform; // ���� �̹����� RectTransform ����
    public GameObject buttonNext;
    public GameObject buttonPrevious;
    public GameObject stage0Button;
    public GameObject stage1Button;
    public GameObject stage2Button;

    public Vector3 targetPosition0;
    public Vector3 targetPosition1;
    public Vector3 targetPosition2;
    public float animationSpeed = 2f; // �ִϸ��̼� �ӵ�

    private bool isAnimating = false; // �ִϸ��̼��� ���� ������ ���θ� ��Ÿ���� ����
    private int targetIndex; // ���� ��ǥ ��ġ�� �ε���
    private Vector3 initialPosition; // �ʱ� ��ġ ���� ����

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
        // �ִϸ��̼� ����
        if (isAnimating)
        {
            // Lerp�� ����Ͽ� �ε巴�� �̵�
            if (targetIndex == 0)
            {
                mapTransform.position = Vector3.Lerp(mapTransform.position, targetPosition0, Time.deltaTime * animationSpeed);

                // �̵��� �Ÿ��� ���� �� ���Ϸ� ������ �ִϸ��̼� ����
                if (Vector3.Distance(mapTransform.position, targetPosition0) < 0.1f)
                {
                    isAnimating = false;
                }
            }
            else if (targetIndex == 1)
            {
                mapTransform.position = Vector3.Lerp(mapTransform.position, targetPosition1, Time.deltaTime * animationSpeed);

                // �̵��� �Ÿ��� ���� �� ���Ϸ� ������ �ִϸ��̼� ����
                if (Vector3.Distance(mapTransform.position, targetPosition1) < 0.1f)
                {
                    isAnimating = false;
                }
            }
            else if (targetIndex == 2)
            {
                mapTransform.position = Vector3.Lerp(mapTransform.position, targetPosition2, Time.deltaTime * animationSpeed);

                // �̵��� �Ÿ��� ���� �� ���Ϸ� ������ �ִϸ��̼� ����
                if (Vector3.Distance(mapTransform.position, targetPosition2) < 0.1f)
                {
                    isAnimating = false;
                }
            }
        }
    }

    // ȭ��ǥ ��ư Ŭ�� �� ȣ��� �Լ�
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
