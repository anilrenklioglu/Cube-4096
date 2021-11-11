using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RedZone : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        CubeController cube = other.GetComponent<CubeController>();

        if (cube != null)
        {
            if (!cube.isMainCube && cube.cubeRb.velocity.magnitude < .1f)
            {
                Debug.Log("Game Over");
                Restartevel();
            }
        }
    }
    
    public void Restartevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
