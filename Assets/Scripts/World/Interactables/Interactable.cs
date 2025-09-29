using UnityEngine;

public interface IInteractable
{
    public bool beenSlashed { get; set; }
    public void Slash(GameObject context);
}
