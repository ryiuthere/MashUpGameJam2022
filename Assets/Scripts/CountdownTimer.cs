using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    public static CountdownTimer Instance { get; private set; }

    [SerializeField]
    private float TotalSeconds = 180;

    public float CurrentTime { get; private set; }

    [SerializeField]
    protected TextMeshProUGUI textMesh;

    protected bool activated = false;

    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        CurrentTime = TotalSeconds;
        activated = true;
    }

    void Update()
    {
        if (!activated)
            return;

        CurrentTime -=  Time.deltaTime;
        if (CurrentTime < 0)
        {
            CurrentTime = 0;
            var player = GameObject.Find("Player").GetComponent<Player>();
            player.Kill();
            activated = false;
        }
        var time = TimeSpan.FromSeconds(CurrentTime);
        textMesh.text = time.ToString("mm':'ss'.'fff");
    }

    public void AddTime(float seconds)
    {
        CurrentTime += seconds;
    }
}
