using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics.Contracts;
using DG.Tweening;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public delegate void HealthFullHandler(string ID);
    public static HealthFullHandler healthFullHandler;
    public GameObject Mask;
    public GameObject Fill;
    public ParticleSystem particles;
    AudioSource snd;
    enum HealthBarState
    {
        idle,
        filling,
        emptying
    }
    HealthBarState state = HealthBarState.idle;
    const float fillDuration = 0.0f;
    float fillTimer;
    Vector3 startMaskPos;
    float maskPosXTotalDelta;
    float health = 0;
    float lastHealth;
    float lastAddAmount;
    float progress;
    float startMaskPosX;
    float endMaskPosX;
    float maskPosDelta;
    float dingTimer = 0;
    const float dingInterval = 0.25f;
    Vector2 startScale;
    float contractTimer;
    [SerializeField] float contractInterval = 1.0f;
    [SerializeField] float contractDuration = 0.25f;
    [SerializeField] string ID;

    void Start()
    {
        startMaskPos = Mask.transform.position;
        endMaskPosX = startMaskPos.x;
        maskPosXTotalDelta = Fill.transform.position.x - endMaskPosX;
        snd = GetComponent<AudioSource>();
        startScale = transform.localScale;
        transform.localScale = Vector2.zero;
    }

    void Expand()
    {
        particles.Play();
        contractTimer = contractInterval;
        DOTween.Sequence()
        .Append(transform.DOScale(startScale.x, contractDuration).SetEase(Ease.OutBounce))
        //.AppendCallback(() => { particles.Play(); })
        .SetLink(gameObject);
    }

    void Contract()
    {
        particles.Stop();
        contractTimer = 0;
        DOTween.Sequence().Append(transform.DOScale(0, contractDuration).SetEase(Ease.OutExpo).SetLink(gameObject));
    }

    public void AddToHealth(float amount)
    {
        if (contractTimer == 0)
        {
            Expand();
            if (health <= 0) contractTimer = contractInterval;
        }

        if (state != HealthBarState.idle)
        {
            health = lastHealth + lastAddAmount * progress;
        }

        bool isFilling = amount > 0;
        health = isFilling
            ? Math.Min(health + amount, 1.0f)
            : Math.Max(health + amount, 0f);
        lastHealth = health;
        lastAddAmount = amount;

        startMaskPosX = endMaskPosX;
        endMaskPosX = startMaskPos.x + maskPosXTotalDelta * health;
        maskPosDelta = endMaskPosX - startMaskPosX;
        dingTimer = 0;

        state = isFilling ? HealthBarState.filling : HealthBarState.emptying;
        fillTimer = 0;

        if (isFilling)
        {
            //particles.Play();
            //snd.Play();
        }
    }

    void Update()
    {
        if (contractTimer > 0)
        {
            contractTimer -= Time.deltaTime;
            if (contractTimer <= 0)
            {
                Contract();
            }
        }

        if (state != HealthBarState.idle)
        {
            fillTimer += Time.deltaTime;
            progress = fillTimer / fillDuration;

            if (progress >= 1.0f)
            {
                progress = 1.0f;
                fillTimer = 0;
                state = HealthBarState.idle;

                if (health == 1)
                {
                    healthFullHandler?.Invoke(ID);
                }
            }
            else if (state == HealthBarState.filling)
            {
                dingTimer += Time.deltaTime;
                if (dingTimer >= dingInterval)
                {
                    snd.Play();
                    dingTimer = 0;
                }
            }

            float newX = EasingFunction.EaseInOutSine(startMaskPosX, startMaskPosX + maskPosDelta, progress);
            Mask.transform.position = new(newX, startMaskPos.y, startMaskPos.z);
        }
    }
}
