using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextMapDropdownScript : MonoBehaviour {
	private Dropdown.OptionData newData;
	private DebugBetweenScreenController controller;
	private Dropdown dropdown;
	private List<Dropdown.OptionData> mapOptions;
    // Start is called before the first frame update
    void Start() {
		controller = FindObjectOfType<DebugBetweenScreenController>();
        dropdown = GetComponent<Dropdown>();
		dropdown.ClearOptions();
		controller.SetNextGame("Random");

		dropdown.onValueChanged.AddListener(delegate {
            ChangeMap(dropdown);
        });

		//We create a list of dropdown options that includes all maps and "Random"
		//"Random" is set as the caption (default option) of the dropdown menu
		mapOptions = new List<Dropdown.OptionData>();
		newData = new Dropdown.OptionData();
		newData.text = "Random";
		mapOptions.Add(newData);
		dropdown.captionText.text = newData.text;

		//Add the dropdown options to the actual dropdown game object
		foreach (string s in GameManager.getGames()) {
			newData = new Dropdown.OptionData();
			newData.text = s;
			mapOptions.Add(newData);
		}

		foreach(Dropdown.OptionData d in mapOptions) {
			dropdown.options.Add(d);
		}
    }

	public void ChangeMap(Dropdown change) {
		controller.SetNextGame(mapOptions[change.value].text);
	}
}
