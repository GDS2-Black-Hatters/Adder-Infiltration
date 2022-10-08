using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static ActionInputSubscriber.CallbackContext;
using static InputManager;
using static VariableManager;

public class AbilityWheelBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject abilityIconPrefab;
    private readonly List<AbilityPivotBehaviour> pivotBehaviours = new();

    private List<AbilityBase> availableAbilities = new();
    private int selectedIndex = 0;

    [SerializeField] private int numberOfIconsShown = 3;
    [SerializeField] private float rotationTime = 0.1f;
    public static bool IsRotating { get; private set; } = false;
    private float rotationStep;

    private void Start()
    {
        numberOfIconsShown += 1 - numberOfIconsShown & 1; //& 1 checks for odd and return 1 if true. Much faster than mod.

        //Calculate constant rotation
        rotationStep = 120 / (numberOfIconsShown + 1);

        //Spawn numIcons + 2 with correct rotation
        for (int i = 0; i < numberOfIconsShown + 2; i++)
        {
            Transform child = Instantiate(abilityIconPrefab, transform).transform;
            pivotBehaviours.Add(child.GetComponent<AbilityPivotBehaviour>());

            child.eulerAngles = new()
            {
                z = -60 + rotationStep * i,
            };
        }

        //Get available abilities.
        foreach (AllAbilities abilityType in DoStatic.EnumList<AllAbilities>())
        {
            Upgradeable upgrade = (Upgradeable)GameManager.VariableManager.GetUnlockable(DoStatic.EnumToEnum<AllAbilities, AllUnlockables>(abilityType));
            if (upgrade.UnlockProgression != 0)
            {
                availableAbilities.Add(GameManager.VariableManager.GetAbility(abilityType));
            }
        }

        //If there is no unlocked abilities, then just turn them off and do nothing else.
        if (availableAbilities.Count == 0)
        {
            gameObject.SetActive(false);
            return;
        }
        UpdateAbilityWheel();

        //Add scrolling input delegate
        gameObject.AddComponent<ActionInputSubscriber>().AddActions(new()
        {
            new(MainGameScroll, Performed, Scroll),
            new(MainGameAbility, Performed, UseAbility),
        });
    }

    private void UpdateAbilityWheel()
    {
        int midIndex = Mathf.RoundToInt(pivotBehaviours.Count * 0.5f);
        pivotBehaviours[midIndex].UpdateAppearance(DoStatic.GetIndexValue(selectedIndex, ref availableAbilities));
        for (int i = 0; i < midIndex; i++)
        {
            int next = i + 1;
            pivotBehaviours[midIndex - next].UpdateAppearance(DoStatic.GetIndexValue(selectedIndex + next, ref availableAbilities));
            pivotBehaviours[midIndex + next].UpdateAppearance(DoStatic.GetIndexValue(selectedIndex - next, ref availableAbilities));
        }
    }

    private void Scroll(InputAction.CallbackContext callback)
    {
        float value = callback.ReadValue<float>();
        if (IsRotating || value == 0) { return; }
        StartCoroutine(Scroll(value < 0));
    }

    private void UseAbility(InputAction.CallbackContext callback)
    {
        DoStatic.GetIndexValue(selectedIndex, ref availableAbilities).ActivateAbility();
    }

    private IEnumerator Scroll(bool scrollDown)
    {
        IsRotating = true;
        Vector3 goalRot = new(0, 0, scrollDown ? rotationStep : -rotationStep);
        TimeTracker time = new(rotationTime, 1);
        time.onFinish += () =>
        {
            IsRotating = false;
            transform.eulerAngles = new();
            selectedIndex += scrollDown ? 1 : -1;
            UpdateAbilityWheel();
            //TODO: Reset icon sizes (Optional)
        };

        do
        {
            yield return null;
            time.Update(Time.deltaTime);
            transform.eulerAngles = Vector3.Lerp(Vector3.zero, goalRot, time.TimePercentage);
            //TODO: Update icon sizes. (Optional)
        } while (time.TimePercentage != 1);
        time.Reset();
    }
}
