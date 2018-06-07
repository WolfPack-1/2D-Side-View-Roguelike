using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class StateUIController : MonoBehaviour
{
    [SerializeField] [Range(0, 1)] float hpPercent;
    [SerializeField] [Range(0, 1)] float gaugePercent;
    
    public float HpPercent { get { return hpPercent; }}
    public float GaugePercent { get { return gaugePercent; }}

    [SerializeField] Slider hpSlider;
    [SerializeField] Transform gaugeTransform;
    Quaternion gaugeRotation;

    [SerializeField] float gaugeSpeed = 3;
    [SerializeField] float gaugeRandomness = 10;

    public void UpdateUI()
    {
        hpSlider.value = HpPercent;
        gaugeRotation = gaugeTransform.localRotation;
        Vector3 gaugeEuler = gaugeRotation.eulerAngles;
        gaugeEuler.z  = 270 * gaugePercent - 45 + Random.Range(-gaugeRandomness, gaugeRandomness);
        gaugeRotation.eulerAngles = gaugeEuler;
        gaugeTransform.rotation = Quaternion.Slerp(gaugeTransform.rotation, gaugeRotation, Time.deltaTime * gaugeSpeed);
    }

    void Update()
    {
        UpdateUI();
    }
}
