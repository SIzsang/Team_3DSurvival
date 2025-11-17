using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerCondition condition;

    private void Awake()
    {
        condition = GetComponent<PlayerCondition>();
    }
}