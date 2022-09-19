using UnityEngine;

public class ChangeLevelButton : BaseButton
{
    [SerializeField] private LevelManager.Level changeLevelTo = LevelManager.Level.Hub;

    protected override void OnClick()
    {
        GameManager.LevelManager.ChangeLevel(changeLevelTo);
    }
}
