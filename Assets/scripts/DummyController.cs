using UnityEngine;

public class DummyController : MonoBehaviour, ISabotagable
{
    [SerializeField] bool hasTech = false;
    [SerializeField] float iconScale = 1.0f;
    [SerializeField] HealthBar healthBar;
    [SerializeField] float sabotageRate = 0.01f;
    [SerializeField] string ID;
    bool isSabotaging = false;


    SpriteRenderer rndr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        rndr = GetComponent<SpriteRenderer>();
        PlayerController.playerPositionHandler += HandlePlayerPosUpdate;
        HealthBar.healthFullHandler += HandleHealthFull;
    }

    void HandleHealthFull(string healthID)
    {
        if (healthID == ID)
        {
            Explode();
        }
    }

    void Explode()
    {
        GetComponentInChildren<HealthBar>().gameObject.SetActive(false);
        rndr.color = new(0, 0, 0, 0);
        GetComponent<ParticleSystem>().Play();
        GetComponent<Rigidbody2D>().simulated = false;
        if (hasTech) ShowTech();
    }

    void ShowTech()
    {
        GetComponentInChildren<PowerUp>().ShowPowerUp();
    }

    void HandlePlayerPosUpdate(float posY, int sortOrder)
    {
        rndr.sortingOrder = posY >= transform.position.y ? sortOrder + 1 : sortOrder - 1;
    }
    void Start()
    {
        gameObject.name = "testName";
    }

    // Update is called once per frame
    void Update()
    {
        if (isSabotaging)
        {
            healthBar.AddToHealth(sabotageRate * Time.deltaTime);
        }
    }

    public void IsTouching(bool touching)
    {
        isSabotaging = touching;
        //Highlight(touching);
    }

    void Highlight(bool doHighlight)
    {
        rndr.color = new(1, 1, doHighlight ? 0.25f : 1.0f);
    }

    public void Sabotage()
    {

    }

    void OnDisable()
    {
        PlayerController.playerPositionHandler -= HandlePlayerPosUpdate;    
    }
}
