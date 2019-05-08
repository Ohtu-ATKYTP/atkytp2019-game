//Moves elevator shaft
using UnityEngine;

public class ElevatorShaftMove : MonoBehaviour
{
    public bool move;
    private float speed;

    void Start() {
        move = true;
        speed = 1f;
    }
    
    void Update() {
        if(move) {
            Vector2 position = transform.position;
            position.y -= speed;
            transform.position = position;
        }
    }
}
