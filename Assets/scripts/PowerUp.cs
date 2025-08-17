using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    float startScale;
    Collider2D col;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startScale = transform.localScale.x;
        transform.localScale = Vector2.zero;
        col = GetComponent<Collider2D>();
        col.enabled = false;
    }

    public void ShowPowerUp()
    {
        DOTween.Sequence().Append(transform.DOScale(startScale, 0.5f).SetEase(Ease.OutBounce).SetLink(gameObject));
        col.enabled = true;
    }

    public void Die()
    {
        Debug.Log("tech picked up");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
