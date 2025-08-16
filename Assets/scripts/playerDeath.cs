using UnityEngine;
using UnityEngine.SceneManagement;

public class playerDeath : MonoBehaviour
{
    public int hp;
    void onCollisionEnter2D(Collision2D hit) {
        if(hit.gameObject.tag.Equals("Lazer")) {
            hp--;
            if(hp <= 0) {
                SceneManager.LoadScene(1);
            }
        }
    }
}
