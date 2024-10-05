using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DinoUI : MonoBehaviour
{
    private UIDocument dinoUI;
    private VisualElement dinoVisualElement;

    private bool visible = true;

    private float timer = 10;

    private void Awake()
    {
        dinoUI = GetComponent<UIDocument>();
        dinoVisualElement = dinoUI.rootVisualElement.Q<VisualElement>("UIPanel");
    }

    private void Update()
    {
        if (timer <= 0 && visible)
        {
            dinoVisualElement.style.display = DisplayStyle.None;
            visible = false;
        }
        else
        {
            timer -= 10f * Time.deltaTime;
            Debug.Log(timer);
        }
    }
}
