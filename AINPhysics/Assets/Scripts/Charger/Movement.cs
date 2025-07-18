using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{


    public NavMeshAgent chargerAgent;

    public GameObject chargerSelf;


    public float PatrolRange;
    public float chargerSpeed;


    public bool foundPlayer;




    public Transform patrolCenter;
    public Vector3 playerDestination;


    private void Start()
    {

        chargerAgent = GetComponent<NavMeshAgent>();

       // chargerSelf = GetComponent<GameObject>();


    }



    private void Update()
    {


        if (foundPlayer == false && chargerAgent.remainingDistance <= chargerAgent.stoppingDistance)
        {

            Vector3 point;
            if(RandomPoint(patrolCenter.position, PatrolRange, out point))
            {

                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                chargerAgent.SetDestination(point);
                

            }

        }
        else if (foundPlayer == true)
        {


            chargerAgent.SetDestination(playerDestination);



        }
    }



    bool RandomPoint(Vector3 center, float PatrolRange, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * PatrolRange; 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) 
        {
            
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;

    }



    private void OnTriggerEnter(Collider other)
    {
        
        if(other.CompareTag("Player"))
        {

            playerDestination = other.transform.position;

            Debug.Log("Seen Player");

            foundPlayer = true;

            chargerSpeed = 10.0f;

            chargerAgent.speed = chargerSpeed;

           // chargerSelf.transform.LookAt(other.transform);


        }


    }

    private void OnTriggerExit(Collider other)
    {
        
        if(other.CompareTag("Player"))
        {

            foundPlayer = false;


        }






    }

}
