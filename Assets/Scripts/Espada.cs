using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espada : MonoBehaviour
{


    public bool pTouched = false;
    public int contador;
    public int contadorAgarre;
    public GameObject barra;
    public GameObject barra2;
    public GameObject barra3;
    public GameObject bladeAcabado;




    // Start is called before the first frame update
    void Start()
    {
        pTouched = false;
        contador = 0;
        contadorAgarre = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EspadaFueAgarrada()
    {
        if(contadorAgarre == 0)
        {
            Debug.Log("Espada agarrada");
            GameObject.Find("SimulationController").GetComponent<SimulationController>().VerifyUserAction(new SimulationObject.Action(gameObject.name, "EspadaFueAgarrada", ""));
            contadorAgarre++;
        }
    }

    public void checkColisionesConMartillo()
    {
        GameObject.Find("SimulationController").GetComponent<SimulationController>().VerifyUserAction(new SimulationObject.Action(gameObject.name, "checkColisionesConMartillo", ""));
    }

    public void espadaEnAgua()
    {
        GameObject.Find("SimulationController").GetComponent<SimulationController>().VerifyUserAction(new SimulationObject.Action(gameObject.name, "espadaEnAgua", ""));
    }

    public void espadaEnHorno()
    {
        GameObject.Find("SimulationController").GetComponent<SimulationController>().VerifyUserAction(new SimulationObject.Action(gameObject.name, "espadaEnHorno", ""));
    }

    IEnumerator OnCollisionEnter(Collision other)
    {
        if(!pTouched && other.gameObject.CompareTag("Martillo"))
        {
            pTouched = true;
            contador++;
            Debug.Log("Martillo ha colisionado " + contador);

            if(contador == 2)
            {
                barra.SetActive(false);
                barra2.SetActive(true);
            }

            if (contador == 4)
            {
                barra2.SetActive(false);
                barra3.SetActive(true);
            }

            if (contador == 5)
            {
                barra3.SetActive(false);
                bladeAcabado.SetActive(true);
                checkColisionesConMartillo();
                Debug.Log("Espada con martillo");
            }
            yield return new WaitForSeconds(0.5f);
            pTouched = false;
        }


        if (!pTouched && other.gameObject.CompareTag("Horno"))
        {
            pTouched = true;
            Debug.Log("Espada con Horno");

            espadaEnHorno();
            yield return new WaitForSeconds(0.5f);
            
            pTouched = false;
        }

        

    }


    IEnumerator OnTriggerEnter(Collider other)
    {
        if (!pTouched && other.gameObject.CompareTag("Agua"))
        {
            pTouched = true;
            Debug.Log("Espada con agua");

            espadaEnAgua();
            yield return new WaitForSeconds(0.5f);
            

            Debug.Log("Espada duró 10 segundos en agua");

            pTouched = false;
        }

        if (!pTouched && other.gameObject.CompareTag("Mano"))
        {
            pTouched = true;
            
            
            EspadaFueAgarrada();
             
            

            yield return new WaitForSeconds(0.5f);
            pTouched = false;
        }
    }







}
