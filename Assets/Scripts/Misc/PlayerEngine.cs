using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEngine : MonoBehaviour
{
    bool isMine = true;
    // Start is called before the first frame update
    void Start()
    {
        HumanoidLandController humanoidLandController = gameObject.GetComponent<HumanoidLandController>();
        if (isMine) { humanoidLandController.enabled = true; } else { humanoidLandController.enabled = false; }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
