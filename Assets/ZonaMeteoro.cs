using UnityEngine;

public class ZonaMeteoro : MonoBehaviour
{
    [Header("Meteorito")]
    public GameObject meteoritoPrefab;
    public Transform puntoImpacto;          // lugar exacto donde caerá
    public float alturaSpawn = 30f;         // altura sobre el punto desde donde aparece
    public bool soloUnaVez = true;

    bool yaActivado = false;

    void OnTriggerEnter(Collider other)
    {
        if (yaActivado && soloUnaVez) return;
        if (other.GetComponentInParent<movimiento>() == null) return;

        yaActivado = true;


        Vector3 spawn = puntoImpacto.position + Vector3.up * alturaSpawn;
        Instantiate(meteoritoPrefab, spawn, Quaternion.identity);
    }
}