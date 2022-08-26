/// <summary>
/// Simple interface for the Managers.
/// </summary>
interface IManager
{
    /// <summary>
    /// For GameManager use to call on all the other managers.
    /// This is for priority initialisation.
    /// Should only be called once.
    /// Meant the replace Awake().
    /// </summary>
    public void StartUp();
}
