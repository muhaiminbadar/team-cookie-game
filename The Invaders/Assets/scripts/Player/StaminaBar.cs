using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public delegate void RunPlayerEvent(float cost);

public class StaminaBar : MonoBehaviour
{
    public Slider slider;
    private float health;
    private float lastRan;
    
    void RunCheck(float cost)
    {
        lastRan = Time.time;
        if (health >= 0f)
        {
            SetHealth(health - cost);
        }
        else
        {
            characterMovement.runLocked = true;
        }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    void OnEnable()
    {
        Events<RunPlayerEvent>.Instance.Register(RunCheck);
    }
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    void OnDisable()
    {
        Events<RunPlayerEvent>.Instance.Unregister(RunCheck);
    }

    void Awake()
    {
        lastRan = 0f;
        SetMaxHealth(100f);
        SetHealth(100f);
    }

    public void SetMaxHealth(float value)
    {
        slider.maxValue = value;
    }

    public void SetHealth(float value)
    {
        health = value;
        slider.value = health;
    }

    void FixedUpdate()
    {
        if ((Time.time - lastRan) >= 2f && health < 100f)
        {
            characterMovement.runLocked = false;
            SetHealth(health + 0.5f);
        }
    }

}



