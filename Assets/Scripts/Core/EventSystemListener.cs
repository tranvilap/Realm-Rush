using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystemListener : MonoBehaviour
{
    public static EventSystemListener main;
    [SerializeField] List<GameObject> listener = new List<GameObject>();

    public List<GameObject> Listeners { get => listener; }

    // Start is called before the first frame update
    void Awake()
    {
        if(main == null)
        {
            main = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        var gos = GameObject.FindGameObjectsWithTag("Listener");
        Listeners.AddRange(gos);
    }


    public void AddListener(GameObject go)
    {
        if(!Listeners.Contains(go))
        {
            Listeners.Add(go);
        }
    }
}
