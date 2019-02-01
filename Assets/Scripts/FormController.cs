using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class FormController : MonoBehaviour
{
    [SerializeField] private WebServiceScript webScript;  

    // Placeholder while the user data is not saved. 
    // Currently we have to pick the values from the text fields

    public void SendFormData(){
        string userName = GameObject.Find("Username Input").transform.Find("Text").GetComponent<Text>().text;
        string token = GameObject.Find("Token Input").transform.Find("Text").GetComponent<Text>().text;
        webScript.RegisterUser(userName, token); 
        Debug.Log("user: " + userName + ", token: " + token);
        }


}
