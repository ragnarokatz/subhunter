using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class Notification : MonoBehaviour
{
    private const float DURATION = 3f;

    private static Notification instance;
    public static Notification I { get { return Notification.instance; } }

    public GameObject Template;
    public GameObject Grid;

    private Dictionary<GameObject, float> messages;

    public void DisplayMessage(string format, params object[] objs)
    {
        var message = String.Format(format, objs);

        var go = GameObject.Instantiate(this.Template) as GameObject;
        go.transform.SetParent(this.Grid.transform);
        go.transform.localScale = Vector3.one;
        go.GetComponent<Text>().text = message;
        go.SetActive(true);

        this.messages.Add(go, Time.time);
    }

    private void Start()
    {
        Notification.instance = this;

        this.messages = new Dictionary<GameObject, float>(4);
    }

    private void Update()
    {
        if (this.messages.Count <= 0)
            return;

        var keys = this.messages.Keys;
        foreach (var go in keys)
        {
            var startTime = this.messages[go];
            if (Time.time - startTime < Notification.DURATION)
                continue;

            Destroy(go);
            this.messages.Remove(go);
            return;
        }
    }
}