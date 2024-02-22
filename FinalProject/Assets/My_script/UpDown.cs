using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDown : MonoBehaviour
{
    public float distance = 5f; //장애물을 이동하는 거리
    public float speed = 3f;
    public float offset = 1.0f; //시작지점에서 위치를 수정하기 위해

    private bool isForward = true; //움직임을 완료했으면
    private Vector3 startPos;
   

    void Start()
    {
        startPos = transform.position; //시작위치
        
            transform.position += Vector3.up * offset;

    }

    void Update()
    {
        
			if (isForward)
			{
				if (transform.position.y < startPos.y + distance) //위로 이동
				{
					transform.position += Vector3.up * Time.deltaTime * speed;
				}
				else
					isForward = false;
			}
			else
			{
				if (transform.position.y > startPos.y) //아래로 이동
				{
					transform.position -= Vector3.up * Time.deltaTime * speed;
				}
				else
					isForward = true;
			}
		
        
    }
}
