using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mgl;

public class I18nManager : MonoBehaviour
{

    private I18n i18n = I18n.Instance;

    public Text TapToPlay;
    public Text HowToPlay;
    public Text BuyFor10Cheese;
    public Text CheeseVideoLabel;
    public Text UseThis;

    void Start()
    {
        InitLanguage();
    }

    // Update is called once per frame
    void InitLanguage()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.Russian: SetLanguage("ru-RU"); break;

            default: SetLanguage("en-US"); break;
        }
    }

    private void SetLanguage(string locale)
    {
        Debug.Log("I18nManager.SetLanguage: " + locale);
        I18n.SetLocale(locale);
        DoTranslations();
    }

    private void DoTranslations()
    {
        TapToPlay.text = i18n.__("Tap to Play");
        HowToPlay.text = i18n.__("Touch,\nHold,\nThen Release");
        BuyFor10Cheese.text = i18n.__("Buy for\n10 cheese");
        CheeseVideoLabel.text = i18n.__("+10 cheese");
        UseThis.text = i18n.__("Use this");
    }

    public static string __(string key)
    {
        return I18n.Instance.__(key);
    }

}
