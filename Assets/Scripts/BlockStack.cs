using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockStack : MonoBehaviour
{
    private List<BlockRow> rows = new ();
    private bool isTested = false;
    [SerializeField]
    private Transform[] prefabs;

    public void Awake()
    {
        AddRow();
    }

    private class BlockRow
    {
        private readonly int myLevel;
        private int currBlocks;
        public List<Block> blocks = new ();

        private const float OffsetHorizontal = 0.35f;
        private const float OffsetVertical = 0.2f;

        public BlockRow(int level)
        {
            myLevel = level;
            currBlocks = 0;
        }
        
        public bool IsFull()
        {
            return currBlocks == 3;
        }

        public void AddBlock(Transform masteryPrefab, Transform myStack, DataItemAPI.EntryAPI info)
        {
            var isRotated = myLevel % 2 == 0;
            var posX = isRotated ? -OffsetHorizontal : 0f;
            var posZ = isRotated ? 0f : -OffsetHorizontal;
            var posY = OffsetVertical * myLevel;

            if (isRotated)
            {
                posX += currBlocks * OffsetHorizontal;
            }
            else
            {
                posZ += currBlocks * OffsetHorizontal;
            }

            Vector3 spawnPosition = new Vector3(posX, posY, posZ);
            Quaternion spawnRotation = isRotated ? Quaternion.Euler(0,90,0) : Quaternion.identity;
            
            var blockObject = Instantiate(masteryPrefab,myStack);
            blockObject.localPosition = spawnPosition;
            blockObject.localRotation = spawnRotation;

            Block blockComponent = blockObject.GetComponent<Block>();
            blockComponent.SetInfo(info);
            
            blocks.Add(blockComponent);
            currBlocks++;
        }
    }

    private void AddRow()
    {
        rows.Add(new BlockRow(rows.Count));
    }
    public void AddBlockInStack(DataItemAPI.EntryAPI info)
    {
        var lastRow = rows.Last();
        if (lastRow.IsFull())
        {
            AddRow();
            lastRow = rows.Last();
        }
        
        lastRow.AddBlock(prefabs[info.mastery], transform, info);
    }

    public void TestMyStack()
    {
        if(isTested)
            return;
        
        foreach (var row in rows)
        {
            foreach (var block in row.blocks)
            {
                if (block.IsGlass())
                {
                    block.gameObject.SetActive(false);
                }
                else
                {
                    block.EnablePhysics();
                }
            }
        }

        isTested = true;
    }
}
