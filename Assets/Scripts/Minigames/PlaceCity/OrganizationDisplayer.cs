using UnityEngine;
using UnityEngine.UI;

public class OrganizationDisplayer : MonoBehaviour {
    public OrganizationInfos orgInfo;
    Image logo;
    Text orgName;

    void Awake() {
        logo = GetComponent<Image>();
        orgName = GetComponentInChildren<Text>();
    }

    public void Initialize(string name) {
        OrganizationInfo info = orgInfo.GetOrgByName(name);
        orgName.text = info.name;
        logo.sprite = info.logo;
    }
}
