using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // private string dataFile;
    private DataItemAPI dataFile;
    
    [SerializeField]
    private BlockStack[] stacks;

    private BlockStack selectedStack;
    private int selectedStackIndex;

    [SerializeField]
    private Camera mainCamera;
    private Vector3 mainCameraStartPos;

    [SerializeField] private Image infoImage;
    [SerializeField] private Text infoText;
    
    // Start is called before the first frame update
    void Start()
    {
        if (File.Exists("Assets/stack"))
        {
            var dataFileString = File.ReadAllText("Assets/stack");
            dataFile = JsonUtility.FromJson<DataItemAPI>("{\"entries\":" + dataFileString + "}");
            Debug.Log(dataFile.entries.Count);
        }
        
        if (stacks is null)
        {
            throw new NullReferenceException("stacks");
        }

        if (mainCamera is null)
        {
            throw new NullReferenceException("Main camera missing");
        }
        
        mainCameraStartPos = mainCamera.transform.position;
        selectedStackIndex = 0;
        FocusCamera();
        

        OrderBlocks();
        BuildStacks();
        
        HideInfo();
    }

    private void FocusCamera()
    {
        mainCamera.transform.position = mainCameraStartPos;
        selectedStack = stacks[selectedStackIndex];
        mainCamera.transform.LookAt(selectedStack.transform);
    }

    private void OrderBlocks()
    {
        dataFile.entries.Sort();
    }

    private void BuildStacks()
    {
        foreach (var entry in dataFile.entries)
        {
            switch (entry.grade)
            {
                case "6th Grade":
                    stacks[0].AddBlockInStack(entry);
                    break;
                case "7th Grade":
                    stacks[1].AddBlockInStack(entry);
                    break;
                case "8th Grade":
                    stacks[2].AddBlockInStack(entry);
                    break;
            }
        }
    }

    public void TestMyStack()
    {
        selectedStack.TestMyStack();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && selectedStackIndex > 0)
        {
            selectedStackIndex -= 1;
            FocusCamera();
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow) && selectedStackIndex < 2)
        {
            selectedStackIndex += 1;
            FocusCamera();
        }


        if (Input.GetMouseButton(0)){
            
            mainCamera.transform.RotateAround(selectedStack.transform.position, Vector3.up, 1f);
        }
    }

    public void DisplayInfo(string infoPresentation)
    {
        infoText.text = infoPresentation;
        infoImage.gameObject.SetActive(true);
    }

    public void HideInfo()
    {
        infoImage.gameObject.SetActive(false);
    }
}
