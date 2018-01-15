using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolV_1_0 : MonoBehaviour {
    public List< Control> mycontrol = new List<Control>();
    // Use this for initialization
    void Start () {

    }


   public void CreateObject(string Name) {
        for (int i = 0; i < mycontrol.Count; i++)
        {
            if (Name == mycontrol[i].Name) {
                if (mycontrol[i].Pool.Count == 0)
                {
                    GameObject g = Instantiate(mycontrol[i].ObjectToCreate);
                    g.name = mycontrol[i].Name;
                    g.transform.position = RandomPos(5, 5, 5);
                    Debug.Log("CreateObject" + g.name);
                }
                else {
                    mycontrol[i].Pool.Peek().SetActive(true);
                    mycontrol[i].Pool.Peek().transform.parent = null;
                    mycontrol[i].Pool.Peek().transform.position = RandomPos(5, 5, 5);
                    Debug.Log("Shoing Object Again" + mycontrol[i].Pool.Peek().name);
                    mycontrol[i].Pool.Pop();
                }
            }
        }


    }

   public void CollectObject(string Name) {
        GameObject[] g = FindObjectsOfType< GameObject>();
        for (int i = 0; i < g.Length; i++)
        {
            if (Name == g[i].name)
            {
                for (int j = 0; j < mycontrol.Count; j++)
                {
                    if (g[i].name == mycontrol[j].Name)
                    {
                        g[i].transform.position =  Vector3.zero;
                        g[i].transform.parent = mycontrol[j].Location;
                        mycontrol[j].Pool.Push(g[i]);
                        g[i].SetActive(false);
                    }
                }
                Debug.Log("CollectObject"+ g[i].name);
                return;            
            }
        }
    }

	// Update is called once per frame
	void Update () {
    }

    public static Vector3 RandomPos(float _x,float _y,float _z) {
        float x = Random.Range(-_x, _x);
        float y = Random.Range(-_y, _y);
        float z = Random.Range(-_z, _z);
        return new Vector3(x, y, z);
    }

    [System.Serializable]
    public class Control {
        public string Name;
        public GameObject ObjectToCreate;
        public Transform Location;
        public Stack<GameObject> Pool = new Stack<GameObject>();

    }
}

