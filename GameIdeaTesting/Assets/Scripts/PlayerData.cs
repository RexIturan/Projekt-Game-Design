using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "Player Data", order = 51)]
public class PlayerData : ScriptableObject
{
    [SerializeField]
    private float MovementSpeed = 0.5f;
    [SerializeField]
    private float RotationSpeed = 3.0f;
    [SerializeField]
    private float JumpSpeed = 5.0f;
    [SerializeField]
    private float Gravity = 2.0f;
    [SerializeField] private string NameID = "Default";


    public float getMovementSpeed()
    {
        return MovementSpeed;
    }
    
    public float getRotationSpeed()
    {
        return RotationSpeed;
    }
    
    public float getJumpSpeed()
    {
        return JumpSpeed;
    }
    
    public float getGravity()
    {
        return Gravity;
    }

    public string getNameID()
    {
        return NameID;
    }
}

