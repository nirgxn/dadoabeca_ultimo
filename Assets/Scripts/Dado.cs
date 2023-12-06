using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Dado : MonoBehaviour,
    IPointerDownHandler
{
    bool clicado;
    public int qntd;
    bool manter;

 

    private void Start()
    {
        //controla dado clicado
        //clicado = manter = false;
        this.gameObject.GetComponent<BoxCollider2D>().autoTiling = true;
        this.clicado = false;
        this.manter = true;
    }
    public void resetAll()
    {
        this.clicado = false;
        this.manter = true;
        this.gameObject.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    public void setValor(int valor)
    {
        this.qntd = valor;
    }


    public bool getClicado()
    {
        return this.clicado;
    }

    public void setClicado(bool status)
    {
        this.clicado = status;
    }

    public void zerarDados()
    {    
        this.manter = true;
        this.clicado = false;
    }

    public bool getManter()
    {
        return this.manter;
    }

    public void setManter(bool status)
    {
        this.manter = status;
    }

    public void getLogDado()
    {
      
       Debug.Log("Clicado:" + this.clicado + "\tManter:" + this.manter);
    }

    public void OnPointerDown(PointerEventData eventData) // Quando clicar em cima
    {
        if (Input.GetMouseButtonDown(0))
        {
            clicado = !clicado; //false
            manter = !manter; //true
           // this.getLogDado();

            if (clicado)
            {
                //aumenta
                this.gameObject.GetComponent<RectTransform>().localScale = new Vector3(1.3f, 1.3f, 1.3f);

            }
            else
            {
                //diminui
                this.gameObject.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);

            }
        }
    }

    public void resetar()
    {
        this.Start();
    }
}
