using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ActionType
{
    PlayAnimation,
    GoToPoint
}

[System.Serializable]
public class ActionData
{
    public float timeUntilThisAction;
    public ActionType actionType;
    public string animationName; // For PlayAnimation actions
    public Vector3 desiredPosition; // For GoToPoint actions
}

public class Puppet : MonoBehaviour
{
    public GameObject objectToPuppet;
    public List<ActionData> actions = new List<ActionData>();

    void Update()
    {
        foreach (var action in actions)
        {
            if (action.timeUntilThisAction <= 0f)
            {
                switch (action.actionType)
                {
                    case ActionType.PlayAnimation:
                        // Get the Animator component and play the specified animation
                        Animator animator = objectToPuppet.GetComponent<Animator>();
                        if (animator != null && !string.IsNullOrEmpty(action.animationName))
                        {
                            animator.Play(action.animationName);
                            Debug.Log($"Playing animation: {action.animationName}");
                        }
                        break;
                    case ActionType.GoToPoint:
                        // Use NavMeshAgent to move to the desired position
                        NavMeshAgent agent = objectToPuppet.GetComponent<NavMeshAgent>();
                        if (agent != null)
                        {
                            agent.SetDestination(action.desiredPosition);
                            Debug.Log($"Moving to position: {action.desiredPosition}");
                        }
                        break;
                }
                // Remove the action once it has been executed
                actions.Remove(action);
            }
            else
            {
                // Decrement the time until this action
                action.timeUntilThisAction -= Time.deltaTime;
            }
        }
    }
}