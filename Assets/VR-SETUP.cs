using UnityEngine;
public class VRSetup : MonoBehaviour
    {
    public Camera camaraIzquierda;
    public Camera camaraDerecha;
    public float separacionOjos = 0.056f;
    void Start()
        {
            Vector3 posIzq = camaraIzquierda.transform.localPosition;
            Vector3 posDer = camaraDerecha.transform.localPosition;

            camaraIzquierda.transform.localPosition = posIzq + new Vector3(-separacionOjos / 2, 0, 0);
            camaraDerecha.transform.localPosition   = posDer + new Vector3( separacionOjos / 2, 0, 0);

            camaraIzquierda.rect = new Rect(0, 0, 0.5f, 1);
            camaraDerecha.rect   = new Rect(0.5f, 0, 0.5f, 1);
        }
    }