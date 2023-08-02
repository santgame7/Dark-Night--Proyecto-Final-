using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_AI : MonoBehaviour
{
    /// <summary>
    /// enemy: Es el que guarda el componente para navegar por el mapa
    /// _target: son aquellos objetos que sigue el enemigo para que la ruta de caminar sea posible esta puede ponerse aleatoria si se quiere.
    /// jugador: es el objeto jugador en la escena
    /// nTar: es el numero que identifica los targets del enemigo
    /// medidasRadar: son las medidas en la que el enemigo puede detectar al jugador.
    /// capaJ: se refiere a capa del jugador.
    /// colision: es aquel que se encargar de colisionar para detectar al jugador.
    /// </summary>
 
    NavMeshAgent enemy;
    [SerializeField] GameObject[] _target;
    GameObject _jugador;

    [SerializeField] int nTar;

    [SerializeField] Vector3 medidasRadar;
    [SerializeField] LayerMask capaJ;

    Collider[] colision;


    /// <summary>
    /// _target = GameObject.FindGameObjectsWithTag("Target");
    /// _jugador = GameObject.FindGameObjectWithTag("Player");
    /// enemy = GetComponent<NavMeshAgent>();
    /// nTar = 0;
    /// </summary>
    void Start()
    {
        _target = GameObject.FindGameObjectsWithTag("Target");
        _jugador = GameObject.FindGameObjectWithTag("Player");
        enemy = GetComponent<NavMeshAgent>();

        nTar = 0;
    }


    // Update is called once per frame
    void Update()
    {
        if (enemy.enabled == true)
            enemy.SetDestination(_target[nTar].transform.position);

       colision = Physics.OverlapBox(this.transform.position, medidasRadar, this.transform.rotation, capaJ);


        foreach (Collider collider in colision)
        {
            if (collider.CompareTag("Player") && colision.Length > 0)
            {
                enemy.SetDestination(_jugador.transform.position);
            }
            else
            {
                enemy.SetDestination(_target[nTar].transform.position);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Target")
        {
            //z = Random.Range(0, pos.Length);
            nTar++;
            if (nTar >= _target.Length)
            {
                nTar = 0;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawSphere(_target[nTar].transform.position, 1f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(this.transform.position, medidasRadar);
    }
}
