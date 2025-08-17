using UnityEngine;

public class cameraController : MonoBehaviour
{

    public GameObject player1;
    void Update()
    {
        this.gameObject.transform.position = player1.transform.position + new Vector3(0, 0, -70);
    }
}
