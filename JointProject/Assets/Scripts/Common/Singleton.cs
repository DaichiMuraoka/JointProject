using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    [SerializeField]
    private bool dontDestrayOnLoad = false; //Trueにするとシーンまたいでも消えない

    public static T Instance;

    public virtual void Initialize() { }    //継承クラスのAwakeの代わり

    private void Awake()
    {
        if (Instance == null)
        {
            System.Type type = typeof(T);
            Instance = FindObjectOfType(type) as T;
            Initialize();
        }
        else
        {
            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
        if (dontDestrayOnLoad)
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
