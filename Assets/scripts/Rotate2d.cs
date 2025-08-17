using UnityEngine;
using UnityEngine.AI;

public class Rotate2d : MonoBehaviour
{
    [SerializeField] float speed;
    float startScaleX;
    int direction = -1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        startScaleX = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 curScale = transform.localScale;
        float newScaleX = curScale.x + speed * Time.deltaTime * direction;

        if ((direction == 1 && newScaleX >= startScaleX) ||
                direction == -1 && newScaleX <= -startScaleX) {
            direction = -direction; 
        }

        transform.localScale = new(newScaleX, curScale.y);
    }
}
