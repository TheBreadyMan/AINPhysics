using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{


    public NavMeshAgent chargerAgent;

    public GameObject chargerSelf;
    public GameObject playerBody;


    public float PatrolRange;
    public float chargerSpeed;

    public bool touchingPlayer;

    public Transform patrolCenter;
    public Vector3 playerDestination;


    private void Start()
    {

        chargerAgent = GetComponent<NavMeshAgent>();

        playerBody = GameObject.Find("Player");

       // chargerSelf = GetComponent<GameObject>();


    }



    private void Update()
    {

        if (touchingPlayer == false)
        {
            if (playerinFront() && chargerLineOfSight())
            {


                playerDestination = playerBody.transform.position;



                chargerAgent.SetDestination(playerDestination);

            }
            else
            {

                if (chargerAgent.remainingDistance <= chargerAgent.stoppingDistance)
                {
                    Vector3 point;
                    if (RandomPoint(patrolCenter.position, PatrolRange, out point))
                    {

                        Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                        chargerAgent.SetDestination(point);


                    }
                }


            }
        }



            playerinFront();
        chargerLineOfSight();

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


    bool playerinFront()
    {

        Vector3 directionofPlayer = transform.position - playerBody.transform.position;
        float angle = Vector3.Angle(transform.forward, directionofPlayer);

        if(Mathf.Abs(angle) > 90 && Mathf.Abs(angle) < 270)
        {
            Debug.DrawLine(transform.position, playerBody.transform.position, Color.red);
            return true;
        }
        return false;

    }

    bool chargerLineOfSight()
    {

        RaycastHit _hit;
        Vector3 directionOfPlayer =  playerBody.transform.position - transform.position;

        if(Physics.Raycast(transform.position, directionOfPlayer, out _hit, 50000f))
        {

            if(_hit.transform.tag == "Player")
            {

                Debug.DrawLine(transform.position, playerBody.transform.position, Color.green);
                return true;

            }

        }

        return false;


    }



    private void OnTriggerEnter(Collider other)
    {

        if(other.CompareTag("Player"))
        {

            touchingPlayer = true;


        }

        


    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {

            touchingPlayer = false;


        }



    }

}
