using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    OptionsMenu options;

    public GameObject sheep;
    public int numSheep = 5;
    public GameObject[] allSheep;
    public Vector3 WanderLimits = new Vector3(20, 0, 10);
    int PenCount = 0;
    public bool win = false;
    public List<List<GameObject>> Flocks;
    List<List<GameObject>> Union;
    
    public GameObject cam;
    public List<GameObject> Temp;
    float changeDirTime;
    public int flockNum;

    public Vector3 goalPos = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        allSheep = new GameObject[numSheep];
        Flocks = new List<List<GameObject>>();
        Union = new List<List<GameObject>>();
        for (int i = 0; i < numSheep; i++)
        {
            Vector3 pos = transform.position + new Vector3(Random.Range(-WanderLimits.x/2, WanderLimits.x), 2, Random.Range(-WanderLimits.z, WanderLimits.z));
            allSheep[i] = Instantiate(sheep, pos, Quaternion.identity);
            allSheep[i].GetComponent<SheepTasks>().myManager = this;
        }
        Time.timeScale = 1f;

        if (PlayerPrefs.GetInt("CameraIndex") == 0)
        {
            cam.SetActive(true);

        }
        else if (PlayerPrefs.GetInt("CameraIndex") == 1)
        {
            cam.SetActive(false);

        }

       
        

    }

    // Update is called once per frame
    void Update()
    {
        PenCount = 0;
        foreach (GameObject go in allSheep)
        {
            if (GameObject.FindGameObjectWithTag("PenTrigger").GetComponent<BoxCollider>().bounds.Contains(go.transform.position))
            {
                PenCount++;
            }
        }

        cam.GetComponent<Camera>().fieldOfView = PlayerPrefs.GetFloat("FOV", 60);

        if (PenCount == allSheep.Length)
        {
            win = true;
        }

        ManageFlocks();

        //changeDirTime -= Time.deltaTime;
        for (int i = 0; i < Flocks.Count(); i++)
        {

            //if (changeDirTime <= 0)
            //{
            int imp = 0;
            foreach (GameObject sheep in Flocks[i])
            {

                sheep.GetComponent<SheepTasks>().isLeader = false;
                sheep.GetComponent<SheepTasks>().importance = imp;
                imp++;

            }
            //flock[0].GetComponent<SheepTasks>().direction = new Vector3(Random.Range(-WanderLimits.x, WanderLimits.x), 1, Random.Range(-WanderLimits.z, WanderLimits.z));
            Flocks[i].First().GetComponent<SheepTasks>().isLeader = true;

            //changeDirTime += 10;
            //}
        }

    }
    void ManageFlocks()
    {
        Flocks.Clear();
        //for every sheep
        foreach (GameObject go in allSheep)
        {
            //check if its flocked

            //check each flock
            //if(Flocks == null)
            //{
            if (go.GetComponent<SheepTasks>().flocked == true)
            {
                Temp.Clear();

                Temp.AddRange(go.GetComponent<SheepTasks>().Neighbours);


                Temp.Add(go);
                Flocks.Add(Temp);
            }


            ///////////////////////////////////////////OLD STUFF KEEPING JUST IN CASE////////////////////////////////////////////////////////////////     
            //}
            //else
            //{
            //    for(int i = 0; i < Flocks.Count;i++)
            //    {
            //        //if the flock contains the sheep
            //        if(Flocks[i].Contains(go))
            //        {
            //            //check each of the sheeps neighbours
            //            foreach(GameObject neighbour in go.GetComponent<SheepTasks>().Neighbours)
            //            {
            //                bool contains = false;
            //                //against the sheep currently in the flock
            //                foreach(GameObject sheep in Flocks[i])
            //                {
            //                    //if it is in the flock set contains to true
            //                    if(neighbour == sheep)
            //                    {
            //                        contains = true;
            //                    }
            //                }
            //                //if contains is false
            //                if(contains == false)
            //                {
            //                    //add the sheep to the flock
            //                    Flocks[i].Add(neighbour);
            //                }
            //            }
            //        }
            //        // otherwise make a new flock and add its neighbours to the flock
            //        else
            //        {
            //            Flocks.Add(new List<GameObject>());

            //                Flocks[Flocks.Count - 1].AddRange(go.GetComponent<SheepTasks>().Neighbours);

            //        }
            //    }     
            //}
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        }
        //check and merge any flocks that share sheep
        bool merge = false;
        int countingMerges = 0;
        if (Flocks != null)
        {
            Union.Clear();
            for (int i = Flocks.Count() - 1; i > -1; i--)
            {
               for (int f = Flocks.Count() - 1; f > -1; f--)
               {
                   merge = false;
                    if (i < f)
                    {
                        if (countingMerges < Flocks.Count())
                        {
                            if (Flocks[i].Intersect(Flocks[f]).Count() > 0)
                            {
                                merge = true;
                                countingMerges++;
                            }
                            if (merge == true)
                            {
                                IEnumerable<GameObject> FU = Flocks[i].Union(Flocks[f]);
                                Union.Add(FU.ToList());
                            }
                        }
                    }
               }
            }
            Flocks.Clear();
            for (int i = 0; i < Union.Count(); i++)
            {
                merge = false;
                for (int f = 0; f < Union.Count(); f++)
                {
                    if (Union[i].Intersect(Union[f]).Count() > 0 && merge == false)
                    {
                        merge = true;
                        IEnumerable<GameObject> fu = Union[i].Union(Union[f]);
                        Flocks.Add(fu.ToList());
                    }
                }
                if (merge == false)
                {
                    Flocks.Add(Union[i]);
                }
            }
            flockNum = Flocks.Count();
        }
    }
}

