using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelectScreenController : MonoBehaviour
{
    [SerializeField]
    private GameObject _towers;
    [SerializeField]
    private GameObject _traps;

    public void AdjustScreen()
    {
        if (LevelController.CurrentSelected.IsPath)
        {
            _traps.SetActive(true);
            _towers.SetActive(false);   
        }

        else if (LevelController.IsAdjacentToPath(LevelController.CurrentSelected))
        {
            _towers.SetActive(true);
            _traps.SetActive(false);
        }

        else
        {
            _towers.SetActive(false);
            _traps.SetActive(false);
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
