using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
public class VRActivatedNO : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ActivatorVR("none"));
    }
    public IEnumerator ActivatorVR(string VRDevice)
    {
        XRSettings.LoadDeviceByName(VRDevice);
        yield return null;
        XRSettings.enabled = true;
    }
}
