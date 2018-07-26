 using UnityEngine;
using UnityEngine.UI;

public class StateUIController : MonoBehaviour
{
    [SerializeField] Slider hpSlider;
    [SerializeField] Transform gaugeTransform;
    Quaternion gaugeRotation;

    [SerializeField] float gaugeSpeed = 3;
    [SerializeField] float gaugeRandomness = 10;

    Player player;

    public void Init(Player player)
    {
        this.player = player;
    }

    void Update()
    {
        UpdateUI();
    }
    
    public void UpdateUI()
    {
        if (!player)
            return;
        
        hpSlider.value = player.CurrentHp / player.MaxHp;
        gaugeRotation = gaugeTransform.localRotation;
        Vector3 gaugeEuler = gaugeRotation.eulerAngles;
        gaugeEuler.z  = 270 * (player.CurrentSteam / player.MaxSteam) - 45 + Random.Range(-gaugeRandomness, gaugeRandomness);
        gaugeRotation.eulerAngles = gaugeEuler;
        gaugeTransform.rotation = Quaternion.Slerp(gaugeTransform.rotation, gaugeRotation, Time.deltaTime * gaugeSpeed);
    }
}
