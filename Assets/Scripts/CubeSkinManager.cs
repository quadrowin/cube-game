using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSkinManager : MonoBehaviour {

    const string PREFS_OPENED_SKINS = "openedSkins";
    const string PREFS_SELECTED_SKIN = "selectedSkin";
    public static Quaternion zeroCubeRotation = Quaternion.Euler(-90, 0, 0);

    private Dictionary<string, bool> openedSkins = new Dictionary<string, bool>();
    private string selectedSkin;
    private Dictionary<string, CubeSkinOption> knownTemplates = new Dictionary<string, CubeSkinOption>();

    public CubeSkinOption DefaultSkinTpl;
    public GameObject[] SkinUsers;
	
	void Start () {
        selectedSkin = PlayerPrefs.GetString(PREFS_SELECTED_SKIN);

        var saved = PlayerPrefs.GetString(PREFS_OPENED_SKINS);
        print("OpenedSkins: " + saved);
        foreach (var skin in saved.Split(','))
        {
            openedSkins.Add(skin, true);
        }

        foreach (var tpl in Resources.FindObjectsOfTypeAll<CubeSkinOption>())
        {
            knownTemplates.Add(tpl.SkinName, tpl);
        }

        UpdateUsersSkin();
    }
	
	public bool IsSkinOpened(string name)
    {
        return "" == name || openedSkins.ContainsKey(name);
    }

    public string GetSelectedSkinName()
    {
        return selectedSkin;
    }

    public CubeSkinOption GetSelectedSkinTpl()
    {
        if (selectedSkin != "" && knownTemplates.ContainsKey(selectedSkin))
        {
            CubeSkinOption tpl;
            if (knownTemplates.TryGetValue(selectedSkin, out tpl))
            {
                return tpl;
            }
        }
        return DefaultSkinTpl;
    }

    public void OpenSkin(CubeSkinOption skin)
    {
        openedSkins.Add(skin.SkinName, true);

        string openedSkinsNames = "";
        foreach (string skinName in openedSkins.Keys)
        {
            if (openedSkinsNames.Length > 0)
            {
                openedSkinsNames += "," + skinName;
            }
            else
            {
                openedSkinsNames = skinName;
            }
        }
        PlayerPrefs.SetString(PREFS_OPENED_SKINS, openedSkinsNames);

        skin.SetOpened(true);
    }

    public void SetSelectedSkin(string name)
    {
        selectedSkin = name;
        UpdateUsersSkin();
        PlayerPrefs.SetString(PREFS_SELECTED_SKIN, name);
    }

    public void UpdateUsersSkin()
    {
        var skinParts = new List<GameObject>();
        foreach (Transform child in GetSelectedSkinTpl().transform)
        {
            if (child.tag == Tags.PADLOCK_CHEESE)
            {
                continue;
            }
            skinParts.Add(child.gameObject);
        }

        foreach (var user in SkinUsers)
        {
            user.GetComponent<MeshRenderer>().material.color = Color.clear;
            // clear children
            var children = new List<GameObject>();
            foreach (Transform child in user.transform)
            {
                children.Add(child.gameObject);
            }
            children.ForEach(child => Destroy(child));
            // add new children of active skin
            skinParts.ForEach(part => {
                var inst = Instantiate(part, user.transform);
                inst.transform.localPosition = Vector3.zero;
                inst.transform.localRotation = zeroCubeRotation;
                inst.transform.localScale = Vector3.one;
            });
        }
    }

}
