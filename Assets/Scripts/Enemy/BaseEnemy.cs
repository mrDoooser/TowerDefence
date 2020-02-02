using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : AbstractEnemy
{

    private void Start()
    {
        if (!_navMeshAgent)
            Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
