using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour {

    int currentCameraIndex = 0;
    WebCamTexture cameraTexture;
    public RawImage display;
    public Text startStopButtonText;

    public void SwitchCamera() {
        int cameraCount = WebCamTexture.devices.Length;

        if (cameraCount > 0) {
            currentCameraIndex++;
            currentCameraIndex %= cameraCount;

            if (cameraTexture != null) {
                this.StopCamera();
                this.StartStopCamera();
            }
        }
    }

    public void StartStopCamera() {
        if (cameraTexture != null)
        {
            this.StopCamera();
            this.startStopButtonText.text = "Start Camera";
        }
        else {
        WebCamDevice camera = WebCamTexture.devices[currentCameraIndex];
        this.cameraTexture = new WebCamTexture(camera.name);
        display.texture = this.cameraTexture;

            float antiRotate = -(360 - cameraTexture.videoRotationAngle);
            Quaternion quatRot = new Quaternion();
            quatRot.eulerAngles = new Vector3(0, 0, antiRotate);
            display.transform.rotation = quatRot;

        cameraTexture.Play();
            this.startStopButtonText.text = "Stop Camera";
        }
        
    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void StopCamera() {
            cameraTexture.Stop();
            display.texture = null;
            cameraTexture = null;}
}
