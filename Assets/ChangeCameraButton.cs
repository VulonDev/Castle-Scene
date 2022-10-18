using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

public class ChangeCameraButton : MonoBehaviour
{
    public List<Camera> Cameras;
    private VisualElement frame;
    private Button button;

    void OnEnable() {
        var uiDocument =  GetComponent<UIDocument>();
        var rootVisualElement = uiDocument.rootVisualElement;
        frame = rootVisualElement.Q<VisualElement>("Frame");
        button = frame.Q<Button>("Button");
        button.RegisterCallback<ClickEvent>(ev => ChangeCamera());
    }

    int click = 0;

    private void ChangeCamera() {
        EnableCamera(click);
        click++;
        if (click > 3){
            click = 0;
        }
    }

    private void EnableCamera(int n) {
        Cameras.ForEach(cam =>cam.enabled = false);
        Cameras[n].enabled = true;
    }
}
