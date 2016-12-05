using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToPlaceParent : MonoBehaviour
{
    bool placing = false;

    void OnSelect()
    {
        placing = !placing;
        if(placing)
        {
            SpatialMapping.Instance.drawVisualMeshes = true;
        }
        else
        {
            SpatialMapping.Instance.drawVisualMeshes = false;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(placing)
        {
            var headPos = Camera.main.transform.position;
            var gazeDir = Camera.main.transform.forward;

            RaycastHit hitInfo;
            if(Physics.Raycast(headPos, gazeDir, out hitInfo, 30.0f, SpatialMapping.PhysicsRaycastMask))
            {
                this.transform.parent.position = hitInfo.point;

                Quaternion toQuat = Camera.main.transform.localRotation;
                toQuat.x = 0;
                toQuat.z = 0;
                this.transform.parent.rotation = toQuat;
            }
        }
	}
}
