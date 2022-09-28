using UnityEngine;

[RequireComponent(typeof(AudioManager))]
[RequireComponent(typeof(PoolManager))]
[RequireComponent(typeof(SaveManager))]
[RequireComponent(typeof(LevelManager))]
[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(VariableManager))]
public sealed class GameManager : MonoBehaviour
{
    private static GameManager Instance;

    public static AudioManager AudioManager { get; private set; }
    public static PoolManager PoolManager { get; private set; }
    public static SaveManager SaveManager { get; private set; }
    public static LevelManager LevelManager { get; private set; }
    public static InputManager InputManager { get; private set; }
    public static VariableManager VariableManager { get; private set; }

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        //There may be a better way than this but this is theoretically faster than the previous version.
        //Also we can now control the execution order.
        VariableManager = (VariableManager)GetComponent<VariableManager>().StartUp();
        AudioManager = (AudioManager)GetComponent<AudioManager>().StartUp();
        PoolManager = (PoolManager)GetComponent<PoolManager>().StartUp();
        SaveManager = (SaveManager)GetComponent<SaveManager>().StartUp();
        LevelManager = (LevelManager)GetComponent<LevelManager>().StartUp();
        InputManager = (InputManager)GetComponent<InputManager>().StartUp();

        DontDestroyOnLoad(gameObject);
    }
}
