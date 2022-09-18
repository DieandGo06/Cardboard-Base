using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    float posY;

    private void Start()
    {
        posY = transform.position.y;
    }

    private void Update()
    {
        Vector3 position = new Vector3(player.position.x, posY, player.position.z);
        transform.position = position;
    }
}
