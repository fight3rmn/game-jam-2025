using UnityEngine;

public class ColorCycler : MonoBehaviour
{
    [SerializeField] float minValue;
    [SerializeField] float maxValue;
    [SerializeField] float speed = 1.0f;
    int direction = -1;
    int colorIndex = 2;
    float[] colors;
    SpriteRenderer rndr;

    void Start()
    {
        colors = new[] { minValue, maxValue, maxValue };
        rndr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        colors[colorIndex] += speed * direction * Time.deltaTime;
        if (direction == -1)
        {
            if (colors[colorIndex] <= minValue)
            {
                colors[colorIndex] = minValue;
                NextStage();
            }
        }
        else
        {
            if (colors[colorIndex] >= maxValue)
            {
                colors[colorIndex] = maxValue;
                NextStage();
            }
        }
        rndr.color = new(colors[0], colors[1], colors[2]);
    }

    void NextStage()
    {
        direction = -direction;
        colorIndex += 1;
        colorIndex = colorIndex == colors.Length ? 0 : colorIndex;
    }
}
