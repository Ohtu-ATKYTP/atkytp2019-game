using UnityEngine;
using UnityEngine.UI;

public class OrganizationDisplayer : MonoBehaviour {
    public OrganizationInfos orgInfo;
    private Image logo;

    void Awake() {
        logo = GetComponent<Image>();
    }

    public void Initialize(string name) {
        OrganizationInfo info = orgInfo.GetOrgByName(name);
        logo.sprite = info.logo;
    }
}
