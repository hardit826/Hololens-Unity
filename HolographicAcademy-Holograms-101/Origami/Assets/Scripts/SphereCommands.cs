using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCommands : MonoBehaviour
{
    Vector3 originalPos;

    void Start()
    {
        originalPos = this.transform.localPosition;
    }
 
   void OnSelect()
    {
        if(!this.GetComponent<Rigidbody>())
        {
            var rigidbody = this.gameObject.AddComponent<Rigidbody>();
            rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }
    }

    void OnReset()
    {
        var rigidbody = this.GetComponent<Rigidbody>();
        if(rigidbody!=null)
        {
            DestroyImmediate(rigidbody);
        }
        this.transform.position = originalPos;
    }
    void OnDrop()
    {
        OnSelect();
    }
}
