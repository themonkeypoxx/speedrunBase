using UnityEngine;

public class Meteorito : MonoBehaviour
{
    public float velocidadCaida = 25f;
    public GameObject efectoImpacto;   
    public float tiempoVida = 10f;

    void Start()
    {
        Destroy(gameObject, tiempoVida);
    }

    void Update()
    {
        transform.Translate(Vector3.down * velocidadCaida * Time.deltaTime, Space.World);
        transform.Rotate(90f * Time.deltaTime, 60f * Time.deltaTime, 0f);
    }

    void OnCollisionEnter(Collision col)
    {
        if (efectoImpacto != null)
            Instantiate(efectoImpacto, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}