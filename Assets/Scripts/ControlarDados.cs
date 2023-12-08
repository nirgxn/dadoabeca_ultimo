
using System;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Diagnostics.Contracts;

public class ControlarDados : MonoBehaviour
{
    private Jogo controlarJogo = new Jogo(true, 3);
    public Button botaoRolarDado;
    public Button botaoManterDado;
    [SerializeField] private List<GameObject> listDados;
    public Text textoJogador;
    public Text textoLances;
    private System.Random rand = new System.Random();


    public List<GameObject> visor1 = new List<GameObject>();
    public List<GameObject> visor2 = new List<GameObject>();
    void Start()
    {
        controlarJogo.jogador1 = true;
        controlarJogo.rolagemLimite = 3;
        controlarJogo.contador = 1;
        textoLances.text = "2 lances restantes";
        textoJogador.text = "Jogador 1";


        controlarJogo.p1.visor.addGameObjects(visor1.ToArray());
        controlarJogo.p2.visor.addGameObjects(visor2.ToArray());


        sortearDados();
        botaoRolarDado.onClick.AddListener(delegate ()
        {
            verificar_trocarCena("sdsds");
            if (controlarJogo.jogador1)
            {
                textoJogador.text = "Jogador 1";
            }
            else
            {
                textoJogador.text = "Jogador 2";
                //Debug.Log("Vez do jogador 2");
            }
            controlarJogo.contador++;
            sortearDados();

            if (controlarJogo.contador == 4)
            {
                controlarJogo.contador = 1;
                textoLances.text = "2 lances restantes";
                trocarJogador();
            }
            if (controlarJogo.contador < 4)
            {
                textoLances.text = (controlarJogo.rolagemLimite - controlarJogo.contador).ToString() + " lances restantes";
            }
        });

        //evento de clique no botão manter.
        botaoManterDado.onClick.AddListener(delegate ()
        {


            verificar_trocarCena("sdsds");


            if (controlarJogo.jogador1)
            {

                controlarJogo.p1.addPontuacao(detectarTipoPontuacao());
                //Debug.Log("Vez do jogador 1");
            }
            else
            {
                controlarJogo.p2.addPontuacao(detectarTipoPontuacao());
                //Debug.Log("Vez do jogador 2");
            }
            mostrarDados();
            trocarJogador();
            controlarJogo.contador = 1;

            //  Debug.Log("Você clicou no botão manter dado!");
        });
    }

    public void sortearDados()
    {
 
        int numeroSorteado;

        for (int i = 0; i < 5; i++)
        {
            // gerar número random d 1 a 6.
            numeroSorteado = rand.Next(1, 7);
            if (listDados[i].GetComponent<Dado>().getClicado())
            {
                continue;
            }
            listDados[i].GetComponent<Dado>().setValor(numeroSorteado);
            listDados[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/dado_" + numeroSorteado);
            listDados[i].GetComponent<Dado>().qntd = numeroSorteado;
        }
    }

    public int detectarTipoPontuacao()
    {

        if (pontuacaoGeneral())
        {
            //  Debug.LogWarning("General");
            return (int)Pontuacao.TiposPontuacao.GENERAL;
        }

        else if (pontuacaoPoker())
        {
            //Debug.LogWarning("Poker");
            return (int)Pontuacao.TiposPontuacao.POKER;
        }

        else if (pontuacaoFula())
        {
            //Debug.LogWarning("Fula");
            return (int)Pontuacao.TiposPontuacao.FULA;
        }
        else if (pontuacaoSequencia())
        {
            //Debug.LogWarning("Sequencia");
            return (int)Pontuacao.TiposPontuacao.SEQUENCIA;
        }
        else if (pontuacaoComum())
        {
            return (int)Pontuacao.TiposPontuacao.COMUM;
        }
        else
        {
            throw new Exception("NAO SEI O QUE ACONTECEU!");
        }
    }
    private bool pontuacaoGeneral()
    {

        if (listDados[0].GetComponent<Dado>().qntd == listDados[1].GetComponent<Dado>().qntd &&
            listDados[1].GetComponent<Dado>().qntd == listDados[2].GetComponent<Dado>().qntd &&
            listDados[2].GetComponent<Dado>().qntd == listDados[3].GetComponent<Dado>().qntd &&
            listDados[3].GetComponent<Dado>().qntd == listDados[4].GetComponent<Dado>().qntd
            )
        {
            return true;
        }

        return false;
    }
    private bool pontuacaoPoker()
    {
        List<int> lista = new List<int>();

        for (int i = 0; i < listDados.Count; i++)
        {
            lista.Add(listDados[i].GetComponent<Dado>().qntd);
        }

        lista.Sort();

        if (lista[0] == lista[1] && lista[1] == lista[2] && lista[2] == lista[3] && lista[4] != lista[3] ||
        lista[0] != lista[1] && lista[1] == lista[2] && lista[2] == lista[3] && lista[3] == lista[4]
    )
        {
            return true;
        }


        return false;
    }
    private bool pontuacaoFula()
    {
        List<int> lista = new List<int>();

        for (int i = 0; i < listDados.Count; i++)
        {
            lista.Add(listDados[i].GetComponent<Dado>().qntd);
        }

        lista.Sort();

        if (lista[0] == lista[1] && lista[1] == lista[2] && (lista[2] != lista[3]) && lista[3] == lista[4] ||
        lista[0] == lista[1] && lista[1] != lista[2] && lista[2] == lista[3] && lista[3] == lista[4]
)
        {
            return true;
        }

        return false;
    }

    private bool pontuacaoSequencia()
    {
        List<int> lista = new List<int>();

        for (int i = 0; i < listDados.Count; i++)
        {
            lista.Add(listDados[i].GetComponent<Dado>().qntd);
        }

        lista.Sort();

        if ((lista[0] == 1 && lista[1] == 2 && lista[2] == 3 && lista[3] == 4 && lista[4] == 5) ||
         (lista[0] == 2 && lista[1] == 3 && lista[2] == 4 && lista[3] == 5 && lista[4] == 6) ||
         (lista[0] == 1 && lista[1] == 3 && lista[2] == 4 && lista[3] == 5 && lista[4] == 6))
        {
            return true;
        }

        return false;
    }

    private bool pontuacaoComum()
    {
        // contagemNumeros  = contar quantos números se repetem
        int[] contagemNumeros = new int[5];
        List<int> lista = new List<int>();

        for (int i = 0; i < listDados.Count; i++)
        {
            lista.Add(listDados[i].GetComponent<Dado>().qntd);
        }

        lista.Sort();


        for (int i = 0; i < lista.Count; i++)
        {

            switch (lista[i])
            {
                case 1:
                    contagemNumeros[0] += 1;
                    break;
                case 2:
                    contagemNumeros[1] += 1;
                    break;
                case 3:
                    contagemNumeros[2] += 1;
                    break;
                case 4:
                    contagemNumeros[3] += 1;
                    break;
                case 5:
                    contagemNumeros[4] += 1;
                    break;
            }
        }


        int[] vetor = lista.ToArray();
        EncontrarNumerosRepetidos(vetor);

        return true;

    }
    void trocarJogador()
    {

        foreach (var dado in listDados)
        {
            dado.GetComponent<Dado>().setClicado(false);
            dado.GetComponent<Dado>().resetAnimacao();
        }

        controlarJogo.jogador1 = !controlarJogo.jogador1;
        controlarJogo.zerarContador();
        controlarJogo.contaRodadas++;
        if (controlarJogo.jogador1)
        {
            textoJogador.text = "Jogador 1";
        }
        else
        {
            textoJogador.text = "Jogador 2";
        }

        textoLances.text = (controlarJogo.rolagemLimite - controlarJogo.contador).ToString() + " lances restantes";


        sortearDados();
        foreach (var dado in listDados)
        {
            dado.GetComponent<Dado>().atualizarAnimacao();
        }
    }

    void mostrarDados()
    {
        string a = "";


        for (int i = 0; i < 5; i++)
        {
            a += listDados[i].GetComponent<Dado>().qntd.ToString() + " ";
        }

        //Debug.LogError(aux);
    }

    void EncontrarNumerosRepetidos(int[] vetor)
    {
        // Dicionário para armazenar a contagem de cada número
        Dictionary<int, int> contagemNumeros = new Dictionary<int, int>();
        System.Random rand = new System.Random();
        // Percorre o vetor para contar a ocorrência de cada número
        foreach (int numero in vetor)
        {
            if (contagemNumeros.ContainsKey(numero))
            {
                // Se o número já existe no dicionário, incrementa a contagem
                contagemNumeros[numero]++;
            }
            else
            {
                // Se o número não existe no dicionário, adiciona com contagem 1
                contagemNumeros[numero] = 1;
            }
        }


        int res_for_aux_1 = -1;
        int res_for_aux_2 = -1;
        int ext_key_1 = 0;
        int ext_key_2 = 0;
        //Debug.Log("Números que se repetem:");
        foreach (var par in contagemNumeros)
        {
            if (par.Value > 1)
            {
                //Debug.Log($"{par.Key} se repete {par.Value} vezes");

                if (res_for_aux_1 == -1)
                {
                    res_for_aux_1 = par.Key * par.Value;
                    ext_key_1 = par.Key;

                }
                else if (res_for_aux_2 == -1)
                {
                    res_for_aux_2 = par.Key * par.Value;
                    ext_key_2 = par.Key;
                }
            }
        }

        int nroSorteado = rand.Next(0, 2);

        if (nroSorteado == 0)
        {
            Pontuacao.aux = res_for_aux_1;
            Pontuacao.facetaDado = ext_key_1;
            return;
        }

        Pontuacao.aux = res_for_aux_2;
        Pontuacao.facetaDado = ext_key_2;

    }

    void zerarTodosOsDados()
    {

        foreach (var dados in this.listDados)
        {
            dados.GetComponent<Dado>().resetAll();
        }


    }

    void verificar_trocarCena(string cena)
    {
        //Debug.Log("Contador:" +controlarJogo.contaRodadas);
        if (controlarJogo.contaRodadas / 2 == 10)
        {
            SceneManager.LoadScene(cena);
            return;
        }
    }
}
public class Jogo
{
    public bool jogador1;
    public Pontuacao p1;
    public Pontuacao p2;
    public int rolagemLimite;
    public int contador;
    public int contaRodadas;
    public Jogo(bool jogador1, int rolagemLimite)
    {
        p1 = new Pontuacao();
        p2 = new Pontuacao();
        this.jogador1 = jogador1;
        this.rolagemLimite = rolagemLimite;
        contador = 1;
        contaRodadas = 0;
    }

    public void zerarContador()
    {
        this.contador = 1;
    }

}


public class DadosJogador
{
    //lista priv armazenar lista clicados
    private List<string> dadosClicados;
    private string jogador;
    public Pontuacao pontuacaoJogador;

    //inicializa lista d lista clicados e define o jogador.
    public DadosJogador(string __jogador)
    {
        this.dadosClicados = new List<string>();
        this.jogador = __jogador;
    }

    //add dado clicado na lista
    void adicionarDadoClicado(string dadoClicado)
    {
        if (dadosClicados.Count <= 5)
        {
            dadosClicados.Add(dadoClicado);
        }
    }

    void removerDadoClicado(string dadoClicado)
    {
        if (dadosClicados.Count != 0)
        {
            dadosClicados.Remove(dadoClicado);
        }
    }

    void limparLista()
    {
        dadosClicados.Clear();
    }


}

