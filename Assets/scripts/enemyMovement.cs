using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    Vector3 startPosition;
    int direction = 2;
    void Start() {
        startPosition = this.gameObject.transform.position;
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
    }
}
