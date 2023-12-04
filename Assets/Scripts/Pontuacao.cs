using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;
public class Pontuacao
{
    public static int aux = 0;
    public static int facetaDado = 0;
    public Visor visor;
   public enum TiposPontuacao
    {
        GENERAL,
        POKER,
        FULA,
        SEQUENCIA,
        ERRO,
        COMUM
    };

    public int pontos;

    public Pontuacao()
    {
        pontos = 0;
        this.visor = new Visor();
    }


    public void addPontuacao(int tipo)
    {
        Debug.Log("entrou add");
        switch (tipo)
        {
            case (int) TiposPontuacao.GENERAL:
                pontos += 50;
                visor.changePontos(9,50);
                Debug.LogWarning("Deu General");
                break;
            case (int)TiposPontuacao.POKER:
                pontos += 40;
                visor.changePontos(8, 40);
                Debug.LogWarning("Deu Poker");
                break;
            case (int)TiposPontuacao.FULA:
                pontos += 30;
                visor.changePontos(7, 30); 
                Debug.LogWarning("Deu fula");
                break;
            case (int)TiposPontuacao.SEQUENCIA:
                pontos += 20;
                visor.changePontos(6, 20);
                Debug.LogWarning("Deu Sequencia");
                break;
            case (int)TiposPontuacao.COMUM:
                pontos += aux;
                Debug.Log("Cara do dado a beca:" +facetaDado);
                switch (facetaDado)
                {
                    case 1: 
                        visor.changePontos(0, aux);
                        break;
                    case 2:
                        visor.changePontos(1, aux);
                        break;
                    case 3:
                        visor.changePontos(2, aux);
                        break;
                    case 4:
                        visor.changePontos(3, aux);
                        break;
                    case 5:
                        visor.changePontos(4, aux);
                        break;
                    case 6:
                        visor.changePontos(5, aux);
                        break;
                }
                Debug.Log("aUXILIADOR:" +aux);
                aux = 0;
                Debug.LogWarning("Comum");
                break;
            default:
                Debug.LogError("Pontuação invalida");
                break;

        }
    }

    public void resetPontuacao()
    {
        pontos = 0;
    }

    public void mostrarNoVisor(int tipoJogador)
    {
        try
        {
            if(tipoJogador != 1 || tipoJogador != 2)
            {
                throw new UnityException("O tipo do jogador é inválido.");  
            }
               

        }
        catch(UnityException err)
        {
            throw err;
        }
    }
}