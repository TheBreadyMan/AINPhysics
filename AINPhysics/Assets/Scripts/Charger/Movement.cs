using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{


    public NavMeshAgent chargerAgent;


    public float PatrolRange;


    public bool foundPlayer;


    public Transform patrolCenter;


    private void Start()
    {

        chargerAgent = GetComponent<NavMeshAgent>();


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
    }



    bool RandomPoint(Vector3 center, float PatrolRange, out Vector3 result)
    {



        Vector3 randomPoint = center + Random.insideUnitSphere * PatrolRange; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
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




        }



    }









}
