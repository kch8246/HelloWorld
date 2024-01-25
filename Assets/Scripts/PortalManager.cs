using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    [SerializeField] private Portal portalRed = null;
    [SerializeField] private Portal portalBlue = null;

    private bool isSetRed = false;
    private bool isSetBlue = false;


    public void ActivatePortal(EPortalType _type, Vector3 _pos)
    {
        if (_type == EPortalType.Red)
        {
            isSetRed = true;
            portalRed.transform.position = _pos;
        }
        else
        {
            isSetBlue = true;
            portalBlue.transform.position = _pos;
        }

        if (isSetRed && isSetBlue)
        {
            portalRed.ActivatePortal();
            portalBlue.ActivatePortal();
        }
    }

    public void DeactivatePortal(EPortalType _type)
    {
        if (_type == EPortalType.Red)
        {
            isSetRed = false;
            portalRed.transform.localPosition = Vector3.zero;
        }
        else
        {
            isSetBlue = false;
            portalBlue.transform.localPosition = Vector3.zero;
        }

        portalRed.DeactivatePortal();
        portalBlue.DeactivatePortal();
    }

    public void PassProcess(EPortalType _inType, GameObject _obj)
    {
        if (_inType == EPortalType.Red)
            portalBlue.PassProcess(_obj);
        else if (_inType == EPortalType.Blue)
            portalRed.PassProcess(_obj);
    }
}
