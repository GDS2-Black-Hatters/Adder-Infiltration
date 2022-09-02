using UnityEngine;

public class Matter : MonoBehaviour
{
    public virtual void InitilizeMatter(MatterShell ownerShell, Transform anchorBase)
    {
        //Do Nothing, Intended to be override by derived classes that may want more complex behavior.
    }
}
