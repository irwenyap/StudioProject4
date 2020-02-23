using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGen : MonoBehaviour
{

    public int SpawnDirection;
   
    int I_direction;
    [SerializeField]
    private Transform[] T_C_startPos;
    [SerializeField]
    public GameObject[] G_C_A_rooms;

    public GameObject[] DeadendRooms;

    // index 0 == LR Room, index 1 == LRD Room, index 2 == LRU Room ,index 3 == LRUD Room
    [SerializeField]
    public Stack<GameObject> path;

    public Stack<GameObject> S_GO_roomData;

    public List<Vector2> L_V2_roomPosition;

    public List<int> L_V2_roomtype;

    [SerializeField]
    public float F_roomDIfferent;
    [SerializeField]
    private float F_timeBtwnRoom = 0.25f;
    [SerializeField]
    private float F_startimeBtwnRoom = 0.25f;
    [SerializeField]
    private float F_minX;
    [SerializeField]
    private float F_minY;
    [SerializeField]
    private float F_maxX;
    [SerializeField]
    private float F_maxY;

    public bool stopGeneration = false;

    public LayerMask room;

    public int I_roomcounter;
    public int I_maxRoom;

    public int downcounter;
    public int seed;
    public string I_seed;
    [SerializeField]
    GameObject renderMap;
    [System.Obsolete]
    private void Awake()
    {
        path = new Stack<GameObject>();
        S_GO_roomData = new Stack<GameObject>();
        L_V2_roomPosition = new List<Vector2>();
        L_V2_roomtype = new List<int>();
        if(seed != 0)
        {
        Random.seed = seed;
           
        }
    }

    // Start is called before the first frame update
    void Start()
    {


        I_seed = "";
        I_roomcounter = 0;
        I_maxRoom = Random.Range(10, 40);
        int randStartPos = Random.Range(0, T_C_startPos.Length);
       
        GameObject instance = (GameObject)Instantiate(G_C_A_rooms[0], transform.position, Quaternion.identity);
        I_direction = Random.Range(1, 6);
        path.Push(instance);
        S_GO_roomData.Push(instance);


        I_seed += 0;
        I_roomcounter++;


        I_seed += I_direction;
    }
    private void Update()
    {
        if (F_timeBtwnRoom <= 0 && !stopGeneration)
        {
            V_Move();
            F_timeBtwnRoom = F_startimeBtwnRoom;
        }
        else
        {
            F_timeBtwnRoom -= Time.deltaTime;
        }
    }

    private void V_Move()
    {
        if (I_roomcounter > I_maxRoom)
        {
            stopGeneration = true;
            fillRoom();
           // renderMap.SetActive(true);
        }
        else if (SpawnDirection == 0)// Up TO down
        {

            if (I_direction == 1 || I_direction == 2)//Moving Right
            {
                Vector2 V2_newPos = new Vector2(transform.position.x + F_roomDIfferent, transform.position.y);
                Collider2D Detection = Physics2D.OverlapCircle(V2_newPos, 1, room);
                if (Detection == null)
                {

                    transform.position = V2_newPos;

                    //Room choosing
                    int randroom = Random.Range(0, G_C_A_rooms.Length);
                    I_seed += randroom;
                    GameObject instance = Instantiate(G_C_A_rooms[randroom], transform.position, Quaternion.identity);

                    path.Push(instance);

                    S_GO_roomData.Push(instance);
                    //direction
                    I_direction = Random.Range(1, 6);
                    if (I_direction == 3)
                    {
                        I_direction = 1;
                    }
                    else if (I_direction == 4)
                    {
                        I_direction = 5;
                    }
                    downcounter = 0;
                    I_roomcounter++;
                }
                else
                    I_direction = 5;
            }
            else if (I_direction == 3 || I_direction == 4)//Moving Left
            {
                Vector2 V2_newPos = new Vector2(transform.position.x - F_roomDIfferent, transform.position.y);
                Collider2D Detection = Physics2D.OverlapCircle(V2_newPos, 1, room);
                if (Detection == null)
                {

                    transform.position = V2_newPos;
                    //room choosing
                    int randroom = Random.Range(0, G_C_A_rooms.Length);
                    I_seed += randroom;
                    GameObject instance = Instantiate(G_C_A_rooms[randroom], transform.position, Quaternion.identity);
                    path.Push(instance);
                    S_GO_roomData.Push(instance);

                    //direction movement
                    I_direction = Random.Range(1, 6);
                    if (I_direction == 1)
                    {
                        I_direction = 3;
                    }
                    else if (I_direction == 2)
                    {
                        I_direction = 5;
                    }
                    downcounter = 0;
                    I_roomcounter++;
                }
                else
                    I_direction = 5;

            }
            else if (I_direction == 5)//Moving Down
            {
                Vector2 V2_newPos = new Vector2(transform.position.x, transform.position.y - F_roomDIfferent);
                Collider2D Detection = Physics2D.OverlapCircle(V2_newPos, 1, room);
                if (Detection == null)
                {
                    ++downcounter;
                    Detection = Physics2D.OverlapCircle(transform.position, 1, room);
                    if (Detection != null)
                    {

                        if (Detection.GetComponent<RoomType>().type != 1)
                        {
                            if (Detection.GetComponent<RoomType>().type != 3)
                            {
                                if (downcounter >= 2)
                                {
                                    Detection.GetComponent<RoomType>().v_DestroyRoom();
                                    path.Pop();
                                    S_GO_roomData.Pop();
                                    GameObject instance = Instantiate(G_C_A_rooms[3], transform.position, Quaternion.identity);
                                    S_GO_roomData.Push(instance);

                                    path.Push(instance);
                                    I_seed += 3;
                                }
                                else
                                {

                                    Detection.GetComponent<RoomType>().v_DestroyRoom();
                                    int randbot = Random.Range(1, 4);
                                    if (randbot == 2)
                                    {
                                        randbot = 1;
                                    }
                                    I_seed += randbot;
                                    path.Pop();
                                    S_GO_roomData.Pop();
                                    GameObject instance = Instantiate(G_C_A_rooms[randbot], transform.position, Quaternion.identity);
                                    path.Push(instance);
                                    S_GO_roomData.Push(instance);

                                }
                            }

                        }
                    }

                    // Vector2 V2_newPos = new Vector2(transform.position.x , transform.position.y - F_roomDIfferent);
                    transform.position = V2_newPos;


                    int randroom = Random.Range(2, G_C_A_rooms.Length);
                    I_seed += randroom;
                    GameObject instance2 = Instantiate(G_C_A_rooms[randroom], transform.position, Quaternion.identity);
                    path.Push(instance2);
                    S_GO_roomData.Push(instance2);

                    I_direction = Random.Range(1, 6);
                    I_roomcounter++;
                }


            }

        }
        else if (SpawnDirection == 1)//Down to Up
        {

            if (I_direction == 1 || I_direction == 2)//Moving Right
            {
                Vector2 V2_newPos = new Vector2(transform.position.x + F_roomDIfferent, transform.position.y);
                Collider2D Detection = Physics2D.OverlapCircle(V2_newPos, 1, room);
                if (Detection == null)
                {

                    transform.position = V2_newPos;

                    //Room choosing
                    int randroom = Random.Range(0, G_C_A_rooms.Length);
                    I_seed += randroom;
                    GameObject instance = Instantiate(G_C_A_rooms[randroom], transform.position, Quaternion.identity);

                    path.Push(instance);

                    S_GO_roomData.Push(instance);
                    //direction
                    I_direction = Random.Range(1, 6);
                    if (I_direction == 3)
                    {
                        I_direction = 1;
                    }
                    else if (I_direction == 4)
                    {
                        I_direction = 5;
                    }
                    downcounter = 0;
                    I_roomcounter++;
                }
                else
                    I_direction = 5;
            }
            else if (I_direction == 3 || I_direction == 4)//Moving Left
            {
                Vector2 V2_newPos = new Vector2(transform.position.x - F_roomDIfferent, transform.position.y);
                Collider2D Detection = Physics2D.OverlapCircle(V2_newPos, 1, room);
                if (Detection == null)
                {

                    transform.position = V2_newPos;
                    //room choosing
                    int randroom = Random.Range(0, G_C_A_rooms.Length);
                    I_seed += randroom;
                    GameObject instance = Instantiate(G_C_A_rooms[randroom], transform.position, Quaternion.identity);
                    path.Push(instance);
                    S_GO_roomData.Push(instance);

                    //direction movement
                    I_direction = Random.Range(1, 6);
                    if (I_direction == 1)
                    {
                        I_direction = 3;
                    }
                    else if (I_direction == 2)
                    {
                        I_direction = 5;
                    }
                    downcounter = 0;
                    I_roomcounter++;
                }
                else
                    I_direction = 5;

            }
            else if (I_direction == 5)//Moving Up
            {
                Vector2 V2_newPos = new Vector2(transform.position.x, transform.position.y + F_roomDIfferent);
                Collider2D Detection = Physics2D.OverlapCircle(V2_newPos, 1, room);
                if (Detection == null)
                {
                    ++downcounter;
                    Detection = Physics2D.OverlapCircle(transform.position, 1, room);
                    if (Detection != null)
                    {

                        if (Detection.GetComponent<RoomType>().type != 2)
                        {
                            if (Detection.GetComponent<RoomType>().type != 3)
                            {
                                if (downcounter >= 2)
                                {
                                    Detection.GetComponent<RoomType>().v_DestroyRoom();
                                    path.Pop();
                                    S_GO_roomData.Pop();
                                    GameObject instance = Instantiate(G_C_A_rooms[3], transform.position, Quaternion.identity);
                                    S_GO_roomData.Push(instance);

                                    path.Push(instance);
                                    I_seed += 3;
                                }
                                else
                                {

                                    Detection.GetComponent<RoomType>().v_DestroyRoom();
                                    int randbot = Random.Range(2, 4);
                                    I_seed += randbot;
                                    path.Pop();
                                    S_GO_roomData.Pop();
                                    GameObject instance = Instantiate(G_C_A_rooms[randbot], transform.position, Quaternion.identity);
                                    path.Push(instance);
                                    S_GO_roomData.Push(instance);

                                }
                            }

                        }
                    }

                    // Vector2 V2_newPos = new Vector2(transform.position.x , transform.position.y - F_roomDIfferent);
                    transform.position = V2_newPos;


                    int randroom = Random.Range(1, G_C_A_rooms.Length);
                    I_seed += randroom;
                    if(randroom == 2)
                    {
                        randroom = 1;
                    }
                    GameObject instance2 = Instantiate(G_C_A_rooms[randroom], transform.position, Quaternion.identity);
                    path.Push(instance2);
                    S_GO_roomData.Push(instance2);

                    I_direction = Random.Range(1, 6);
                    I_roomcounter++;
                }


            }
        }
        else if (SpawnDirection == 2)//Left  to Right
        {

            if (I_direction == 1 || I_direction == 2)//Moving UP
            {
                Vector2 V2_newPos = new Vector2(transform.position.x, transform.position.y + F_roomDIfferent);
                Collider2D Detection = Physics2D.OverlapCircle(V2_newPos, 1, room);
                if (Detection == null)
                {
                    ++downcounter;
                    Detection = Physics2D.OverlapCircle(transform.position, 1, room);
                    if (Detection != null)
                    {

                        if (Detection.GetComponent<RoomType>().type != 2)
                        {
                            if (Detection.GetComponent<RoomType>().type != 3)
                            {
                                if (downcounter >= 2)
                                {
                                    Detection.GetComponent<RoomType>().v_DestroyRoom();
                                    path.Pop();
                                    S_GO_roomData.Pop();
                                    GameObject instance = Instantiate(G_C_A_rooms[3], transform.position, Quaternion.identity);
                                    S_GO_roomData.Push(instance);

                                    path.Push(instance);
                                    I_seed += 3;
                                }
                                else
                                {

                                    Detection.GetComponent<RoomType>().v_DestroyRoom();
                                    int randbot = Random.Range(2, 4);
                                    I_seed += randbot;
                                    path.Pop();
                                    S_GO_roomData.Pop();
                                    GameObject instance = Instantiate(G_C_A_rooms[randbot], transform.position, Quaternion.identity);
                                    path.Push(instance);
                                    S_GO_roomData.Push(instance);

                                }
                            }

                        }
                    }

                    // Vector2 V2_newPos = new Vector2(transform.position.x , transform.position.y - F_roomDIfferent);
                    transform.position = V2_newPos;


                    int randroom = Random.Range(1, G_C_A_rooms.Length);
                    I_seed += randroom;
                    if (randroom == 2)
                    {
                        randroom = 1;
                    }
                    GameObject instance2 = Instantiate(G_C_A_rooms[randroom], transform.position, Quaternion.identity);
                    path.Push(instance2);
                    S_GO_roomData.Push(instance2);

                    I_direction = Random.Range(1, 6);
                }
                else
                    I_direction = 5;
            }
            else if (I_direction == 3 || I_direction == 4)//Moving Down
            {
                Vector2 V2_newPos = new Vector2(transform.position.x, transform.position.y - F_roomDIfferent);
                Collider2D Detection = Physics2D.OverlapCircle(V2_newPos, 1, room);
                if (Detection == null)
                {
                
                    ++downcounter;
                    Detection = Physics2D.OverlapCircle(transform.position, 1, room);
                    if (Detection != null)
                    {

                        if (Detection.GetComponent<RoomType>().type != 1)
                        {
                            if (Detection.GetComponent<RoomType>().type != 3)
                            {
                                if (downcounter >= 2)
                                {
                                    Detection.GetComponent<RoomType>().v_DestroyRoom();
                                    path.Pop();
                                    S_GO_roomData.Pop();
                                    GameObject instance = Instantiate(G_C_A_rooms[3], transform.position, Quaternion.identity);
                                    S_GO_roomData.Push(instance);

                                    path.Push(instance);
                                    I_seed += 3;
                                }
                                else
                                {

                                    Detection.GetComponent<RoomType>().v_DestroyRoom();
                                    int randbot = Random.Range(1, 4);
                                    if (randbot == 2)
                                    {
                                        randbot = 1;
                                    }
                                    I_seed += randbot;
                                    path.Pop();
                                    S_GO_roomData.Pop();
                                    GameObject instance = Instantiate(G_C_A_rooms[randbot], transform.position, Quaternion.identity);
                                    path.Push(instance);
                                    S_GO_roomData.Push(instance);

                                }
                            }

                        }
                    }

                    // Vector2 V2_newPos = new Vector2(transform.position.x , transform.position.y - F_roomDIfferent);
                    transform.position = V2_newPos;


                    int randroom = Random.Range(2, G_C_A_rooms.Length);
                    I_seed += randroom;
                    GameObject instance2 = Instantiate(G_C_A_rooms[randroom], transform.position, Quaternion.identity);
                    path.Push(instance2);
                    S_GO_roomData.Push(instance2);

                    I_direction = Random.Range(1, 6);
                    I_roomcounter++;
                }
                else
                    I_direction = 5;

            }
            else if (I_direction == 5)//Moving Right
            {
                Vector2 V2_newPos = new Vector2(transform.position.x + F_roomDIfferent, transform.position.y);
                Collider2D Detection = Physics2D.OverlapCircle(V2_newPos, 1, room);
                if (Detection == null)
                {

                    transform.position = V2_newPos;

                    //Room choosing
                    int randroom = Random.Range(0, G_C_A_rooms.Length);
                    I_seed += randroom;
                    GameObject instance = Instantiate(G_C_A_rooms[randroom], transform.position, Quaternion.identity);

                    path.Push(instance);

                    S_GO_roomData.Push(instance);
                    //direction
                    I_direction = Random.Range(1, 6);
                    downcounter = 0;
                    I_roomcounter++;
                }


            }
        }
        else if (SpawnDirection == 3)//right  to left
        {

            if (I_direction == 1 || I_direction == 2)//Moving UP
            {
                Vector2 V2_newPos = new Vector2(transform.position.x, transform.position.y + F_roomDIfferent);
                Collider2D Detection = Physics2D.OverlapCircle(V2_newPos, 1, room);
                if (Detection == null)
                {
                    ++downcounter;
                    Detection = Physics2D.OverlapCircle(transform.position, 1, room);
                    if (Detection != null)
                    {

                        if (Detection.GetComponent<RoomType>().type != 2)
                        {
                            if (Detection.GetComponent<RoomType>().type != 3)
                            {
                                if (downcounter >= 2)
                                {
                                    Detection.GetComponent<RoomType>().v_DestroyRoom();
                                    path.Pop();
                                    S_GO_roomData.Pop();
                                    GameObject instance = Instantiate(G_C_A_rooms[3], transform.position, Quaternion.identity);
                                    S_GO_roomData.Push(instance);

                                    path.Push(instance);
                                    I_seed += 3;
                                }
                                else
                                {

                                    Detection.GetComponent<RoomType>().v_DestroyRoom();
                                    int randbot = Random.Range(2, 4);
                                    I_seed += randbot;
                                    path.Pop();
                                    S_GO_roomData.Pop();
                                    GameObject instance = Instantiate(G_C_A_rooms[randbot], transform.position, Quaternion.identity);
                                    path.Push(instance);
                                    S_GO_roomData.Push(instance);

                                }
                            }

                        }
                    }

                    // Vector2 V2_newPos = new Vector2(transform.position.x , transform.position.y - F_roomDIfferent);
                    transform.position = V2_newPos;


                    int randroom = Random.Range(1, G_C_A_rooms.Length);
                    I_seed += randroom;
                    if (randroom == 2)
                    {
                        randroom = 1;
                    }
                    GameObject instance2 = Instantiate(G_C_A_rooms[randroom], transform.position, Quaternion.identity);
                    path.Push(instance2);
                    S_GO_roomData.Push(instance2);

                    I_direction = Random.Range(1, 6);
                }
                else
                    I_direction = 5;
            }
            else if (I_direction == 3 || I_direction == 4)//Moving Down
            {
                Vector2 V2_newPos = new Vector2(transform.position.x, transform.position.y - F_roomDIfferent);
                Collider2D Detection = Physics2D.OverlapCircle(V2_newPos, 1, room);
                if (Detection == null)
                {

                    ++downcounter;
                    Detection = Physics2D.OverlapCircle(transform.position, 1, room);
                    if (Detection != null)
                    {

                        if (Detection.GetComponent<RoomType>().type != 1)
                        {
                            if (Detection.GetComponent<RoomType>().type != 3)
                            {
                                if (downcounter >= 2)
                                {
                                    Detection.GetComponent<RoomType>().v_DestroyRoom();
                                    path.Pop();
                                    S_GO_roomData.Pop();
                                    GameObject instance = Instantiate(G_C_A_rooms[3], transform.position, Quaternion.identity);
                                    S_GO_roomData.Push(instance);

                                    path.Push(instance);
                                    I_seed += 3;
                                }
                                else
                                {

                                    Detection.GetComponent<RoomType>().v_DestroyRoom();
                                    int randbot = Random.Range(1, 4);
                                    if (randbot == 2)
                                    {
                                        randbot = 1;
                                    }
                                    I_seed += randbot;
                                    path.Pop();
                                    S_GO_roomData.Pop();
                                    GameObject instance = Instantiate(G_C_A_rooms[randbot], transform.position, Quaternion.identity);
                                    path.Push(instance);
                                    S_GO_roomData.Push(instance);

                                }
                            }

                        }
                    }

                    // Vector2 V2_newPos = new Vector2(transform.position.x , transform.position.y - F_roomDIfferent);
                    transform.position = V2_newPos;


                    int randroom = Random.Range(2, G_C_A_rooms.Length);
                    I_seed += randroom;
                    GameObject instance2 = Instantiate(G_C_A_rooms[randroom], transform.position, Quaternion.identity);
                    path.Push(instance2);
                    S_GO_roomData.Push(instance2);

                    I_direction = Random.Range(1, 6);
                    I_roomcounter++;
                }
                else
                    I_direction = 5;

            }
            else if (I_direction == 5)//Moving Right
            {
                Vector2 V2_newPos = new Vector2(transform.position.x - F_roomDIfferent, transform.position.y);
                Collider2D Detection = Physics2D.OverlapCircle(V2_newPos, 1, room);
                if (Detection == null)
                {

                    transform.position = V2_newPos;

                    //Room choosing
                    int randroom = Random.Range(0, G_C_A_rooms.Length);
                    I_seed += randroom;
                    GameObject instance = Instantiate(G_C_A_rooms[randroom], transform.position, Quaternion.identity);

                    path.Push(instance);

                    S_GO_roomData.Push(instance);
                    //direction
                    I_direction = Random.Range(1, 6);
                    downcounter = 0;
                    I_roomcounter++;
                }


            }
        }
        if (I_roomcounter <= I_maxRoom)
            I_seed += I_direction;

    }
    public void fillRoom()
    {
        path.Pop();
        while (path.Count != 1)
        {
            GameObject temp = path.Peek();
            switch (temp.GetComponent<RoomType>().type)
            {
                case 0:
                    {

                        GameObject curr = new GameObject();
                        curr.transform.position = temp.transform.position;
                        Vector2 V2_newPos = new Vector2(curr.transform.position.x - F_roomDIfferent, curr.transform.position.y);
                        Collider2D Detection = Physics2D.OverlapCircle(V2_newPos, 1, room);
                        if (Detection == null)
                        {

                            int randroom = Random.Range(0, G_C_A_rooms.Length);
                            GameObject instance2 = Instantiate(DeadendRooms[3], V2_newPos, Quaternion.identity);
                            S_GO_roomData.Push(instance2);

                        }
                        V2_newPos = new Vector2(curr.transform.position.x + F_roomDIfferent, curr.transform.position.y);
                        Detection = Physics2D.OverlapCircle(V2_newPos, 1, room);
                        if (Detection == null)
                        {

                            int randroom = Random.Range(0, G_C_A_rooms.Length);
                            GameObject instance2 = Instantiate(DeadendRooms[2], V2_newPos, Quaternion.identity);
                            S_GO_roomData.Push(instance2);

                        }
                    }
                    break;
                case 1:
                    {

                        GameObject curr = new GameObject();
                        curr.transform.position = temp.transform.position;
                        Vector2 V2_newPos = new Vector2(curr.transform.position.x - F_roomDIfferent, curr.transform.position.y);
                        Collider2D Detection = Physics2D.OverlapCircle(V2_newPos, 1, room);
                        if (Detection == null)
                        {

                            int randroom = Random.Range(0, G_C_A_rooms.Length);
                            GameObject instance2 = Instantiate(DeadendRooms[3], V2_newPos, Quaternion.identity);
                            S_GO_roomData.Push(instance2);

                        }
                        V2_newPos = new Vector2(curr.transform.position.x + F_roomDIfferent, curr.transform.position.y);
                        Detection = Physics2D.OverlapCircle(V2_newPos, 1, room);
                        if (Detection == null)
                        {

                            int randroom = Random.Range(0, G_C_A_rooms.Length);
                            GameObject instance2 = Instantiate(DeadendRooms[2], V2_newPos, Quaternion.identity);
                            S_GO_roomData.Push(instance2);

                        }
                        V2_newPos = new Vector2(curr.transform.position.x, curr.transform.position.y - F_roomDIfferent);
                        Detection = Physics2D.OverlapCircle(V2_newPos, 1, room);
                        if (Detection == null)
                        {

                            int randroom = Random.Range(0, G_C_A_rooms.Length);
                            GameObject instance2 = Instantiate(DeadendRooms[0], V2_newPos, Quaternion.identity);
                            S_GO_roomData.Push(instance2);

                        }
                    }

                    break;
                case 2:
                    {
                        GameObject curr = new GameObject();
                        curr.transform.position = temp.transform.position;
                        Vector2 V2_newPos = new Vector2(curr.transform.position.x - F_roomDIfferent, curr.transform.position.y);
                        Collider2D Detection = Physics2D.OverlapCircle(V2_newPos, 1, room);
                        if (Detection == null)
                        {

                            int randroom = Random.Range(0, G_C_A_rooms.Length);
                            GameObject instance2 = Instantiate(DeadendRooms[3], V2_newPos, Quaternion.identity);
                            S_GO_roomData.Push(instance2);

                        }
                        V2_newPos = new Vector2(curr.transform.position.x + F_roomDIfferent, curr.transform.position.y);
                        Detection = Physics2D.OverlapCircle(V2_newPos, 1, room);
                        if (Detection == null)
                        {

                            int randroom = Random.Range(0, G_C_A_rooms.Length);
                            GameObject instance2 = Instantiate(DeadendRooms[2], V2_newPos, Quaternion.identity);
                            S_GO_roomData.Push(instance2);

                        }
                        V2_newPos = new Vector2(curr.transform.position.x, curr.transform.position.y + F_roomDIfferent);
                        Detection = Physics2D.OverlapCircle(V2_newPos, 1, room);
                        if (Detection == null)
                        {

                            int randroom = Random.Range(0, G_C_A_rooms.Length);
                            GameObject instance2 = Instantiate(DeadendRooms[1], V2_newPos, Quaternion.identity);
                            S_GO_roomData.Push(instance2);

                        }
                    }
                    break;
                case 3:
                    {
                        GameObject curr = new GameObject();
                        curr.transform.position = temp.transform.position;
                        Vector2 V2_newPos = new Vector2(curr.transform.position.x - F_roomDIfferent, curr.transform.position.y);
                        Collider2D Detection = Physics2D.OverlapCircle(V2_newPos, 1, room);
                        if (Detection == null)
                        {

                            GameObject instance2 = Instantiate(DeadendRooms[3], V2_newPos, Quaternion.identity);
                            S_GO_roomData.Push(instance2);

                        }
                        V2_newPos = new Vector2(curr.transform.position.x + F_roomDIfferent, curr.transform.position.y);
                        Detection = Physics2D.OverlapCircle(V2_newPos, 1, room);
                        if (Detection == null)
                        {

                            int randroom = Random.Range(0, G_C_A_rooms.Length);
                            GameObject instance2 = Instantiate(DeadendRooms[2], V2_newPos, Quaternion.identity);
                            S_GO_roomData.Push(instance2);

                        }
                        V2_newPos = new Vector2(curr.transform.position.x, curr.transform.position.y + F_roomDIfferent);
                        Detection = Physics2D.OverlapCircle(V2_newPos, 1, room);
                        if (Detection == null)
                        {

                            int randroom = Random.Range(0, G_C_A_rooms.Length);
                            GameObject instance2 = Instantiate(DeadendRooms[1], V2_newPos, Quaternion.identity);
                            S_GO_roomData.Push(instance2);

                        }
                        V2_newPos = new Vector2(curr.transform.position.x, curr.transform.position.y - F_roomDIfferent);
                        Detection = Physics2D.OverlapCircle(V2_newPos, 1, room);
                        if (Detection == null)
                        {

                            int randroom = Random.Range(0, G_C_A_rooms.Length);
                            GameObject instance2 = Instantiate(DeadendRooms[0], V2_newPos, Quaternion.identity);
                            S_GO_roomData.Push(instance2);

                        }
                    }
                    break;
            }
            path.Pop();
            
        }

        while (S_GO_roomData.Count != 0)
        {
            GameObject temp = S_GO_roomData.Peek();
            L_V2_roomPosition.Add(temp.transform.position);
            L_V2_roomtype.Add(temp.GetComponent<RoomType>().type);
            S_GO_roomData.Pop();
        }
    }

}

