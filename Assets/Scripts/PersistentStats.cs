using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentStats : MonoBehaviour
{
    public enum WallSide { Top, Bottom, Left, Right }

    [HideInInspector]
    public WallSide ExitSide;

    private static PersistentStats instance = null;
    public static PersistentStats Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
        transform.parent = null;
    }

}
