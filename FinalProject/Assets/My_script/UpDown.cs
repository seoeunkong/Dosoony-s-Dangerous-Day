using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDown : MonoBehaviour
{
    public float distance = 5f; //��ֹ��� �̵��ϴ� �Ÿ�
    public float speed = 3f;
    public float offset = 1.0f; //������������ ��ġ�� �����ϱ� ����

    private bool isForward = true; //�������� �Ϸ�������
    private Vector3 startPos;
   

    void Start()
    {
        startPos = transform.position; //������ġ
        
            transform.position += Vector3.up * offset;

    }

    void Update()
    {
        
			if (isForward)
			{
				if (transform.position.y < startPos.y + distance) //���� �̵�
				{
					transform.position += Vector3.up * Time.deltaTime * speed;
				}
				else
					isForward = false;
			}
			else
			{
				if (transform.position.y > startPos.y) //�Ʒ��� �̵�
				{
					transform.position -= Vector3.up * Time.deltaTime * speed;
				}
				else
					isForward = true;
			}
		
        
    }
}
