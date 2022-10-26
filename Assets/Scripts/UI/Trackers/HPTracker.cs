using UnityEngine;

public class HPTracker : CaughtVariableTracker
{
    private readonly Lerper fillAmountLerper = new();
    [SerializeField] private Color full = new(0, 1, 0);
    [SerializeField] private Color empty = new(1, 0, 0);


    protected override void Start()
    {
        base.Start();
        GameManager.LevelManager.ActiveSceneController.Player.HP.onHurt += UpdateHP;
        fillAmountLerper.SetValues(0, 0.25f, 1);
    }

    private void UpdateHP()
    {
        float percent = GameManager.LevelManager.ActiveSceneController.Player.HP.healthPercentage;
        ui.fillAmount = Mathf.Lerp(0, 0.25f, percent);
        ui.color = Color.Lerp(empty, full, DoStatic.RoundToNearestFloat(percent, 0.25f));
    }

    private void OnDisable()
    {
        GameManager.LevelManager.ActiveSceneController.Player.HP.onHurt -= UpdateHP;
    }
}
