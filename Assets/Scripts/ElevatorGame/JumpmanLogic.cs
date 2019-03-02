using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class JumpmanLogic : MonoBehaviour {
    
    private ElevatorGameLogic EGLogic;
    public Sprite Face;
    void Start() {
        this.EGLogic = FindObjectOfType<ElevatorGameLogic>();
    }
    void Update() {
    }
    void OnCollisionEnter2D(Collision2D collision2D){
        EGLogic.AddDamage();
    }

    public void Jump() {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up*3000, ForceMode2D.Impulse);
    }
    public void ChangeFace(){
        GetComponent<Image>().sprite = Face;
        GetComponent<Image>().color = Color.white;
    }


}