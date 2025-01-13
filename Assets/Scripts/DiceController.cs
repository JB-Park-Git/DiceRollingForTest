using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceController : MonoBehaviour
{
    public int diceFaceNum; // 주사위 눈금
    public float posForce = 2f; // 물리 이동력 세기
    public float rotForce = 10f; // 물리 회전력 세기
    public float minRotSpeed = 200; // 최소 회전속도
    public float maxRotSpeed = 400; // 최대 회전속도
    public float followHeight = 2f; // 마우스 높이 고정 (Y축)
    bool isBeingHeld = false; // 마우스로 잡고 있는지 여부
    Vector3 mouseOffset; // 마우스와 주사위 사이의 거리
    Rigidbody rb;   // 주사위의 Rigidbody
    
    // 랜덤 회전 속도
    float rotationSpeedX;
    float rotationSpeedY;
    float rotationSpeedZ;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody 컴포넌트 가져오기

        // 랜덤한 회전 속도 설정
        rotationSpeedX = Random.Range(minRotSpeed, maxRotSpeed);
        rotationSpeedY = Random.Range(minRotSpeed, maxRotSpeed);
        rotationSpeedZ = Random.Range(minRotSpeed, maxRotSpeed);
    }

    void Update()
    {
        // 마우스 클릭으로 주사위 잡기
        if (Input.GetMouseButtonDown(0)) // 왼쪽 마우스 클릭
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // 마우스 위치에서 Raycast
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                isBeingHeld = true;
                mouseOffset = transform.position - hit.point; // 마우스와 주사위 사이의 오프셋 저장
                rb.isKinematic = true;
            }
        }

        // 마우스를 클릭하고 있는 동안 주사위가 마우스를 따라오도록 처리
        if (isBeingHeld)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // 주사위의 원래 지점에 Y축 높이를 고정시켜 주사위를 이동
                Vector3 newPosition = hit.point + mouseOffset;
                newPosition.y = followHeight; // Y축 높이를 고정
                transform.position = newPosition;

                // 주사위가 랜덤하게 계속 회전하도록 처리
                transform.Rotate(Vector3.up, rotationSpeedX * Time.deltaTime, Space.World);
                transform.Rotate(Vector3.right, rotationSpeedY * Time.deltaTime, Space.World);
                transform.Rotate(Vector3.forward, rotationSpeedZ * Time.deltaTime, Space.World);
            }
        }

        // 마우스 버튼을 놓으면 주사위가 떨어지게 하기
        if (Input.GetMouseButtonUp(0) && isBeingHeld)
        {
            isBeingHeld = false;
            rb.isKinematic = false;

            // 주사위에 랜덤 힘을 가해 회전하며 떨어지게 함
            rb.AddForce(new Vector3(Random.Range(-posForce, posForce), posForce, Random.Range(-posForce, posForce)), ForceMode.Impulse);
            rb.AddTorque(new Vector3(Random.Range(-rotForce, rotForce), Random.Range(-rotForce, rotForce), Random.Range(-rotForce, rotForce)), ForceMode.Impulse);
        }
    }

    
}
