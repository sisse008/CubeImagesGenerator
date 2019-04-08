using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ImagesGenerator : MonoBehaviour
{
    public Button b_genrateButton;

    [Range(0.2f, 2f*Mathf.PI)]
    public float stepSize;
    
    public Camera pic_camera;

    
    float theta = 0;
    Vector3 camInitalPosition;
    Transform centerTransform;

    public Text savedImagesText;

   

    private void OnDisable()
    {
        b_genrateButton.onClick.RemoveAllListeners();
    }
  
    float radius; Vector3 center;
   
    void Start()
    {
        camInitalPosition = pic_camera.transform.position;

        b_genrateButton.onClick.AddListener(GenerateImages);

      

        centerTransform = GameManager.instance.building;
        center = centerTransform.position;
        radius = (center - pic_camera.transform.position).magnitude;

        
    }


    float[] PointOnCircle()
    {
       

        float updated_theta = theta + stepSize;

        float x = center.x + radius * Mathf.Cos(updated_theta);
        float z = center.z + radius * Mathf.Sin(updated_theta);

        theta = updated_theta;

        float[] position = { x, z };

        return position;


    }

    void SaveImagesToFile(List<Texture2D> images)
    {
        int num = 0;
        string saveImagesPath = ApplicationPath() + "/Images";
        System.IO.Directory.CreateDirectory(saveImagesPath);

        string[] files = System.IO.Directory.GetFiles(saveImagesPath);
        foreach (string file in files)
        {
            System.IO.File.Delete(file);
        }

        foreach (Texture2D _texture in images)
        {
            byte[] _bytes = _texture.EncodeToPNG();
            string name = "/image" + num.ToString() + ".png";
            num++;

           
            System.IO.File.WriteAllBytes(saveImagesPath + name, _bytes);
            Debug.Log(_bytes.Length / 1024 + "Kb was saved as: " + saveImagesPath);
        }
        
        
        savedImagesText.text = "Saved Path: " + saveImagesPath;


    }

    void GenerateImages()
    {
        List<Texture2D> images = new List<Texture2D>();
        pic_camera.transform.position = Camera.main.transform.position;
        pic_camera.transform.rotation = Camera.main.transform.rotation;

        while (theta <= 2f * Mathf.PI)
        {
            float[] new_position = PointOnCircle();
            pic_camera.transform.position = new Vector3(new_position[0], pic_camera.transform.position.y, new_position[1]);
            pic_camera.transform.LookAt(centerTransform);

            Texture2D image = pic_camera.GetComponent<TakeImage>().TakePicture_2();

            images.Add(image);
            Debug.Log("cam position = " + pic_camera.transform.position);
        }
         pic_camera.transform.position = camInitalPosition;
        Debug.Log("number of images in list = " + images.Count);

        SaveImagesToFile(images);

    }

    string ApplicationPath()
    {
        string path = Application.dataPath;
        if (Application.platform == RuntimePlatform.OSXPlayer)
        {
            path += "/../../";
        }
        else if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            path += "/../";
        }

        return path;
    }
}
