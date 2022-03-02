using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    [SerializeField]
    private Scenes _scene;

    public void Do()
    {
        SceneChanger.GoToScene(_scene);
    }
}
