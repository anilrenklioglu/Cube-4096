using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class SliderController : MonoBehaviour, IPointerDownHandler,IPointerUpHandler
{
   public UnityAction OnPointerDownEvent;
   public UnityAction <float> OnPointerDragEvent;
   public UnityAction OnPointerUpEvent;

   private Slider slider;

   private void Awake()
   {
      slider = GetComponent<Slider>();
      slider.onValueChanged.AddListener(OnSliderValueChanged);
   }

   public void OnPointerDown(PointerEventData eventData)
   {
      if (OnPointerDownEvent != null)
      {
         OnPointerDownEvent.Invoke();
      }

      if (OnPointerDragEvent != null)
      {
         OnPointerDragEvent.Invoke(slider.value);
      }
   }
   
   public void OnSliderValueChanged(float value)
   {
      if (OnPointerDragEvent != null)
      {
         OnPointerDragEvent.Invoke(value);
      }
   }

   public void OnPointerUp(PointerEventData eventData)
   {
      if (OnPointerUpEvent != null)
      {
         OnPointerUpEvent.Invoke();
      }
      //reset slider value 
      slider.value = 0f;
   }

   private void OnDestroy()
   {
      //remove listeners to avoid memory leaks
      slider.onValueChanged.RemoveListener(OnSliderValueChanged);
   }
}
