using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPathfindingMovementHandler : MonoBehaviour {

    private const float speed = 10f;
    private int currentPathIndex = 0;
    private List<Vector3> pathVectorList;
    private PathFinding pathFinding;
    public Transform target;
    public Vector3 prevTarget;

    private void Start() {
            target = GameObject.Find("Player").transform;
            prevTarget = new Vector2(target.position.x, target.position.y);
            if(target == null){
                Debug.Log("target not found");
            }
            else{
                Debug.Log("target found!");
            }
            pathFinding = new PathFinding(26, 21);
            SetTargetPosition(target.position);
    }

    private void Update() {
            HandleMovement();
            if(Vector3.Distance(prevTarget,target.position) >= 1f){
                pathVectorList = null;
                SetTargetPosition(target.position);
                prevTarget = new Vector2(target.position.x, target.position.y);
            }
    }
    
    private void HandleMovement() {
        if (pathVectorList != null) {
            Vector3 targetPosition = pathVectorList[currentPathIndex];
            
            if (Vector3.Distance(transform.position, targetPosition) > 1f) {
                Vector3 moveDir = (targetPosition - transform.position).normalized;
                transform.position = transform.position + moveDir * speed * Time.deltaTime;
            } else {
                currentPathIndex++;
                if (currentPathIndex >= pathVectorList.Count) {
                    pathVectorList = null;
                }
            }
        }
        else{
            Debug.Log("path not found");
        }
    }

    public void SetTargetPosition(Vector3 targetPosition) {
        currentPathIndex = 0;
        pathVectorList = pathFinding.FindPath(transform.position, targetPosition);

        if (pathVectorList != null && pathVectorList.Count > 1) {
            //Debug.Log("removing node from path " + currentPathIndex + " " + pathVectorList[currentPathIndex]);
            pathVectorList.RemoveAt(0);
        }
    }

}