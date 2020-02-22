using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomType :MonoBehaviour
{
    [SerializeField]
    private int i_type;

    public int type
    {
        get { return i_type; }
        set { i_type = value; }
    }
    public void v_DestroyRoom()
    {
        Destroy(gameObject);
    }
}
