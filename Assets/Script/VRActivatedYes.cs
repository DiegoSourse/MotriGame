using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
public class VRActivatedYes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ActivatorVR("cardboard"));
    }
    public IEnumerator ActivatorVR(string VRDevice)
    {
        XRSettings.LoadDeviceByName(VRDevice);
        yield return null;
        XRSettings.enabled = true;
        //Debug.Log("DESDE EL YES " + XRSettings.loadedDeviceName.ToString());
    }
}
