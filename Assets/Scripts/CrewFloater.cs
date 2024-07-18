using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewFloater : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab; // 소환할 크루원 프리팹
    [SerializeField]
    private List<Sprite> sprites; // 크루원 이미지 리스트

    private bool[] crewStates = new bool[12]; // 각 크루원의 상태를 저장하는 배열 (총 12개의 크루원)
    private float timer = 0.5f; // 크루원 소환 주기를 위한 타이머
    private float distance = 11f; // 중심으로부터 크루원이 소환될 거리

    void Start()
    {
        // 게임 시작 시 12명의 크루원을 소환
        for (int i = 0; i < 12; i++)
        {
            SpawnFloatingCrew((EPlayerColor)i, Random.Range(0f, distance)); // 무작위 거리에서 크루원 소환
        }
    }

    void Update()
    {
        timer -= Time.deltaTime; // 매 프레임마다 타이머를 감소시킴
        if (timer <= 0f) // 타이머가 0 이하가 되면
        {
            SpawnFloatingCrew((EPlayerColor)Random.Range(0, 12), distance); // 무작위 크루원 소환
            timer = 1f; // 타이머를 1초로 리셋
        }
    }

    public void SpawnFloatingCrew(EPlayerColor playerColor, float dist)
    {
        // 해당 크루원이 아직 소환되지 않았다면
        if (!crewStates[(int)playerColor])
        {
            crewStates[(int)playerColor] = true; // 해당 크루원을 소환 상태로 설정
            float angle = Random.Range(0f, 360f); // 소환될 각도를 무작위로 설정
            Vector3 spawnPos = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0f) * dist; // 각도와 거리를 기반으로 소환 위치 계산
            Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f); // 이동 방향을 무작위로 설정
            float floatingSpeed = Random.Range(1f, 2.5f); // 부유 속도를 무작위로 설정
            float rotateSpeed = Random.Range(-1.2f, 1.2f); // 회전 속도를 무작위로 설정
            float size = Random.Range(0.5f, 1f); // 크루원의 크기를 무작위로 설정
            var crew = Instantiate(prefab, spawnPos, Quaternion.identity).GetComponent<FloatingCrew>(); // 프리팹을 소환하고 FloatingCrew 컴포넌트를 가져옴
            crew.SetFloatingCrew(sprites[Random.Range(0, sprites.Count)], playerColor, direction, floatingSpeed, rotateSpeed, size); // 크루원 설정
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var crew = collision.GetComponent<FloatingCrew>(); // 충돌한 객체에서 FloatingCrew 컴포넌트를 가져옴
        if (crew != null)
        {
            crewStates[(int)crew.playerColor] = false; // 해당 크루원의 상태를 비활성화로 설정
            Destroy(crew.gameObject); // 크루원 오브젝트를 삭제
        }
    }
}
