using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CraftingUIController : MonoBehaviour
{
    PlayerInventory inventory;

    [SerializeField] Transform styleTubeHolder;
    [SerializeField] Transform enhancerTubeHolder;
    [SerializeField] Transform coolerTubeHolder;
    [SerializeField] Transform relicTubeHolder;
    [SerializeField] SkillUI skillIcon;
    [SerializeField] TextMeshProUGUI tubeDiscription;
    [SerializeField] TextMeshProUGUI skillDiscription;

    int styleIndex, enhancerIndex, coolerIndex, relicIndex;

    public void Init(Player player)
    {
        inventory = player.Inventory;
    }

    void UpdateUI()
    {
        
        styleIndex = Math.Min(styleIndex, inventory.GetCount(SocketEnum.STYLE) - 1);
        enhancerIndex = Math.Min(enhancerIndex, inventory.GetCount(SocketEnum.ENHANCER) - 1);
        coolerIndex = Math.Min(coolerIndex, inventory.GetCount(SocketEnum.COOLER) - 1);
        relicIndex = Math.Min(relicIndex, inventory.GetCount(SocketEnum.RELIC) - 1);

        styleIndex = Math.Max(styleIndex, 0);
        enhancerIndex = Math.Max(enhancerIndex, 0);
        coolerIndex = Math.Max(coolerIndex, 0);
        relicIndex = Math.Max(relicIndex, 0);
        
        
        List<Tube> styleTubes = inventory.Tubes.FindAll(t => t.Socket == SocketEnum.STYLE);
        List<Tube> enhancerTubes = inventory.Tubes.FindAll(t => t.Socket == SocketEnum.ENHANCER);
        List<Tube> coolerTubes = inventory.Tubes.FindAll(t => t.Socket == SocketEnum.COOLER);
        List<Tube> relicTubes = inventory.Tubes.FindAll(t => t.Socket == SocketEnum.RELIC);

        if (styleTubes.Count > 0)
            styleTubeHolder.GetChild(0).GetComponent<TubeUI>().SetTube(styleTubes[styleIndex]);
        else
            styleTubeHolder.GetChild(0).GetComponent<TubeUI>().Disable();

        if (enhancerTubes.Count > 0)
            enhancerTubeHolder.GetChild(0).GetComponent<TubeUI>().SetTube(enhancerTubes[enhancerIndex]);
        else
            enhancerTubeHolder.GetChild(0).GetComponent<TubeUI>().Disable();

        if (coolerTubes.Count > 0)
            coolerTubeHolder.GetChild(0).GetComponent<TubeUI>().SetTube(coolerTubes[coolerIndex]);
        else
            coolerTubeHolder.GetChild(0).GetComponent<TubeUI>().Disable();

        if (relicTubes.Count > 0)
            relicTubeHolder.GetChild(0).GetComponent<TubeUI>().SetTube(relicTubes[relicIndex]);
        else
            relicTubeHolder.GetChild(0).GetComponent<TubeUI>().Disable();

        tubeDiscription.text = "Style Tube " + styleTubes.Count + " Counts\n" +
                               "Enhancer Tube " + enhancerTubes.Count + " Counts\n" +
                               "Cooler Tube " + coolerTubes.Count + " Counts\n" +
                               "Relic Tube " + relicTubes.Count + " Counts";

        if (styleTubes.Count > 0 && enhancerTubes.Count > 0 && coolerTubes.Count > 0)
        {
            skillDiscription.text = "Current Selected Tubes\n" +
                                    styleTubes[styleIndex].Name + "\n" +
                                    enhancerTubes[enhancerIndex].Name + "\n" +
                                    coolerTubes[coolerIndex].Name;
            skillIcon.SetTube(styleTubes[styleIndex], enhancerTubes[enhancerIndex], coolerTubes[coolerIndex]);
        }
        else
        {
            skillDiscription.text = "Need More Tubes";
            skillIcon.Disable();
        }
    }

    public void Open()
    {
        if (gameObject.activeSelf)
        {
            Close();
            return;
        }

        gameObject.SetActive(true);
        UpdateUI();
    }

    public void Close()
    {
        gameObject.SetActive(false);
        styleIndex = enhancerIndex = coolerIndex = relicIndex = 0;
    }

    public void CraftingLeftButton(int socket)
    {
        switch ((SocketEnum) socket)
        {
            case SocketEnum.STYLE:
                styleIndex = Math.Max(styleIndex - 1, 0);
                break;
            case SocketEnum.ENHANCER:
                enhancerIndex = Math.Max(enhancerIndex - 1, 0);
                break;
            case SocketEnum.COOLER:
                coolerIndex = Math.Max(coolerIndex - 1, 0);
                break;
            case SocketEnum.RELIC:
                relicIndex = Math.Max(relicIndex - 1, 0);
                break;
        }
        UpdateUI();
    }

    public void CraftingRightButton(int socket)
    {
        switch ((SocketEnum) socket)
        {
            case SocketEnum.STYLE:
                styleIndex = Math.Min(styleIndex + 1, inventory.GetCount((SocketEnum) socket) - 1);
                break;
            case SocketEnum.ENHANCER:
                enhancerIndex = Math.Min(enhancerIndex + 1, inventory.GetCount((SocketEnum) socket) - 1);
                break;
            case SocketEnum.COOLER:
                coolerIndex = Math.Min(coolerIndex + 1, inventory.GetCount((SocketEnum) socket) - 1);
                break;
            case SocketEnum.RELIC:
                relicIndex = Math.Min(relicIndex + 1, inventory.GetCount((SocketEnum) socket) - 1);
                break;
        }
        UpdateUI();
    }

    public void CraftSkill()
    {
        if (!skillIcon.IsInitByTube && !skillIcon.IsInitBySkill) return;
        inventory.CreateSkill(skillIcon.StyleCid, skillIcon.EnhancerCid, skillIcon.CoolerCid);
        UpdateUI();
    }
}