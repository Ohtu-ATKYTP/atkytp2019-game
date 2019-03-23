using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderParameterUpdate : MonoBehaviour {
    
    private Slider slider;
    public Text valueText;
    public string parameterName;
    private DataController dataController;
    
    void Start()    {
        this.slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(delegate {ValueChangeCheck(); });
        dataController = FindObjectOfType<DataController>();
        this.initSliderValue();
    }

    public void ValueChangeCheck() {
        dataController.setGameParameter(parameterName, slider.value);
        valueText.text = dataController.getGameParameter(parameterName).ToString();
    }

    public void initSliderValue() {
        if(dataController.hasGameParameter(parameterName)){
            slider.value = dataController.getGameParameter(parameterName);
        }else{
            dataController.setGameParameter(parameterName, slider.value);
        }
    }


}
