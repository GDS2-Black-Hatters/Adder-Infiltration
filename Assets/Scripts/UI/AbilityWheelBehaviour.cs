using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static ActionInputSubscriber.CallbackContext;
using static InputManager;
using static VariableManager;

public class AbilityWheelBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject allAbilities;
    [SerializeField] private GameObject abilityIconPrefab;
    private readonly List<AbilityPivotBehaviour> pivotBehaviours = new();

    private List<AbilityBase> availableAbilities = new();
    private int selectedIndex = 0;

    [SerializeField] private int numberOfIconsShown = 3;
    [SerializeField] private float rotationTime = 0.1f;
    public static bool IsRotating { get; private set; } = false;
    private float rotationStep;
    [SerializeField] protected AK.Wwise.Event scrollSFXEvent;

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

        Dictionary<AllAbilities, AbilityBase> abilityDictionary = new();
        foreach (AbilityBase ability in allAbilities.GetComponentsInChildren<AbilityBase>())
        {
            abilityDictionary.Add(ability.AbilityID, ability);
        }

        //Get available abilities.
        foreach (Ability ability in GameManager.VariableManager.allAbilities.Abilities)
        {
            Unlockable unlock = GameManager.VariableManager.GetUnlockable(DoStatic.EnumToEnum<AllAbilities, AllUnlockables>(ability.AbilityID));
            if (unlock.IsUnlocked)
            {
                availableAbilities.Add(abilityDictionary[ability.AbilityID]);
            }
        }

        //If there is no unlocked abilities, destroy this.
        if (availableAbilities.Count == 0)
        {
            Destroy(gameObject);
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
        pivotBehaviours[midIndex].UpdateAppearance(DoStatic.GetElement(selectedIndex, ref availableAbilities));
        for (int i = 0; i < midIndex; i++)
        {
            int next = i + 1;
            pivotBehaviours[midIndex - next].UpdateAppearance(DoStatic.GetElement(selectedIndex + next, ref availableAbilities));
            pivotBehaviours[midIndex + next].UpdateAppearance(DoStatic.GetElement(selectedIndex - next, ref availableAbilities));
        }
    }

    private void Scroll(InputAction.CallbackContext callback)
    {
        scrollSFXEvent.Post(gameObject);
        float value = callback.ReadValue<float>();
        if (IsRotating || value == 0) { return; }
        StartCoroutine(Scroll(value < 0));
    }

    private void UseAbility(InputAction.CallbackContext callback)
    {
        if (LevelManager.isGamePaused) { return; }
        DoStatic.GetElement(selectedIndex, ref availableAbilities).ActivateAbility();
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
            transform.eulerAngles = Vector3.Lerp(Vector3.zero, goalRot, time.TimePercentage);
            time.Update(Time.deltaTime);
            //TODO: Update icon sizes. (Optional)
        } while (IsRotating);
    }
}
