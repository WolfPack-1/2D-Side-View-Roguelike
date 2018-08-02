
public interface IInteractable
{
    bool CanInteractable { get; }
    void Reset();
    void Contact();
    void Interact(Player plyaer);
}
