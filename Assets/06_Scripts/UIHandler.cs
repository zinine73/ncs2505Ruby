using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{
    //public float CurrentHealth = 0.5f;
    VisualElement m_Healthbar;
    void Start()
    {
        var uiDocument = GetComponent<UIDocument>();
        m_Healthbar = uiDocument.rootVisualElement.
            Q<VisualElement>("HealthBar");
        //healthBar.style.width
        //    = Length.Percent(CurrentHealth * 100.0f);
        SetHealthValue(1.0f);
    }

    public void SetHealthValue(float percentage)
    {
        m_Healthbar.style.width
            = Length.Percent(100 * percentage);
    }
}
