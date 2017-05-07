using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSkinManager : MonoBehaviour {

    const string PREFS_OPENED_SKINS = "openedSkins";
    const string PREFS_SELECTED_SKIN = "selectedSkin";

    private Dictionary<string, bool> openedSkins = new Dictionary<string, bool>();
    private string selectedSkin;
    private Dictionary<string, CubeSkinSelect> knownTemplates = new Dictionary<string, CubeSkinSelect>();

    public CubeSkinSelect DefaultSkinTpl;
    public GameObject[] SkinUsers;
	
	void Start () {
        selectedSkin = PlayerPrefs.GetString(PREFS_SELECTED_SKIN);

        var saved = PlayerPrefs.GetString(PREFS_OPENED_SKINS);
        foreach (var skin in saved.Split(','))
        {
            openedSkins.Add(skin, true);
        }

        foreach (var tpl in Resources.FindObjectsOfTypeAll<CubeSkinSelect>())
        {
            knownTemplates.Add(tpl.SkinName, tpl);
        }

        UpdateUsersSkin();
    }
	
	public bool IsSkinOpened(string name)
    {
        return openedSkins.ContainsKey(name);
    }

    public string GetSelectedSkinName()
    {
        return selectedSkin;
    }

    public CubeSkinSelect GetSelectedSkinTpl()
    {
        if (selectedSkin != "" && knownTemplates.ContainsKey(selectedSkin))
        {
            CubeSkinSelect tpl;
            if (knownTemplates.TryGetValue(selectedSkin, out tpl))
            {
                return tpl;
            }
        }
        return DefaultSkinTpl;
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
            skinParts.Add(child.gameObject);
        }

        foreach (var user in SkinUsers)
        {
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
                inst.transform.localRotation = Quaternion.identity;
                inst.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            });
        }
    }

}
