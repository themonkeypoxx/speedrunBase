using UnityEngine;

public class camera : MonoBehaviour
{
    public GameObject loquetengoqueseguircfff; // esto se pondra en el inspector como el jugador
    private Vector3 distancia;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //saca la posición del jugador y define la distancia de la cámara y este
        distancia = transform.position - loquetengoqueseguircfff.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // actualiza por el último frame
    private void LateUpdate()
    {
        transform.position = loquetengoqueseguircfff.transform.position + distancia;
    }
}
