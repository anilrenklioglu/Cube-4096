using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CubeController : MonoBehaviour
{
    static int staticID = 0;
    [SerializeField] private TMP_Text[] numberText;

    [HideInInspector] public int CubeID;
    [HideInInspector] public Color cubeColor;
    [HideInInspector] public int cubeNumber;
    [HideInInspector] public Rigidbody cubeRb;
    [HideInInspector] public bool isMainCube;

    private MeshRenderer cubeMeshRenderer;

    private void Awake()
    {
        CubeID = staticID++;
        cubeMeshRenderer = GetComponent<MeshRenderer>();
        cubeRb = GetComponent<Rigidbody>();
    }

    public void SetCubeColor(Color color)
    {
        cubeColor = color;
        cubeMeshRenderer.material.color = color;
    }

    public void SetCubeNumber(int number)
    {
        cubeNumber = number;

        for (int i = 0; i < 6; i++)
        {
            numberText[i].text = number.ToString();
        }
    }

}
