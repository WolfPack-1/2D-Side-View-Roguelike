using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class TubeSlotUI : MonoBehaviour
{
    [SerializeField] GameObject highlight;
    [SerializeField] TubeIcon tubeIcon;
    [SerializeField] GameObject discription;
    [SerializeField] TextMeshProUGUI tubeName;
    [SerializeField] TextMeshProUGUI tubeInfo;
    List<Tube> tubes;
    int index;
    SocketEnum socket;

    public int Index { get { return index; } }

    public Tube CurrentTube
    {
        get
        {
            if (tubes != null && tubes.Count > index && tubes.Count > 0) return tubes[index];
            return null;
        }
    }

    public void Init(List<Tube> tubes, SocketEnum socket)
    {
        this.tubes = tubes;
        if (tubes == null || tubes.Count <= 0)
        {
            tubeIcon.Init(null);
            discription.SetActive(false);
            return;
        }

        index = 0;
        this.socket = socket;
        tubeIcon.Init(CurrentTube);
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (tubes == null || tubes.Count <= 0)
        {
            tubeIcon.Init(null);
            discription.SetActive(false);
            return;
        }
        tubeIcon.Init(CurrentTube);
        tubeIcon.UpdateUI();

//        string attackType;
//        switch (socket)
//        {
//            case SocketEnum.STYLE:
//                attackType = ((TubeStyleStruct) CurrentTube.TubeData).attackType.ToString();
//                break;
//            case SocketEnum.ENHANCER:
//                break;
//            case SocketEnum.COOLER:
//                break;
//            case SocketEnum.RELIC:
//                break;
//            default:
//                throw new ArgumentOutOfRangeException();
//        }
//        
        tubeName.SetText
        (
            CurrentTube.Socket + " : [" + CurrentTube.NameKor + "]"
        );
        tubeInfo.SetText
        (
            CurrentTube.Cid.ToString()
        );
    }

    public void Enable()
    {
        highlight.SetActive(true);
        discription.SetActive(true);
        UpdateUI();
    }

    public void Disalbe()
    {
        highlight.SetActive(false);
        discription.SetActive(false);
        UpdateUI();
    }

    public void Left()
    {
        index = Mathf.Clamp(index - 1, 0, int.MaxValue);
        UpdateUI();
    }

    public void Right()
    {
        index = Mathf.Clamp(index + 1, 0, tubes.Count - 1);
        UpdateUI();
    }
}
