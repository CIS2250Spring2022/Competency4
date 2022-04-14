using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButtonController : MonoBehaviour
{
    private GameObject _parent;

    private void Start()
    {
        _parent = transform.parent.gameObject;
    }
    public void OnClose()
    {
        _parent.SetActive(false);
    }
}
