using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wax : MonoBehaviour
{
    [SerializeField] private float _growthRate;
    [SerializeField] public bool isDone;

    private bool _isEntered;
    
    private void GrowthRate()
    {
        if (!_isEntered)
        {
            return;
        }
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1, 1, 1), _growthRate * Time.deltaTime);
    }

    private void LateUpdate()
    {
        GrowthRate();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Stick"))
        {
            _isEntered = true;
        }
       
    }
}
