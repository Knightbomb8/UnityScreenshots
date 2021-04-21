using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading.Tasks;

public class ScreenShotAutomation : MonoBehaviour
{
    public List<Camera> cameras;
    public int imageCount = 0;
    
    /// <summary>
    /// checks for button press to trigger image creation
    /// </summary>
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            TakePictures();
            Debug.Log("Images Taken and saved in Assets/Screenshots, Check File Explorer as they take up to a minute to show in Unity project folder.");
        }
    }

    /// <summary>
    /// takes a picture and saves it for every camera in cameras
    /// </summary>
    void TakePictures()
    {
        foreach (Camera camera in cameras)
        {
            RenderTexture activeRenderTexture = RenderTexture.active;
            RenderTexture.active = camera.targetTexture;
 
            camera.Render();
 
            Texture2D image = new Texture2D(camera.targetTexture.width, camera.targetTexture.height);
            image.ReadPixels(new Rect(0, 0, camera.targetTexture.width, camera.targetTexture.height), 0, 0);
            image.Apply();
            RenderTexture.active = activeRenderTexture;
 
            byte[] bytes = image.EncodeToPNG();
            Destroy(image);
 
            File.WriteAllBytes(Application.dataPath + "/Screenshots/" + camera.transform.name + " " + imageCount + ".png", bytes);
        }
        imageCount++;
    }
}
