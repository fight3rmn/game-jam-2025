using UnityEngine;
using System;

public class vision : MonoBehaviour
{
    public GameObject Lazer;
    public GameObject thisEnemy;
    public GameObject player2;
    public float aggroDist;
    public float timeBetweenHits;
    void OnTriggerEnter2D(Collider2D hit) {
        if(hit.gameObject.tag.Equals("Player")) {
            //Instantiate(Lazer, thisEnemy.gameObject.transform.position + thisEnemy.gameObject.transform.up * 7, Quaternion.identity);
            UnityEngine.Vector2 dist = new UnityEngine.Vector2(thisEnemy.transform.position.x - player2.transform.position.x, thisEnemy.transform.position.y - player2.transform.position.y);
            float xDist = (float)Math.Sqrt((dist.x * dist.x)/(UnityEngine.Vector2.Distance(thisEnemy.transform.position, player2.transform.position) * UnityEngine.Vector2.Distance(thisEnemy.transform.position, player2.transform.position)));
            float yDist = (float)Math.Sqrt((dist.y * dist.y)/(UnityEngine.Vector2.Distance(thisEnemy.transform.position, player2.transform.position) * UnityEngine.Vector2.Distance(thisEnemy.transform.position, player2.transform.position)));
            if(player2.transform.position.x < thisEnemy.transform.position.x) {
                xDist = -xDist;
            }
            if(player2.transform.position.y < thisEnemy.transform.position.y) {
                yDist = -yDist;
            }

            Lazer.GetComponent<lazerHit>().player1 = player2;
            Instantiate(Lazer, new Vector3(thisEnemy.transform.position.x + (xDist * 3), thisEnemy.transform.position.y + (yDist * 3), thisEnemy.transform.position.z), Quaternion.identity);
        }
    }
}
