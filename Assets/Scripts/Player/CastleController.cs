using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CastleController : MonoBehaviour
{
    [Inject]
    PlayerController _playerController;

    private void OnTriggerEnter(Collider other)
    {

        AbstractEnemy enemy = other.gameObject.GetComponent<AbstractEnemy>();
        if (enemy)
        {

            _playerController.TakeDamage(enemy.Damage);

            Destroy(enemy.gameObject);
        }

    }

}
