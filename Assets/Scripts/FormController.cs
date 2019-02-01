using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class FormController : MonoBehaviour
{
    [SerializeField] private WebServiceScript webScript;  

    public void SendFormData(){
        string userName = GameObject.Find("Username Input").transform.Find("Text").GetComponent<Text>().text;
        string token = GameObject.Find("Token Input").transform.Find("Text").GetComponent<Text>().text;
        webScript.RegisterUser(userName, token); 
        }


}
