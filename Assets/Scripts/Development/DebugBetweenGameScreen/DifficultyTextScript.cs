using UnityEngine;
using UnityEngine.UI;

public class DifficultyTextScript : MonoBehaviour
{
	private int difficulty;
	private InputField inputField;
	public Button plusButton;
	public Button minusButton;
    void Start() {
		inputField = GetComponent<InputField>();
		difficulty = int.Parse(inputField.text);
		inputField.onValidateInput += delegate(string input, int charIndex, char addedChar) { 
			return NoNegativeNumbers(addedChar); 
		};

		plusButton.onClick.AddListener(delegate {ScrollDifficulty(1);});
		minusButton.onClick.AddListener(delegate {ScrollDifficulty(-1);});
    }

	private void ScrollDifficulty(int diff) {
		this.difficulty += diff;
		this.difficulty = this.difficulty > 0 ? this.difficulty : 1;
		inputField.text = "" + this.difficulty;
	}

	private char NoNegativeNumbers(char charToValidate){
        if (charToValidate == '-') {
            charToValidate = '\0';
        }
        return charToValidate;
    }

	public void OnInputChange() {
		this.difficulty = int.Parse(inputField.text);
	} 
}
