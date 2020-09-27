using UnityEngine;
using Zenject;

[CreateAssetMenu(menuName = "Test/DependenciesNotInjected")]
public class DependenciesNotInjected : ScriptableObject
{
    public int someValue;
    public Holder holder;
    [Inject]
    public void ThisIsNeverCalled(Holder holder)
    {
        this.holder = holder;
        Debug.Log("You will never see this;");
    }
}