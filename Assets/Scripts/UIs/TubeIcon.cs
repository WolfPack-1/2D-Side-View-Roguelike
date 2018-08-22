using UnityEngine;
using UnityEngine.UI;


public class TubeIcon : MonoBehaviour
{
    [SerializeField] Image tubeIcon;
    Tube tube;

    public void Init(Tube tube)
    {
        this.tube = tube;
    }

    public void UpdateUI()
    {
        // TODO : 튜브 스킬에 따라 iconImage 변경
    }
}
