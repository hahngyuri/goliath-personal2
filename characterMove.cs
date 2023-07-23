using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
/*
public enum Direction
{
    Up = 0,
    Left = 1,
    Right = 2,
    Down = 3,
    UL = 4,
    UR = 5,
    DL = 6,
    DR = 7
}


public class characterMove : MonoBehaviour
{
    public Direction direction;         // 해당 유닛이 움직일 방향
    public int block;                   // 유닛이 움직일 블럭 수

    //애니메이션 = 24프레임, 1프레임 당 움직일 거리 
    private float walkSpeed = 5f;         // 블럭 2개 이하 시, walk
    private float runSpeed = 10f;          // 블럭 3개 이상 시, run  
    private Animator animator;

    // 유닛의 위치 정보
    private float init_PosX;
    private float init_PosY;
    private float init_PosZ;
    private float init_RotX;
    private float init_RotY;
    private float init_RotZ;

    private bool initflag;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        initflag = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isMove == true)
        {
            if(initflag == true)
            {
                init_pos();
                initflag = false;
            }
            else
            {
                Move(direction, block);
            }
        }

        if (isAttack == true)
        {

        }

        if (isDeath == true) 
        { 

        }

    }

    void init_pos()
    {
        init_PosX = transform.position.x;
        init_PosY = transform.position.y;
        init_PosZ = transform.position.z;
        init_RotX = transform.eulerAngles.x;
        init_RotY = transform.eulerAngles.y;
        init_RotZ = transform.eulerAngles.z;
    }

    void Move(Direction direction, int block)
    {
        if (direction == Direction.Up) 
        {
            if(transform.position.z >= init_PosZ + 10.0*block)
            {
                initflag = true;
                isMove =false;
            }
            else
            {
                animator.Play("walking");
            }
        }
        if (direction == Direction.Down)
        {
            animator.Play("turn_180");
            if (transform.position.z <= init_PosZ - 10.0 * block)
            {
                animator.Play("turn_min180");
                initflag = true;
                isMove = false;
            }
            else
            {
                animator.Play("walking");
            }


        }
        if (direction == Direction.Left)
        {
            animator.Play("turn_min90");
            if (transform.position.x <= init_PosX - 10.0 * block)
            {
                animator.Play("turn_90");
                initflag = true;
                isMove = false;
            }
            else
            {
                animator.Play("walking");
            }
        }
        if(direction == Direction.Right)
        {
            animator.Play("turn_90");
            if (transform.position.x >= init_PosX + 10.0 * block)
            {
                animator.Play("turn_min90");
                initflag = true;
                isMove = false;
            }
            else
            {
                animator.Play("walking");
            }
        }
        if(direction == Direction.UL)
        {
            animator.Play("turn_45");
            if (transform.position.x <= init_PosX - 10.0 * block && transform.position.z >= init_PosZ + 10.0 * block)
            {
                animator.Play("turn_min45");
                initflag = true;
                isMove = false;
            }
            else
            {
                animator.Play("walking");
            }
        }
        if(direction == Direction.UR)
        {
            animator.Play("turn_min45");
            if (transform.position.x >= init_PosX + 10.0 * block && transform.position.z >= init_PosZ + 10.0 * block)
            {
                animator.Play("turn_45");
                initflag = true;
                isMove = false;
            }
            else
            {
                animator.Play("walking");
            }
        }
        if (direction == Direction.DL)
        {
            animator.Play("turn_min135");
            if (transform.position.x <= init_PosX - 10.0 * block && transform.position.z <= init_PosZ - 10.0 * block)
            {
                animator.Play("turn_135");
                initflag = true;
                isMove = false;
            }
            else
            {
                animator.Play("walking");
            }
        }
        if (direction == Direction.DR)
        {
            animator.Play("turn_135");
            if (transform.position.x >= init_PosX + 10.0 * block && transform.position.z <= init_PosZ - 10.0 * block)
            {
                animator.Play("turn_min135");
                initflag = true;
                isMove = false;
            }
            else
            {
                animator.Play("walking");
            }
        }
    }

}
*/
