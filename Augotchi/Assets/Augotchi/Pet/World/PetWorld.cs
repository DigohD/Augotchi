using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetWorld : MonoBehaviour {

    public Animator anim;

    GameObject player;

    private int animState = 2;

    private enum PetWorldState { IDLE_SIT, IDLE_STAND, WALK_TARGET }
    private PetWorldState pws = PetWorldState.WALK_TARGET;

    public GameObject petTarget;

    private float speed = 5f;
    private float maxRot = 2f;

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void Update () {
        switch (pws)
        {
            case PetWorldState.IDLE_SIT:
                updateIdle();
                break;
            case PetWorldState.IDLE_STAND:
                updateIdle();
                break;
            case PetWorldState.WALK_TARGET:
                updateWalk();
                break;
        }
	}

    void updateIdle()
    {
        if ((pws == PetWorldState.IDLE_SIT || pws == PetWorldState.IDLE_STAND) && Random.Range(0, 1000) < 3)
        {
            goToTarget();
        }

        float playerDistance = (player.transform.position - transform.position).magnitude;
        if (playerDistance > 40f)
        {
            goToTarget();
        }
    }

    void updateWalk()
    {
        float targetPlayerdistance = (player.transform.position - petTarget.transform.position).magnitude;
        if (targetPlayerdistance > 40f)
        {
            goToTarget();
        }

        Vector3 diff = petTarget.transform.position - transform.position;
        diff.Normalize();
        float rot_y = Mathf.Atan2(diff.z, diff.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, -rot_y + 90, 0);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, maxRot);

        GameObject map = GameObject.FindGameObjectWithTag("Map");
        float scaleConversion = map.transform.localScale.x;

        transform.Translate(0, 0, speed * scaleConversion * Time.deltaTime);

        float targetDistance = (petTarget.transform.position - transform.position).magnitude;
        if (targetDistance < 2f * scaleConversion)
        {
            if(Random.Range(0, 5) < 2)
            {
                pws = PetWorldState.IDLE_SIT;
                anim.SetInteger("State", 0);
                return;
            }
            else
            {
                pws = PetWorldState.IDLE_STAND;
                anim.SetInteger("State", 1);
                return;
            }
        }

        anim.SetInteger("State", 2);
    }

    private void goToTarget()
    {
        GameObject map = GameObject.FindGameObjectWithTag("Map");
        float scaleConversion = map.transform.localScale.x;

        Vector3 offsetDirection = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));
        Vector3 newTarget = player.transform.position + (offsetDirection * Random.Range(15.0f * scaleConversion, 25f * scaleConversion));
        petTarget.transform.position = newTarget;

        pws = PetWorldState.WALK_TARGET;
    }
}
