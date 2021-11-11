using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeCollision : MonoBehaviour
{
   private CubeController cube;

   private void Awake()
   {
      cube = GetComponent<CubeController>();
   }

   private void OnCollisionEnter(Collision other)
   {
      CubeController otherCube = other.gameObject.GetComponent<CubeController>();
      
      //check if contacted with other cube

      if (otherCube != null && cube.CubeID > otherCube.CubeID)
      {
         //check if both cubes has same number
         if (cube.cubeNumber == otherCube.cubeNumber)
         {
            Vector3 contactPoint = other.contacts[0].point;
            
            //check if cubes number is less than max number in CubeSpawnerContoller:

            if (otherCube.cubeNumber < CubeSpawnerController.instance.maxCubeNumber)
            {
               //spawn a new cube as a result

               CubeController newCube =
                  CubeSpawnerController.instance.Spawn
                     (cube.cubeNumber * 2, contactPoint + Vector3.up * 1.6f);
               
               //push the new cube up and forward

               float pushForce = 2.5f;
               newCube.cubeRb.AddForce(new Vector3(0, .3f, 1f) * pushForce, ForceMode.Impulse);
               
               //add same torque

               float randomValue = Random.Range(-20f, 20f);
               Vector3 randomDirection = Vector3.one * randomValue;
               newCube.cubeRb.AddTorque(randomDirection);
            }
            
            //the cube explosion should affect surrounded cubes too:

            Collider[] surroundedCubes = Physics.OverlapSphere(contactPoint, 2f);
            float explosionForce = 400f;
            float explosionRadius = 1.5f;

            foreach (Collider collider in surroundedCubes)
            {
               if (collider.attachedRigidbody != null)
               {
                  collider.attachedRigidbody.AddExplosionForce(explosionForce, contactPoint, explosionRadius);
               }
            }
             
            FX.Instance.PlayFX(contactPoint, cube.cubeColor);
               
            //Destroy the two cubes
            
            CubeSpawnerController.instance.DestroyCube(cube);
            CubeSpawnerController.instance.DestroyCube(otherCube);

         }
      }
   }
}
