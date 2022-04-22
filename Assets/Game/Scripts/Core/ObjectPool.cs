using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour where T : Component
{
    private static ObjectPool<T> instance;
    public static ObjectPool<T> Instance => instance ??= FindObjectOfType<ObjectPool<T>>();

    [SerializeField] private T poolObject;
    [SerializeField] private int poolSize;
    private List<T> pool;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(this.transform);
        FillThePool();
    }

    private void Start()
    {
        GameManager.ActionLevelPassed += CallBackAllObjects;
        GameManager.ActionGameOver += CallBackAllObjects;       
    }

    private void FillThePool()
    {
        pool = new List<T>();

        for (int i = 0; i < poolSize; i++)
        {
            var objectForPool = Instantiate(poolObject, transform);
            objectForPool.gameObject.SetActive(false);
            pool.Add(objectForPool);
        }
    }

    public T GetObject()
    {
        for (int i = 0; i < poolSize; i++)
        {
            if (!pool[i].gameObject.activeInHierarchy)
            {
                pool[i].gameObject.SetActive(true);
                return pool[i];
            }
        }

        return null;
    }

    public void BringObject(T bringedObject, float disableTime)
    {
        StartCoroutine(BringObjectRoutine(bringedObject, disableTime));
    }
    private IEnumerator BringObjectRoutine(T bringedObject, float time)
    {
        yield return new WaitForSeconds(time);

        bringedObject.gameObject.SetActive(false);
    }

    private void CallBackAllObjects()
    {
        StartCoroutine(CallBackAllObjectsRoutine());
    }
    private IEnumerator CallBackAllObjectsRoutine()
    {
        yield return new WaitForSeconds(1.5f);// from GameManager

        for (int i = 0; i < poolSize; i++)
        {
            pool[i].gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        GameManager.ActionLevelPassed -= CallBackAllObjects;
        GameManager.ActionGameOver -= CallBackAllObjects;
    }
}
