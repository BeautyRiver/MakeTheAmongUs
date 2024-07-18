using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewFloater : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab; // ��ȯ�� ũ��� ������
    [SerializeField]
    private List<Sprite> sprites; // ũ��� �̹��� ����Ʈ

    private bool[] crewStates = new bool[12]; // �� ũ����� ���¸� �����ϴ� �迭 (�� 12���� ũ���)
    private float timer = 0.5f; // ũ��� ��ȯ �ֱ⸦ ���� Ÿ�̸�
    private float distance = 11f; // �߽����κ��� ũ����� ��ȯ�� �Ÿ�

    void Start()
    {
        // ���� ���� �� 12���� ũ����� ��ȯ
        for (int i = 0; i < 12; i++)
        {
            SpawnFloatingCrew((EPlayerColor)i, Random.Range(0f, distance)); // ������ �Ÿ����� ũ��� ��ȯ
        }
    }

    void Update()
    {
        timer -= Time.deltaTime; // �� �����Ӹ��� Ÿ�̸Ӹ� ���ҽ�Ŵ
        if (timer <= 0f) // Ÿ�̸Ӱ� 0 ���ϰ� �Ǹ�
        {
            SpawnFloatingCrew((EPlayerColor)Random.Range(0, 12), distance); // ������ ũ��� ��ȯ
            timer = 1f; // Ÿ�̸Ӹ� 1�ʷ� ����
        }
    }

    public void SpawnFloatingCrew(EPlayerColor playerColor, float dist)
    {
        // �ش� ũ����� ���� ��ȯ���� �ʾҴٸ�
        if (!crewStates[(int)playerColor])
        {
            crewStates[(int)playerColor] = true; // �ش� ũ����� ��ȯ ���·� ����
            float angle = Random.Range(0f, 360f); // ��ȯ�� ������ �������� ����
            Vector3 spawnPos = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0f) * dist; // ������ �Ÿ��� ������� ��ȯ ��ġ ���
            Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f); // �̵� ������ �������� ����
            float floatingSpeed = Random.Range(1f, 2.5f); // ���� �ӵ��� �������� ����
            float rotateSpeed = Random.Range(-1.2f, 1.2f); // ȸ�� �ӵ��� �������� ����
            float size = Random.Range(0.5f, 1f); // ũ����� ũ�⸦ �������� ����
            var crew = Instantiate(prefab, spawnPos, Quaternion.identity).GetComponent<FloatingCrew>(); // �������� ��ȯ�ϰ� FloatingCrew ������Ʈ�� ������
            crew.SetFloatingCrew(sprites[Random.Range(0, sprites.Count)], playerColor, direction, floatingSpeed, rotateSpeed, size); // ũ��� ����
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var crew = collision.GetComponent<FloatingCrew>(); // �浹�� ��ü���� FloatingCrew ������Ʈ�� ������
        if (crew != null)
        {
            crewStates[(int)crew.playerColor] = false; // �ش� ũ����� ���¸� ��Ȱ��ȭ�� ����
            Destroy(crew.gameObject); // ũ��� ������Ʈ�� ����
        }
    }
}
