using UnityEngine;
using TMPro;
using UnityEngine.PlayerLoop;

public class movimiento : MonoBehaviour
{
    private Rigidbody rb;
    public float velocidadAdelante = 20f;
    public float fuerzaSalto = 10f;
    public float velocidadCambio = 8f;
    private bool enSuelo = true;
    private int carrilActual = 0; //centro, izq -1, derecha 1
    public float distanciaCarril = 3f;
    private int contador_coleccionables = 0;
    private bool fin = false;
    public int coleccionables = 19;
    public TMP_Text mensajeFinal;
    public Animator animator;
    //TEMPORIZADOR
    public float tiempoRestante = 120.0f;
    public bool cuentaReg = true;
    public TMP_Text temporizador;
    private bool tempoactivo = true;
    public float duracionRetroceso = 1f;
    public float velocidadRetroceso = 10f;
    private float tiempoRetroceso = 0f;
    public TMP_Text contador_mostrado;
    private bool ejeEnUso = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        if (mensajeFinal != null)
        {
            mensajeFinal.gameObject.SetActive(false);
        }
        ActualizarContador();
    }

    // Update is called once per frame
    // Update se necesita para movimientos de carril
    void Update()
    {
        if (tempoactivo)
        {
            if (cuentaReg)
            {
                tiempoRestante -= Time.deltaTime;

                if (tiempoRestante <= 0)
                {
                    tiempoRestante = 0;
                    tempoactivo = false;
                    Mensaje("Se terminó el tiempo");
                }
            }
            else
            {
                tiempoRestante += Time.deltaTime;
            }
            ActualizarTimes(tiempoRestante);
        }

            float ejeVertical = Input.GetAxisRaw("Vertical");
            if (ejeVertical != 0)
            {
                if (!ejeEnUso)
                {
                    if (ejeVertical > 0.5f && carrilActual > -1)
                    {
                        carrilActual--;
                        if (animator != null)
                        {
                            animator.SetTrigger("izq");
                        }
                        ejeEnUso = true; // Bloquea nuevos cambios hasta soltar la palanca
                    }
                    else if (ejeVertical < -0.5f && carrilActual < 1)
                    {
                        carrilActual++;
                        if (animator != null)
                        {
                            animator.SetTrigger("der");
                        }
                        ejeEnUso = true; // Bloquea nuevos cambios hasta soltar la palanca
                    }
                }
            }
            else
            {
                ejeEnUso = false;
            }

        if (Input.GetButtonDown("Jump") && enSuelo)
        {
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
            enSuelo = false;
        }}

    void FixedUpdate()
    {
        if (!tempoactivo || fin)
        {
            fin = true;
            rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
            if (animator != null) animator.SetTrigger("idle");
            return;
        }

        float posXObj = carrilActual * distanciaCarril;
        float velX = (posXObj - rb.position.x) * velocidadCambio;

        if (tiempoRetroceso > 0f)
        {
            tiempoRetroceso -= Time.fixedDeltaTime;
            float factor = Mathf.Clamp01(tiempoRetroceso / duracionRetroceso);
            rb.linearVelocity = new Vector3(velX, rb.linearVelocity.y, -velocidadRetroceso * factor);
            return;
        }

        rb.linearVelocity = new Vector3(velX, rb.linearVelocity.y, velocidadAdelante);
    }

    private void OnCollisionEnter(Collision choque)
    {
        if(choque.gameObject.CompareTag("piso"))
        {
            enSuelo = true;
        }

        if (choque.gameObject.CompareTag("atasco") && tiempoRetroceso <= 0f)
        {
            if (tiempoRetroceso <= 0f)
             {
                 tiempoRetroceso = duracionRetroceso;  
             }
            if (animator != null)
            {
                animator.SetTrigger("choque");
            }
            
        }
    }

    private void ActualizarContador()
    {
        contador_mostrado.text = "Huevos rescatados: " + contador_coleccionables + " / " + coleccionables;
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
            ActualizarContador();
        }
        //Detección del final
        if (otro.CompareTag("Final"))
        {
            fin = true;
            tempoactivo = false;
            if(contador_coleccionables >= coleccionables)
            {
               Mensaje("¡Ganaste!"); 
            }
            else
            {
                int faltantes = coleccionables - contador_coleccionables;
                Mensaje("Te faltaron: "+faltantes +" huevos!");
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
    private void ActualizarTimes(float tiempoEnSegundos)
    {
        // Evita números negativossssss
        if (tiempoEnSegundos < 0) tiempoEnSegundos = 0;

        float minutos = Mathf.FloorToInt(tiempoEnSegundos / 60);
        float segundos = Mathf.FloorToInt(tiempoEnSegundos % 60);

        // pasar a MM:SS
        temporizador.text = string.Format("{0:00}:{1:00}", minutos, segundos);
        if (fin)
        {
            return;
        }
    }
}
