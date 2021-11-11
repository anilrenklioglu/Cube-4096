using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float pushForce;
    [SerializeField] private float maxCubePosX;

    [Space] 
    
    [SerializeField] private SliderController sliderController;

     private CubeController cubeController;

    private bool isPointerDown;
    private bool canMove;
    
    private Vector3 cubePosition;
   
    void Start()
    {
        //spawn new cube
        SpawnCube();
        canMove = true;
        
        // listen to slide events:

        sliderController.OnPointerDownEvent += OnPointerDown;
        sliderController.OnPointerDragEvent += OnPointerDrag;
        sliderController.OnPointerUpEvent += OnPointerUp;
    }

    private void Update()
    {
        if (isPointerDown)
        {
            cubeController.transform.position = Vector3.Lerp
            (cubeController.transform.position, 
                cubePosition,
                moveSpeed * Time.deltaTime);
        }
    }

    private void OnPointerDown()
    {
        isPointerDown = true;
    }
    
    private void OnPointerDrag(float xMovement)
    {
        if (isPointerDown)
        {
            cubePosition = cubeController.transform.position;
            cubePosition.x = xMovement * maxCubePosX;
        }
    }
    
    private void OnPointerUp()
    {
        if (isPointerDown && canMove)
        {
            isPointerDown = false;
            canMove = false;
            
            //PUSH THE CUBE
            
            cubeController.cubeRb.AddForce(Vector3.forward * pushForce, ForceMode.Impulse);
            
            //SPAWN A NEW CUBE AFTER 0.3 SECONDS
            Invoke("SpawnNewCube", 0.3f);
            
        }
    }
    private void SpawnNewCube()
    {
        cubeController.isMainCube = false;
        canMove = true;
        SpawnCube();
    }

    private void SpawnCube()
    {
        cubeController = CubeSpawnerController.instance.SpawnRandom();
        cubeController.isMainCube = true;
        
        //reset cube position
        cubePosition = cubeController.transform.position;
    }

    private void OnDestroy()
    {
        //remove listeners
        
        sliderController.OnPointerDownEvent -= OnPointerDown;
        sliderController.OnPointerDragEvent -= OnPointerDrag;
        sliderController.OnPointerUpEvent -= OnPointerUp;
    }
}
