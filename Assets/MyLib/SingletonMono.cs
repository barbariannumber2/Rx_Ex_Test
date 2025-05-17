#if UNITY_EDITOR 
 //エディター上でもヒエラルキー整理用処理を有効にしないなら以下をコメントアウト
 #define HIERARCHY_ORGANIZE
#endif

using UnityEngine;
using MyLib.Tag;

public class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T>
{
    /// <summary>
    /// DontDestroyOnLoadに設定するか？
    /// falseにしたければインスペクターか派生先Init内で操作
    /// </summary>
    [SerializeField]
    protected bool _dontDestroy = true;

    private static T instance;

    /// <summary>
    /// 初期化済みか？
    /// </summary>
    private static bool isInitialized = false;

#if HIERARCHY_ORGANIZE
    private static readonly string parentObjectName = "Singletons";
    private static readonly string parentObjectTag = "ParentsOfSingletons";
    private static readonly string nonDestroyParentObjectTag = "NonDestroy" + parentObjectTag;
#endif

    public static T I
    {
        get
        {
            if (HasInstance() == false)
            {
                T result = FindInstance();
                if(result == null)//みつからければ作成
                {
                    //作成した時点でAwakeが呼ばれるので初期化も呼ばれる
                    GameObject gameObj = new GameObject(typeof(T).Name,typeof(T));
                }
                else
                {
                    instance = result;
                    instance.InitIfNeed();
                }
                
            }
            return instance;
        }
    }

    /// <summary>
    /// インスタンスの削除
    /// 基本的には使わないはず(?)
    /// </summary>
    public static void DeleteInstance()
    {
        if (HasInstance() == false)
            return;

        isInitialized = false;
        Destroy(instance.gameObject);
        instance = null;
    }

    /// <summary>
    /// 派生先で行いたい初期化処理
    /// </summary>
    protected virtual void Init(){}

    private void Awake()
    {
        if (HasInstance() == false)
        {
            instance = (T)this;
        }

        if (CompareThisAndInstance() == false)
        {
            Destroy(gameObject);
        }

        InitIfNeed();
    }

    /// <summary>
    /// 必要なら初期化を行う
    /// </summary>
    private void InitIfNeed()
    {
        if (isInitialized)
        {
            return;
        }

        Init();
        isInitialized = true;

#if HIERARCHY_ORGANIZE
        instance.SettingParents();
#endif

        if (_dontDestroy)
        {
#if HIERARCHY_ORGANIZE
            DontDestroyOnLoad(transform.parent);
#else
            DontDestroyOnLoad(this);
#endif
        }
    }

    /// <summary>
    /// インスタンス変数がnullではなく実体を保持しているか？
    /// </summary>
    /// <returns>保持していればtrueを返す</returns>
    private static bool HasInstance()
    {
        return instance != null;
    }

    /// <summary>
    /// 型検索でインスタンスを探してくる
    /// </summary>
    /// <returns>探索結果　見つからなければnullを返す</returns>
    private static T FindInstance()
    {
        //return (T)FindObjectOfType(typeof(T));//非推奨化
        return (T)FindAnyObjectByType(typeof(T));
    }

    /// <summary>
    /// 自身とinstance変数の示すインスタンスが同じか？
    /// </summary>
    /// <returns>同じならtrueを返す</returns>
    private bool CompareThisAndInstance()
    {
        return this == instance;
    }

#if HIERARCHY_ORGANIZE
    /// <summary>
    /// エディター上で確認する時だけ整理用の親オブジェクトを設定
    /// </summary>
    private void SettingParents()
    {
        TagHelper.AddTag(parentObjectTag);
        TagHelper.AddTag(nonDestroyParentObjectTag);

        //_dontDestroyがfalseならDontDestroyOnLoadに指定した親の子にしてはいけないので判別
        GameObject parentObject = null;
        if (_dontDestroy)
        {
            parentObject = GameObject.FindGameObjectWithTag(nonDestroyParentObjectTag);
        }
        else
        {
            parentObject = GameObject.FindGameObjectWithTag(parentObjectTag);
        }
     
        //対応する親が見つからなければ作成
        if (parentObject == null)
        {
            parentObject = new GameObject(parentObjectName);
            parentObject.tag = _dontDestroy ? nonDestroyParentObjectTag : parentObjectTag;

        }
        instance.transform.parent = parentObject.transform;
    }
#endif
}
