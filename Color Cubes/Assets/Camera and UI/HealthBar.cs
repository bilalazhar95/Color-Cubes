using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
   [SerializeField] private Transform bar = null;

    private void Start()
    {
      
    }

    public void SetBarSize(float sizeNormalized)
    {
        bar.localScale = new Vector3(sizeNormalized,1f,1f);
    }


}
