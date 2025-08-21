using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController: MonoBehaviour
{
    private CharacterController characterController;
    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    public void OnLevelBegin()
    {
        transform.position = Vector3.zero;
    }

    public void OnLose()
    {
        Destroy(this.gameObject);
    }
    
    void Update()
    {
        characterController.Move(Vector3.right * Time.deltaTime);
    }
}
