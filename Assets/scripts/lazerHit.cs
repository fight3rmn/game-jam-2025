using UnityEngine;
using System;

public class lazerHit : MonoBehaviour
{
    public GameObject lazer;
    public GameObject player1;
    float timer;
    float x;
    float initialX;
    float initialY;
    public float angle;
    public float timeOnScreen;
    public float speed;
    float xSpeed;
    float ySpeed;
    void Start()
    {
        initialX = player1.transform.position.x - lazer.transform.position.x;
        initialY = player1.transform.position.y - lazer.transform.position.y;
        x = (float)Math.Atan((initialY)/(initialX));
        if(player1.transform.position.x < lazer.transform.position.x) {
            lazer.transform.eulerAngles = new Vector3(0,0,90+(float)(x * 180/3.1415926535898));
        }
        else {
            lazer.transform.eulerAngles = new Vector3(0,0,-90+(float)(x * 180/3.1415926535898));
        }
        xSpeed = (float)Math.Sqrt((initialX * initialX)/(Vector2.Distance(player1.transform.position, lazer.transform.position) * Vector2.Distance(player1.transform.position, lazer.transform.position)));
        ySpeed = (float)Math.Sqrt((initialY * initialY)/(Vector2.Distance(player1.transform.position, lazer.transform.position) * Vector2.Distance(player1.transform.position, lazer.transform.position)));
        if(Math.Abs(xSpeed) > Math.Abs(ySpeed)) {
            float xAngle = (float)Math.Asin(xSpeed);
            xSpeed = (float)Math.Sin(xAngle + angle);
            ySpeed = (float)Math.Cos(xAngle + angle);
        }
        else {
            float yAngle = (float)Math.Asin(ySpeed);
            ySpeed = (float)Math.Sin(yAngle + angle);
            xSpeed = (float)Math.Cos(yAngle + angle);
        }
        if(player1.transform.position.x < lazer.transform.position.x) {
            xSpeed = -xSpeed;
        }
        if(player1.transform.position.y < lazer.transform.position.y) {
            ySpeed = -ySpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D hit) {
        //if(!hit.gameObject.tag.Equals("enemy")) {
            Destroy(lazer);
        //}
    }
    void Update()
    {
        timer += Time.deltaTime;
        lazer.transform.position += new Vector3(xSpeed * speed * Time.deltaTime, ySpeed * speed * Time.deltaTime, 0);
        if(timer >= timeOnScreen) {
            timer = 0;
            Destroy(lazer);
        }
    }
}
