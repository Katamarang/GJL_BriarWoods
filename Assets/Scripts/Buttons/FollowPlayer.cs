using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    GameObject unit;
    
    private void Start()
    {
        unit = GameObject.FindGameObjectWithTag("Unit");
    }
    private void Update()
    {       
        transform.position = Camera.main.WorldToScreenPoint(unit.transform.position) + (Vector3.up * 5f); 
    }
}
