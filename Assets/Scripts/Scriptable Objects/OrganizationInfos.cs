using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "Organizations", menuName = "ScriptableObjects/Organizations")]
public class OrganizationInfos : ScriptableObject {
    public OrganizationInfo[] organizations;
    private Dictionary<string, OrganizationInfo> organizationsByNames;

    public void Awake() {
        Initialize();
    }

    public void Initialize() {
        organizationsByNames = new Dictionary<string, OrganizationInfo>();
        for (int i = 0; i < organizations.Length; i++) {
            organizationsByNames.Add(organizations[i].name.Trim().ToLower(), organizations[i]);
        }

    }

    public OrganizationInfo GetOrgByName(string name) {
        Debug.Log("name:" + name);
        if (organizationsByNames == null){ 
            Initialize(); 
        }
            name = name.Trim().ToLower();
        if (organizationsByNames[name] == null) {
            throw new System.ArgumentException("Organization with name " + name + " cannot be found");
        }
        return organizationsByNames[name];
    }
}


[System.Serializable]
public class OrganizationInfo {
    public string name;
    public Sprite logo;
}
