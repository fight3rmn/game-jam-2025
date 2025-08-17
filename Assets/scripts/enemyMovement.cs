using UnityEngine;
using System;
using System.Collections;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using Unity.VisualScripting;
using DG.Tweening;

public class enemyMovement : MonoBehaviour
{
    //Vector3 startPosition;
    //System.Boolean aggro = false;

    [SerializeField] Transform[] destinations;
    public GameObject visionCone;
    public GameObject thisEnemy;
    int curDestination = 0;
    //public Animator anim;
    int curDirection;
    void Start()
    {
        GotoDestination();
    }

    void GotoDestination()
    {
        DOTween.Sequence()
        //.AppendCallback(directionalSprite)
        .Append(transform.DOMove(destinations[curDestination].position, 5.0f))
        .AppendCallback(IncrementDestination)
        .SetLink(gameObject);
    }

    void IncrementDestination()
    {
        if (curDestination < destinations.Length - 1)
        {
            curDestination++;
        }
        else
        {
            curDestination = 0;
        }
        GotoDestination();
    }
    /*void directionalSprite()
    {
        Vector2 distances = destinations[curDestination].position - transform.position;
        if (Math.Abs(distances.x) > Math.Abs(distances.y))
        {
            if (distances.x < 0)
            {
                curDirection = 0;
            }
            else
            {
                curDirection = 1;
            }
        }
        else
        {
            if (distances.y < 0)
            {
                curDirection = 2;
            }
            else
            {
                curDirection = 3;
            }
        }
        anim.SetInteger("curDirection", curDirection);
    }*/
    // enum Direction
    // {
    //     UP, DOWN, LEFT, RIGHT,
    //     UP_LEFT, DOWN_LEFT, UP_RIGHT, DOWN_RIGHT,
    //     IDLE
    // }
    // Direction InputDirection(Vector2 input)
    // {
    //     if (input.x == 0 && input.y > 0) return Direction.UP;
    //     if (input.x == 0 && input.y < 0) return Direction.DOWN;
    //     if (input.x < 0 && input.y == 0) return Direction.LEFT;
    //     if (input.x > 0 && input.y == 0) return Direction.RIGHT;
    //     if (input.x < 0 && input.y > 0) return Direction.UP_LEFT;
    //     if (input.x < 0 && input.y < 0) return Direction.DOWN_LEFT;
    //     if (input.x > 0 && input.y > 0) return Direction.UP_RIGHT;
    //     if (input.x > 0 && input.y < 0) return Direction.DOWN_RIGHT;
    //     return Direction.IDLE;
    // }
    void Update()
    {

        UnityEngine.Vector2 dist = new UnityEngine.Vector2(thisEnemy.transform.position.x - destinations[curDestination].position.x, thisEnemy.transform.position.y - destinations[curDestination].position.y);
        float xDist = (float)Math.Sqrt((dist.x * dist.x) / (UnityEngine.Vector2.Distance(thisEnemy.transform.position, destinations[curDestination].position) * UnityEngine.Vector2.Distance(thisEnemy.transform.position, destinations[curDestination].position)));
        float yDist = (float)Math.Sqrt((dist.y * dist.y) / (UnityEngine.Vector2.Distance(thisEnemy.transform.position, destinations[curDestination].position) * UnityEngine.Vector2.Distance(thisEnemy.transform.position, destinations[curDestination].position)));
        if (destinations[curDestination].position.x < thisEnemy.transform.position.x)
        {
            xDist = -xDist;
        }
        if (destinations[curDestination].position.y < thisEnemy.transform.position.y)
        {
            yDist = -yDist;
        }
        visionCone.transform.position = new Vector3(thisEnemy.transform.position.x + (xDist), thisEnemy.transform.position.y + (yDist), thisEnemy.transform.position.z);
        visionCone.GetComponent<vision>().destination = destinations[curDestination];
        // Vector2 input = destinations[curDestination].position - this.gameObject.transform.position;
        // if (Vector2.Distanc <= 0.1 && curDestination < 3)
        // {
        //     curDestination++;
        // }
        // else if (input <= 0.1)
        // {
        //     curDestination = 0;
        // }

        // Direction thisDirection = InputDirection(input);
        // if (thisDirection != currentDirection)
        // {
        //     currentDirection = thisDirection;
        //     //Debug.Log("new direction: " + currentDirection.ToString());
        //     anim.SetTrigger(currentDirection.ToString());
        // }
        // transform.position += new Vector3(
        //     input.x * moveSpeed * Time.deltaTime,
        //     input.y * moveSpeed * Time.deltaTime, 0);

    }
}
