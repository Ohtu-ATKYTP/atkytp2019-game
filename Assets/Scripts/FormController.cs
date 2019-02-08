using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormController : MonoBehaviour {
    public Text statusMessage;
    private WebServiceScript webScript;

    void Start() {
        webScript = FindObjectOfType<WebServiceScript>();
    }

    public void SendFormData() {
        if(PlayerPrefs.GetInt("registered") == 1){
            DisplayMessage("You cannot register twice");   
            return;
         }

        string userName = GameObject.Find("Username Input").transform.Find("Text").GetComponent<Text>().text;
        string token = GameObject.Find("Token Input").transform.Find("Text").GetComponent<Text>().text;
        webScript.RegisterUser(userName, token, (connectionSuccess, serverSuccess, errorData) => {

            if(!connectionSuccess){ 
                    DisplayMessage("Could not reach server, try again later");
                    return;
            }
   
            if(!serverSuccess){
                DisplayMessage("Validation failed\n" + errorData);
                return;
            }

            HandleSuccess(); }
            );
    }

    private void HandleSuccess(){
        PlayerPrefs.SetInt("registered", 1);
        FindObjectOfType<MenuManager>().displayOnlyMenu("Main Menu Screen");
    }


    private void HandleFailure(string username, string token){ 
        statusMessage.text = "Registration has failed.\nTry again later, or try another username";
    }

    private void DisplayMessage(string message){ 
            statusMessage.text = message;
    }
}
