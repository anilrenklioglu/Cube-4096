using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FX : MonoBehaviour
{
  [SerializeField] private ParticleSystem cubeExplosionFX;

  private ParticleSystem.MainModule cubeExplosionFXMainModule;
  //singleton class

  public static FX Instance;

  private void Awake()
  {
    Instance = this;
  }

  private void Start()
  {
    cubeExplosionFXMainModule = cubeExplosionFX.main;
  }

  public void PlayFX(Vector3 position, Color color)
  {
    cubeExplosionFXMainModule.startColor = new ParticleSystem.MinMaxGradient(color);
    cubeExplosionFX.transform.position = position;
    cubeExplosionFX.Play();
  }
}
