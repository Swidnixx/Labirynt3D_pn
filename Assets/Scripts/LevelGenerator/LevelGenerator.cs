using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Texture2D map;
    public ColorMapping[] mappings;
    public float offset = 5;
    public float yPosition;


    public void GenerateMap()
    {
        for(int x = 0; x < map.width; x++)
        {
            for(int z = 0; z < map.height; z++)
            {
                CreatePixel(x, z);
            }
        }
    }

    private void CreatePixel(int x, int z)
    {
        Color color = map.GetPixel(x, z);

        foreach(ColorMapping mapping in mappings)
        {
            if(mapping.color.Equals(color))
            {
                Vector3 position = new Vector3(x * offset, yPosition, z * offset);
                Instantiate(mapping.prefab, position, Quaternion.identity, transform);
            }
        }
    }

    public void DestroyMap()
    {
        while(transform.childCount != 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
        //Transform[] elements = transform.GetComponentsInChildren<Transform>();
        //for(int i = 1; i < elements.Length; i++)
        //{
        //    GameObject.DestroyImmediate(elements[i].gameObject);
        //}
    }
}
