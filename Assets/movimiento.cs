using UnityEngine;
using TMPro;
using UnityEngine.PlayerLoop;

public class movimiento : MonoBehaviour
{
    private Rigidbody rb;
    public float velocidadAdelante = 10f;
    public float fuerzaSalto = 7f;
    public float velocidadCambio = 8f;
    private bool enSuelo = true;
    private int carrilActual = 0; //centro, izq -1, derecha 1
    public float distanciaCarril = 3f;
    private int contador_coleccionables = 0;
    private bool fin = false;
    public int coleccionables = 19;
    public TMP_Text mensajeFinal;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (mensajeFinal != null)
        {
            mensajeFinal.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    // Update se necesita para movimientos de carril
    void Update()
    {
        //Si se apreta la a, restarle una unidad al carril actual (solo si este es mayor a -1)
        if (Input.GetKeyDown(KeyCode.W) && carrilActual > -1)
        {
            carrilActual--;
        } 

        if (Input.GetKeyDown(KeyCode.S) && carrilActual < 1)
        {
            carrilActual++;
        } 

        if (Input.GetKeyDown(KeyCode.Space) && enSuelo)
        {
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
            enSuelo = false;
        } 

    }

    void FixedUpdate()
    {
        //avance automático
        rb.AddForce(Vector3.forward * velocidadAdelante);

        //Cambio de carril
        float posXObj = carrilActual * distanciaCarril;
        //sacar posicion actual del objeto
        Vector3 posicionActual = rb.position;
        //cambiar si toca hacia otros lados que no sean forward
        Vector3 posicionDeseada = new Vector3(posXObj, posicionActual.y, posicionActual.z);
        //NUEVA
        Vector3 nuevaPos = Vector3.Lerp(posicionActual, posicionDeseada, Time.fixedDeltaTime * velocidadCambio);
        //ALICARLA
        rb.MovePosition(nuevaPos);

    }

    private void OnCollisionEnter(Collision choque)
    {
        if(choque.gameObject.CompareTag("piso")){
            enSuelo = true;
        }
    }


    private void OnTriggerEnter(Collider otro)
    {
        if (fin)
        {
            return;
        }

        //Recoger huevos
        if (otro.CompareTag("huevo"))
        {
            contador_coleccionables++;
            Debug.Log("Coleccionables: " + contador_coleccionables);
            Destroy(otro.gameObject);
        }
        //Detección del final
        if (otro.CompareTag("final"))
        {
            fin = true;
            if(contador_coleccionables >= coleccionables)
            {
               Mensaje("¡Ganaste!"); 
            }
            else
            {
                int faltantes = coleccionables - contador_coleccionables;
                Mensaje("Te faltaron: "+faltantes +"huevos!");
            }
        }
    }
    private void Mensaje(string texto)
    {
        if (mensajeFinal != null)
        {
            mensajeFinal.text = texto;
            mensajeFinal.gameObject.SetActive(true);
        }
        
    }
}
