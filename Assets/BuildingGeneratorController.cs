using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BuildingGeneratorController : MonoBehaviour
{
    


    public InputField scaleX;
    public InputField scaleY;
    public InputField scaleZ;


    public Button b_generateBuilding;


    // Start is called before the first frame update
    void Start()
    {

        scaleX.text = "1";
        scaleY.text = "1";
        scaleZ.text = "1";
        b_generateBuilding.onClick.AddListener(GenerateBuilding);
    }

    private void OnDisable()
    {
        b_generateBuilding.onClick.RemoveAllListeners();
    }

    // Update is called once per frame
    void GenerateBuilding()
    {


        float x;
        float y;
        float z;

        if (float.TryParse(scaleX.text, out x))
        {
            if (float.TryParse(scaleY.text, out y))
            {
                if (float.TryParse(scaleZ.text, out z))
                {
                    GameManager.instance.building.localScale = new Vector3(x, y, z);
                    return;
                }
            }

        }

        Debug.Log("INVALID X, Y, OR Z VALUES. MAKE SURE NO SPACE");

        
    }
}
