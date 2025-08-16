using UnityEngine;
using System;

public class enemyMovement : MonoBehaviour
{
    Vector3 startPosition;
    int direction = 2;
    int rotateDirection = 1;
    Vector3 curRotation;
    int rotationDist = 0;
    public float rotationSpeed;
    System.Boolean aggro = false;
    void Start() {
        startPosition = this.gameObject.transform.position;
        curRotation = this.gameObject.transform.eulerAngles;
        while(rotationDist == 0) {
            rotationDist = UnityEngine.Random.Range(-170, 171);
        }
    }
    void Update()
    {
        if(this.gameObject.transform.position.x - startPosition.x > 10) {
            direction = -2;
        }
        if(this.gameObject.transform.position.x - startPosition.x < 0) {
            direction = 2;
        }
        this.gameObject.transform.position += new Vector3(direction, 0, 0) * Time.deltaTime;
        /*if(rotationDist == 0 && aggro == false) {
            rotationDist = UnityEngine.Random.Range(-179, 179);
            if(rotationDist < 0) {
                rotateDirection = -1;
            }
            if(rotationDist > 0) {
                rotateDirection = 1;
            }
        }*/
        if(rotationDist < 0) {
            rotateDirection = -1;
        }
        if(rotationDist > 0) {
            rotateDirection = 1;
        }
        curRotation += new Vector3(0, 0, rotationSpeed * rotateDirection * Time.deltaTime);
        this.gameObject.transform.eulerAngles = curRotation;
        if(Math.Abs(curRotation.z) > Math.Abs(rotationDist) && aggro == false) {
            if(rotationDist < 0) {
                rotationDist = UnityEngine.Random.Range(1, 170);
                print(rotationDist);
            }
            else {
                rotationDist = UnityEngine.Random.Range(-1, -170);
                print(rotationDist);
            }
        }
    }
}
