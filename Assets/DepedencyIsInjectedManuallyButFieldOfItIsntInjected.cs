using UnityEngine;
using Zenject;

[CreateAssetMenu(menuName = "Test/DepedencyIsInjectedManuallyButFieldOfItIsntInjected")]
public class DepedencyIsInjectedManuallyButFieldOfItIsntInjected : ScriptableObject
{
    public DependenciesNotInjected thisWontBeInjected;
    public Holder holder;
    [Inject]
    public void ThisIsCalledFromHolderStart(Holder holder)
    {
        this.holder = holder;
        Debug.Log("Sandwhiched Between Start messages");
    }
}