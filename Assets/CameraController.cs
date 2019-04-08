using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CameraController : MonoBehaviour
{

    public enum Axis
    {
        X,
        Y,
        Z
    };
    public Slider s_position_X;
    public Slider s_position_Y;
    public Slider s_position_Z;

    public Button b_lookAtBuilding;

   // public Slider s_rotation_X;
    //public Slider s_rotation_Y;
   // public Slider s_rotation_Z;


    Camera camera;

    private void Awake()
    {
        camera = Camera.main;
    }


    public void Start()
    {
        //Adds a listener to the main slider and invokes a method when the value changes.
        s_position_X.onValueChanged.AddListener(delegate { UpdateCamPosition(); });
        s_position_Y.onValueChanged.AddListener(delegate { UpdateCamPosition(); });
        s_position_Z.onValueChanged.AddListener(delegate { UpdateCamPosition(); });

        b_lookAtBuilding.onClick.AddListener(LookatBuilding);
     //   s_rotation_X.onValueChanged.AddListener(delegate { UpdateCamRotation(); });
     //  s_rotation_Y.onValueChanged.AddListener(delegate { UpdateCamRotation(); });
     //    s_rotation_Z.onValueChanged.AddListener(delegate { UpdateCamRotation(); });
    }

    private void OnDisable()
    {
        s_position_X.onValueChanged.RemoveAllListeners();
        s_position_Y.onValueChanged.RemoveAllListeners();
        s_position_Z.onValueChanged.RemoveAllListeners();

     //   s_rotation_X.onValueChanged.RemoveAllListeners();
     //   s_rotation_Y.onValueChanged.RemoveAllListeners();
     //   s_rotation_Z.onValueChanged.RemoveAllListeners();
    }

    void LookatBuilding()
    {
        camera.transform.LookAt(GameManager.instance.building);
    }
    // Invoked when the value of the slider changes.
    void UpdateCamPosition()
    {
        camera.transform.position = new Vector3(s_position_X.value, s_position_Y.value, s_position_Z.value);
       
    }

    /*void UpdateCamRotation()
    {
        camera.transform.eulerAngles = new Vector3(s_rotation_X.value, s_rotation_Y.value, s_rotation_Z.value);
    }*/
    
}
