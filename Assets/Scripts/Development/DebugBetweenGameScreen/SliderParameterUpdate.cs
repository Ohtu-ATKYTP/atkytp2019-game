using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderParameterUpdate : MonoBehaviour {
    
    private Slider slider;
    public Text valueText;
    public string parameterName;
    
    void Start()    {
        this.slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(delegate {ValueChangeCheck(); });
        this.initSliderValue();
    }

    public void ValueChangeCheck() {
        DataController.setGameParameter(parameterName, slider.value);
        valueText.text = DataController.getGameParameter(parameterName).ToString();
    }

    public void initSliderValue() {
        if(DataController.hasGameParameter(parameterName)){
            slider.value = DataController.getGameParameter(parameterName);
        }else{
            DataController.setGameParameter(parameterName, slider.value);
        }
    }


}
