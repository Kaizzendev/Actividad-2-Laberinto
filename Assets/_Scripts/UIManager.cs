using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
   
   [Header("Cameras")]
   [SerializeField] private Camera mainCamera;
   [SerializeField] private Camera cenitalCamera;

   
   public Button playButton;

  

   public void Play()
   {
      GameManager.Instance.Play();
      playButton.gameObject.SetActive(false);
   }

   public void ChangeCamera()//cambio de camaras al pulsar el boton
   {
      mainCamera.gameObject.SetActive(!mainCamera.gameObject.activeSelf);
      cenitalCamera.gameObject.SetActive(!cenitalCamera.gameObject.activeSelf);
   }

    
}
