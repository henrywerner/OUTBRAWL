using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerMeter : MonoBehaviour
{
    public Slider powerMeter;
    public float maxPower;
    public static float currentMeter;

    // Start is called before the first frame update
    void Start()
    {
        currentMeter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        powerMeter.value = currentMeter;
    }
}
