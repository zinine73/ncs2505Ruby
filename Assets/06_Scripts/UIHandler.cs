using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{
    public static UIHandler instance { get; private set; }
    public float displayTime = 4.0f;
    VisualElement m_NPCDialogue;
    VisualElement m_Healthbar;
    float m_TimerDisplay;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        var uiDocument = GetComponent<UIDocument>();
        m_Healthbar = uiDocument.rootVisualElement
            .Q<VisualElement>("HealthBar");
        SetHealthValue(1.0f);
        m_NPCDialogue = uiDocument.rootVisualElement
            .Q<VisualElement>("NPCDialogue");
        m_NPCDialogue.style.display = DisplayStyle.None;
        m_TimerDisplay = -1.0f;
    }

    public void SetHealthValue(float percentage)
    {
        m_Healthbar.style.width
            = Length.Percent(100 * percentage);
    }

    void Update()
    {
        if (m_TimerDisplay > 0)
        {
            m_TimerDisplay -= Time.deltaTime;
            if (m_TimerDisplay < 0)
            {
                m_NPCDialogue.style.display = DisplayStyle.None;
            }
        }
    }

    public void DisplayDialogue()
    {
        m_NPCDialogue
            .Q<VisualElement>("Background")
            .Q<Label>("Label").text = "test";
        m_NPCDialogue.style.display = DisplayStyle.Flex;
        m_TimerDisplay = displayTime;
    }
}
