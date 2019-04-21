using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleExiter : MonoBehaviour
{
    private JalluLogic logic;
    public void Start()
    {
        logic = FindObjectOfType<JalluLogic>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Here come dat boy");
        logic.RemoveLiquid();
        StartCoroutine(collision.GetComponent<LiquidState>().CORStartDestruction());
        Destroy(collision);
    }



}
