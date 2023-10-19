using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private DataItemAPI.EntryAPI info;
    [SerializeField]
    private Rigidbody rigidbody;
    
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void SetInfo(DataItemAPI.EntryAPI entryAPI)
    {
        info = entryAPI;
    }

    public bool IsGlass()
    {
        return info.mastery == 0;
    }

    public void EnablePhysics()
    {
        rigidbody = gameObject.AddComponent<Rigidbody>();
    }

    private string InfoPresentation()
    {
        return $"{info.grade}: {info.domain}\n\n" +
               $"{info.cluster}\n\n" +
               $"{info.standardid}: {info.standarddescription}";
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            // Debug.Log(InfoPresentation());
            gameManager.DisplayInfo(InfoPresentation());
        }
    }

    private void OnMouseExit()
    {
        gameManager.HideInfo();
    }
}
