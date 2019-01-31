using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;

public class SheepTasks : MonoBehaviour {
    public FlockManager myManager;
    GameObject[] gos;
    float speed = 0.3f;
    float rotationSpeed = 2.0f;
    public Vector3 goalPos;
    public bool isLeader;
    int neighbourCount;
    float cohesion = 5;
    public Vector3 direction;
    public int importance;
    public bool flocked = false;
    //bool isEating = false;
    float distsheep;
    Vector3 Dir;
    float time;
    public List<GameObject> Neighbours;
    public bool Penned = false;

    

    //public Transform target;
    [Task]
    void InPen()
    {
        //if (!Penned)
        //{
        //    Penned = true;
        //    goalPos = new Vector3(Random.Range(GameObject.FindGameObjectWithTag("PenTrigger").GetComponent<BoxCollider>().bounds.center.x - (GameObject.FindGameObjectWithTag("PenTrigger").GetComponent<BoxCollider>().bounds.size.x / 2), GameObject.FindGameObjectWithTag("PenTrigger").GetComponent<BoxCollider>().bounds.center.x + (GameObject.FindGameObjectWithTag("PenTrigger").GetComponent<BoxCollider>().bounds.size.x / 2)), 1, Random.Range(GameObject.FindGameObjectWithTag("PenTrigger").GetComponent<BoxCollider>().bounds.center.z - (GameObject.FindGameObjectWithTag("PenTrigger").GetComponent<BoxCollider>().bounds.size.z / 2), GameObject.FindGameObjectWithTag("PenTrigger").GetComponent<BoxCollider>().bounds.center.z + (GameObject.FindGameObjectWithTag("PenTrigger").GetComponent<BoxCollider>().bounds.size.z / 2)));
        //}
        speed = 0.7f;


        if (GameObject.FindGameObjectWithTag("PenTrigger").GetComponent<BoxCollider>().bounds.Contains(transform.position))
        {
            if (!Penned)
            {
                Penned = true;
                goalPos = new Vector3(Random.Range(GameObject.FindGameObjectWithTag("PenTrigger").GetComponent<BoxCollider>().bounds.center.x - (GameObject.FindGameObjectWithTag("PenTrigger").GetComponent<BoxCollider>().bounds.size.x / 2), GameObject.FindGameObjectWithTag("PenTrigger").GetComponent<BoxCollider>().bounds.center.x + (GameObject.FindGameObjectWithTag("PenTrigger").GetComponent<BoxCollider>().bounds.size.x / 2)), 1, Random.Range(GameObject.FindGameObjectWithTag("PenTrigger").GetComponent<BoxCollider>().bounds.center.z - (GameObject.FindGameObjectWithTag("PenTrigger").GetComponent<BoxCollider>().bounds.size.z / 2), GameObject.FindGameObjectWithTag("PenTrigger").GetComponent<BoxCollider>().bounds.center.z + (GameObject.FindGameObjectWithTag("PenTrigger").GetComponent<BoxCollider>().bounds.size.z / 2)));
            }
            if (Random.Range(0, 500) < 1)
            {
                goalPos = new Vector3(Random.Range(GameObject.FindGameObjectWithTag("PenTrigger").GetComponent<BoxCollider>().bounds.center.x - (GameObject.FindGameObjectWithTag("PenTrigger").GetComponent<BoxCollider>().bounds.size.x/2), GameObject.FindGameObjectWithTag("PenTrigger").GetComponent<BoxCollider>().bounds.center.x + (GameObject.FindGameObjectWithTag("PenTrigger").GetComponent<BoxCollider>().bounds.size.x/2)), 1, Random.Range(GameObject.FindGameObjectWithTag("PenTrigger").GetComponent<BoxCollider>().bounds.center.z - (GameObject.FindGameObjectWithTag("PenTrigger").GetComponent<BoxCollider>().bounds.size.z/2), GameObject.FindGameObjectWithTag("PenTrigger").GetComponent<BoxCollider>().bounds.center.z + (GameObject.FindGameObjectWithTag("PenTrigger").GetComponent<BoxCollider>().bounds.size.z/2)));

            }

            Vector3 Dir = goalPos - transform.position;
            

            Dir.y = 0;
            float goalcheck = Vector3.Distance(transform.position, goalPos);



            if (goalcheck < 1.6)
            {
                GetComponent<Animator>().SetBool("Moving", false);
                Task.current.Succeed();
                

            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Dir), rotationSpeed * Time.deltaTime);
                transform.Translate(0, 0, Time.deltaTime * speed);
                GetComponent<Animator>().SetBool("Moving", true);

                Task.current.Succeed();
            }
        }
        else
        {
            Task.current.Fail();
        }

    }
    [Task]
    void Calm()
    {
       
        float dist = Vector3.Distance(GameObject.FindGameObjectWithTag("Dog").transform.position, transform.position);
        if (dist < 5)
        {
            Task.current.Fail();
        }
        else
        {
        flocked = false;
            Neighbours.Clear();
            gos = myManager.allSheep;
            //foreach (Gameobject go in gos)
            //{
            //    float dist = Vector3.Distance(target.position, go.position);
            //    if (dist < 5)
            //    {
            //        Task.current.Fail();
            //    }
            //}

            speed = Random.Range(0.3f,0.7f);
       
        //Task.current.debugInfo = string.Format("t={0:0.00}", dist);

        //Vector3 pos = GameObject.FindGameObjectWithTag("Player").transform.position;
        //Task.current.debugInfo = string.Format("t={0:0.00}", pos);
        Cohesion();
        if (isLeader == true || flocked == false)
        {
            if (Random.Range(0, 500) < 1)
            {
                goalPos = new Vector3(Random.Range(-myManager.WanderLimits.x/2, myManager.WanderLimits.x), 1, Random.Range(-myManager.WanderLimits.z, myManager.WanderLimits.z));

            }
        }
            //neighbourCount = 0;
            //foreach (GameObject go in gos)
            //{
            //    if (go != gameObject)
            //    {
            //        distsheep = Vector3.Distance(go.transform.position, transform.position);

            //        if (distsheep <= cohesion)
            //        {

            //            direction = transform.rotation.eulerAngles + go.transform.rotation.eulerAngles;
            //            neighbourCount++;
            //        }
            //    }
            //}
            ////Task.current.debugInfo = string.Format("t={0:0.00}", neighbourCount);
            //if (neighbourCount != 0)
            //{
            //    //direction = transform.rotation.eulerAngles;
            //    direction = direction / neighbourCount;
            //    flocked = true;
            //    //transform.rotation = Quaternion.Euler(direction);
            //}
            //Task.current.debugInfo = string.Format("t={0:0.00}", flocked);
            //if (Random.Range(0, 500) < 1)
            //{
            //    goalPos = new Vector3(Random.Range(-myManager.WanderLimits.x, myManager.WanderLimits.x), 1, Random.Range(-myManager.WanderLimits.z, myManager.WanderLimits.z));

            //}

            //Vector3 Dir = goalPos - transform.position;
            //Dir = (Dir + direction) / 2;

            //Dir.y = 0;
            //float goalcheck = Vector3.Distance(transform.position, goalPos);

            if (isLeader && flocked)
            {
                direction += ((1.5f * (Alignment() * 3) + (Cohesion() * 4) + (Seperation() * 5)) + ((goalPos - transform.position) * 0.35f));
                direction.Normalize();
            }
            else if (isLeader && !flocked)
            {
                direction = goalPos - transform.position;
                direction.Normalize();
            }
            else
            {
                direction += (Alignment() * 2) + (Cohesion() * 5) + (Seperation() * 4);
                direction.Normalize();
            }



            if (transform.position.x > myManager.WanderLimits.x)
            {
                goalPos = new Vector3(0,0,0);
            }
            if (transform.position.z > myManager.WanderLimits.z)
            {
                goalPos = new Vector3(0, 0, 0);
            }


            float goalcheck = Vector3.Distance(transform.position, goalPos);
            direction.y = 0;

            if (goalcheck < 1.6)
            {
                Task.current.Succeed();
                GetComponent<Animator>().SetBool("Moving", false);
            }
            else
            {
                //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Dir), rotationSpeed * Time.deltaTime);
                //transform.Translate(0, 0, Time.deltaTime * speed);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
                transform.Translate(0, 0, Time.deltaTime * speed);
                GetComponent<Animator>().SetBool("Moving", true);

                Task.current.Succeed();
            }
            
        }

       
        
    }

    [Task]
    void Alert()
    {
        if(Vector3.Distance(GameObject.FindGameObjectWithTag("Dog").transform.position, transform.position) > 5)
        {
            GetComponent<Animator>().SetBool("Moving", true);
            Task.current.Fail();
        }
        else if (Vector3.Distance(GameObject.FindGameObjectWithTag("Dog").transform.position, transform.position) < 4)
        {
            GetComponent<Animator>().SetBool("Moving", true);
            Task.current.Fail();
        }
        else
        {
            GetComponent<Animator>().SetBool("Moving", false);
            Task.current.Succeed();
        }
       
        
    }

    [Task]
    void Fleeing()
    {
        if(Vector3.Distance(GameObject.FindGameObjectWithTag("Dog").transform.position, transform.position) > 4)
        {
            GetComponent<Animator>().SetBool("Moving", false);
            Task.current.Fail();
        }

        if (Vector3.Distance(GameObject.FindGameObjectWithTag("Dog").transform.position, transform.position) < 4)
        {
            speed = Random.Range(1f, 1.5f);
        }

        if (Vector3.Distance(GameObject.FindGameObjectWithTag("Dog").transform.position, transform.position) < 3)
        {
            speed = Random.Range(1.5f, 2f);
        }

        if (Vector3.Distance(GameObject.FindGameObjectWithTag("Dog").transform.position, transform.position) < 1.5)
        {
            speed = 2.5f;
        }
        Vector3 Dir = transform.position - GameObject.FindGameObjectWithTag("Dog").transform.position;
        direction += ((0.5f * (Alignment() * 2) + (Cohesion() * 5) + (Seperation() * 4)) + (Dir * 2f));
        direction.Normalize();
        
        direction.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
        transform.Translate(0, 0, Time.deltaTime * speed);
        GetComponent<Animator>().SetBool("Moving", true);
        Task.current.Succeed();

    }
    Vector3 Alignment()
    {
        Vector3 A = new Vector3(0, 0, 0);
        neighbourCount = 0;
        Neighbours.Clear();
        gos = myManager.allSheep;
        foreach (GameObject go in gos)
        {
            if (go != gameObject)
            {
                distsheep = Vector3.Distance(go.transform.position, transform.position);
                if (go.GetComponent<SheepTasks>().Penned == false)
                {
                    if (distsheep <= cohesion)
                    {
                        if (!isLeader)
                        {
                                if (go.GetComponent<SheepTasks>().isLeader == true)
                                {
                                    A = (transform.rotation.eulerAngles + go.transform.rotation.eulerAngles) + go.transform.rotation.eulerAngles + go.transform.rotation.eulerAngles;
                                    Debug.Log(isLeader);
                                    neighbourCount++;
                                }
                                else
                                {
                                    A = (transform.rotation.eulerAngles + go.transform.rotation.eulerAngles);
                                    neighbourCount++;
                                }
                        }
                        Neighbours.Add(go);
                        flocked = true;
                    }
                }
            }
        }
        if (neighbourCount != 0)
        {
            A = A / (neighbourCount + 1);
            A.Normalize();
            A.y = 0;
        }
        return A;
    }
    Vector3 Cohesion()
    {
        Vector3 A = new Vector3(0, 0, 0);
        neighbourCount = 0;
        foreach (GameObject go in gos)
        {
            if (go != gameObject)
            {
                distsheep = Vector3.Distance(go.transform.position, transform.position);
                if (go.GetComponent<SheepTasks>().Penned == false)
                {
                    if (distsheep <= cohesion)
                    {
                        if (!isLeader)
                        {
                            if (go.GetComponent<SheepTasks>().isLeader == true)
                            {
                                A += go.transform.position;
                                A += go.transform.position;
                                neighbourCount++;
                            }
                            else
                            {
                                A += go.transform.position;
                                neighbourCount++;
                            }
                        }
                        else
                        {
                            A += go.transform.position;
                            neighbourCount++;
                        }
                        flocked = true;
                    }
                } 
            }
        }
        if (neighbourCount != 0)
        {
            A = A / (neighbourCount + 1);
            A = A - transform.position;
            A.Normalize();
            A.y = 0;
            flocked = true;
        }
        return A;
    }
    Vector3 Seperation()
    {
        Vector3 A = new Vector3(0, 0, 0);
        neighbourCount = 0;
        foreach (GameObject go in gos)
        {
            if (go != gameObject)
            {
                distsheep = Vector3.Distance(go.transform.position, transform.position);
                if (go.GetComponent<SheepTasks>().Penned == false)
                {
                    if (distsheep <= (cohesion / 4))
                    {
                        A += (go.transform.position - transform.position);
                        neighbourCount++;
                        flocked = true;
                    }
                }
            }
        }
        if (neighbourCount != 0)
        {
            A = A / (neighbourCount + 1);
            A = A - transform.position;
            A *= -1;
            A.Normalize();
            A.y = 0;
            flocked = true;
        }
        return A;
    }
}
