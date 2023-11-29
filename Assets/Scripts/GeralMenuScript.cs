using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeralMenuScript : MonoBehaviour
{
    public void AbreJogo()
    {
        SceneManager.LoadScene("jogoRodando");
    }
}
