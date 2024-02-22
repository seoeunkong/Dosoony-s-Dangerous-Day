using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
	public float speed = 1.5f;
	public float limit = 75f; //움직임 정도의 제한
	public bool randomStart = false; //시작 위치를 수정하기 위해
	private float random = 0;

	void Awake()
    {
		if(randomStart) //이동 범위를 랜덤으로
			random = Random.Range(0f, 1f);
	}

    void Update() 
    {
		float angle = limit * Mathf.Sin(Time.time + random * speed); //반복운동
		transform.localRotation = Quaternion.Euler(0, 0, angle);
	}
}
