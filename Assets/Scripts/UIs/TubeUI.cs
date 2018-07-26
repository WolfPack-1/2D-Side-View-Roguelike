using UnityEngine;

public class TubeUI : MonoBehaviour
{
    [SerializeField] Tube tube;

    public void SetTube(Tube tube)
    {
        this.tube = tube;
        Enable();
    }

    void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
