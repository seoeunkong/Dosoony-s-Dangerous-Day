using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public Vector3 position;
    public CapsuleCollider capsuleCollider;
    


    Animator animator;
    float movementSpeed = 17f;

    
    Rigidbody rigid;
    bool jDown;
  
    bool DDown;
    bool isDodge;
    bool trampolineActive;
    bool isLand;
   
   
    
    GameObject nearitem;
    GameObject equipweapon;
   
    
    public int energy;
    
   

    public ParticleSystem p_system;
    int a;

    public bool dead;
    public bool goal;

    void Awake()
    {
        energy = 100; //플레이어의 기본 에너지(체력)를 100으로 설정함.
        rigid = GetComponent<Rigidbody>(); //중력 설정(점프할 때 이용)
        animator = GetComponentInChildren<Animator>();//애니메이션
        capsuleCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<CapsuleCollider>(); //콜라이더 모양을 게임중간에 변형하기 위해 필요
        dead = false; // 체력 혹은 시간이 다 떨어질 때를 판단하기 위한 변수
        goal = false; // 도착 지점에 도착했을 때의 판단 변수
    }

   

    void FixedUpdate()  //Update() 함수를 이용하면 플레이어 이동 도중에 카메라가 많이 흔들려 FixedUpdate() 사용
    {
      
        Movement();
        Interation();
        ParticleStart();
            charcter();
            Jump();
            Dodge();
        trampolineJump();
        ChageCollider();
        GameOver();
       
        
        

    }
    

    
    void Movement() //오브젝트 움직임
    {
        position.x += movementSpeed * Time.deltaTime * Input.GetAxisRaw("Horizontal"); //방향키 위,아래로 플레이어의 x좌표를 움직임
        position.z += movementSpeed * Time.deltaTime * Input.GetAxisRaw("Vertical"); // 방향키 오른쪽, 왼쪽으로 플레이어의 z좌표를 움직임
        Vector3 moveVec = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized; //부드럽게 움직이기 위해 normalized 사용

        transform.position = position;
        transform.LookAt(transform.position + moveVec); //움직이는 방향에 따라 플레이어 시선이 따라갈 수 있도록 설정.

    }


    void charcter() //오브젝트 애니메이션
    {
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0) //만약 움직이지 않는다면 isRun(달리는 애니메이션)을 비활성화시키고 기본 애니메이션만(ldie)만 활성화시킴
        {
            animator.SetBool("isRun", false);
        }
        else //움직인다면 isRun(달리는 애니메이션) 활성화시킴.
            animator.SetBool("isRun", true);
    }

    private void OnTriggerEnter(Collider other) //아이템 먹기
    {
            nearitem = other.gameObject; //닿은 아이템을 nearitem변수에 저장
    }

    private void OnTriggerExit(Collider other)
    {
            nearitem = null;  //닿은 범위를 나가면 nearitem 변수는 비워짐
    }
    
    void Jump() //점프
    {
        jDown = Input.GetButtonDown("Jump"); //스페이스바를 누를시 true로 받아짐.

        if (jDown&&rigid.velocity.y<5) //무한번 점프하는 것을 방지하기 위해 rigid.velocity.y<5라는 식을 조건으로 둠. 처음엔 rigid.velocity.y=0으로 하려다 원할 때 점프를 못하는 경우도 발생하여 식을 지금과 같이 변경함.
        {
            rigid.AddForce(Vector3.up * 1000, ForceMode.Impulse);//점프할 때의 이동 구현
            animator.SetTrigger("DoJump"); //점프하는 애니메이션 활성화
            animator.SetBool("IsJump", true); //착지하는 애니메이션 활성화
           
        }
        else
            animator.SetBool("IsJump", false);

    }

    void Dodge() //구르기
    {
        DDown = Input.GetButtonDown("Fire3"); //좌shift를 누르면 구르기가 활성화

        if (DDown )
        {
            movementSpeed *= 2;
            animator.SetTrigger("DoDodge"); //구르기 애니메이션 작동
            isDodge = true; 

            Invoke("DodgeOut", 0.4f); //시간차를 두고 구르기를 끝내는 함수를 불러옴
        }
        
    }

    void DodgeOut() // 구르기 끝났을 때를 구현하기 위한 조건
    {
        movementSpeed *= 0.5f;
        isDodge = false;
    }

    void ChageCollider() //구르기할때 콜라이더 크기 변경
    {
        if (isDodge == true) //구르기를 한다는 조건이 성립이 되면 콜라이더의 크기를 2로 변경(원래의 높이:4)
        {
            capsuleCollider.height = 2;
        }
        else
        {
            capsuleCollider.height = 4;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "car1") //장애물 자동차와 닿았을 때
        {
            
            animator.SetTrigger("isDie"); // 장애물과 접촉했을 때 애니메이션 작동
            Damage(); //energy(체력)을 감소하는 함수를 불러옴
        }
        else if (collision.gameObject.tag == "car2") // 장애물 자동차와 닿았을 때. 단, car1과 다르게 car2는 속도가 더 높다. 따라서 태그를 구분지어서 함.
        {

            animator.SetTrigger("isDie");
            Damage();
        }
        else if (collision.gameObject.tag == "jumping") //점핑 도구를 닿았을 때
        {
            trampolineActive = true; //점핑 도구와 닿았는지에 관한 판단을 위한 변수를 설정함.
        }
        else if (collision.gameObject.tag == "obstacle1") //자동차 이외의 모든 장애물들에 닿았을 때
        {

            animator.SetTrigger("isDie");
            Damage();

        }
        else if (collision.gameObject.tag == "Finish") //도착지점(Finish)에 닿았을 때
        {

            goal = true;

        }


    }

   
    void trampolineJump() //점프를 더 높게 해주는 함수
    {
        if (trampolineActive == true) //점핑도구와 닿으면 trampolineActive가 true로 받음.
        {
            rigid.AddForce(Vector3.up * 1500, ForceMode.Impulse); //기본점프보다 높게 뛰게 설정
            animator.SetBool("IsJump", true);
            animator.SetTrigger("DoJump");

            trampolineActive = false;
        }
    }

    public void Damage() //체력 피해 입었을 때의 함수
    {
        if (energy > 0) //만약 체력이 0이거나 작으면 더이상의 계산을 하지 않음.
        {
            
            energy -= 30;
        }
       
    }

   
    void Interation() //아이템 상호작용
    {
        if (nearitem != null) // 플레이어가 아이템을 먹어서 갖고 있는 nearitem이 빈 값이 아니여야 이 함수가 가능
        {
            if (energy < 100) //체력이 100이상이면 더이상의 계산을 하지 않음
            {
                energy += 20;

                if (energy > 100) //만약 체력이 초기엔 100이하였지만 아이템을 얻어 20이 추가되어 100이상이 되면 100으로 초기화함
                {
                    energy = 100;
                }
            }
            Destroy(nearitem); //아이템을 먹으면 없햄.
        }
       
    }

    

    void ParticleStart() //아이템을 먹었을 때의 파티클 효과
    {
       
        if (nearitem != null)
        {
            if (nearitem.tag == "energy") //nearitem이 체력이라면
            {
                 GetComponent<ParticleSystem>().Play();
               
            }
        }
    }


    void GameOver() //체력이 0일때의 게임오버인 함수
    {
        if (energy <= 0)
        {
            dead = true; //감독 스크립트에 넘길 변수 설정. 
        }
       
    }


}
