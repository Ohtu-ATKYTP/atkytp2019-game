using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidState : MonoBehaviour
{
    private JalluLogic logic; 
    void Start()
    {
        logic = FindObjectOfType<JalluLogic>();
        logic.AddLiquid();
    }

    public IEnumerator CORStartDestruction()
    {
        float time = 5f;
        while(time > 0)
        {
            time -= Time.deltaTime;
            yield return null;
        }
        Destroy(this.gameObject);
    }
}
