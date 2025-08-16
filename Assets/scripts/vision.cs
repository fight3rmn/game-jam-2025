using UnityEngine;

public class vision : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D hit) {
        if(hit.gameObject.tag.Equals("Player")) {
            print("success");
        }
    }
}
