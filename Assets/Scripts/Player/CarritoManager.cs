using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarritoManager : MonoBehaviour
{
    public GameObject[] spaces;

    private void Awake()
    {
        spaces = new GameObject[transform.childCount];
        for (int i=0; i < transform.childCount; i++)
        {
            spaces[i] = transform.GetChild(i).gameObject;
        }
    }

    public GameObject GetEmptySpace()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (spaces[i].transform.childCount == 0)
            {
                return spaces[i].gameObject;
            }
        }
        return null;
    }
}
