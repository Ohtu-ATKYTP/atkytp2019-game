using UnityEngine;
using UnityEngine.UI;

public class SliderParameterUpdate : MonoBehaviour {
    
    private Slider slider;
    public Text valueText;
    public string parameterName;
    
    void Start()    {
        this.slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(delegate {ValueChangeCheck(); });
        this.InitSliderValue();
    }

    public void ValueChangeCheck() {
        DataController.SetGameParameter(parameterName, slider.value);
        valueText.text = DataController.GetGameParameter(parameterName).ToString();
    }

    public void InitSliderValue() {
        if(DataController.HasGameParameter(parameterName)){
            slider.value = DataController.GetGameParameter(parameterName);
        }else{
            DataController.SetGameParameter(parameterName, slider.value);
        }
    }


}
