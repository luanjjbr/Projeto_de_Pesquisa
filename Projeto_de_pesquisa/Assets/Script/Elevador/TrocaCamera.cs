using UnityEngine;

public class TrocarCamera : MonoBehaviour
{
    public GameObject[] objetos;
    public float intervaloDeTroca = 2f; // Intervalo em segundos

    private int indiceAtual = 0;

    void Start()
    {
        Player.id = true;
    }

    void Update()
    {
        // Se a tecla de espaço for pressionada, alternar os objetos
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Player.id = true;
            DesbloquearCursor();
            AlternarObjetos();
        }
    }
    void AlternarObjetos()
    {
        // Desativar o objeto atual
        DesativarObjeto(indiceAtual);

        // Avançar para o próximo objeto
        indiceAtual = (indiceAtual + 1) % objetos.Length;

        // Ativar o próximo objeto
        AtivarObjeto(indiceAtual);
    }

    void AtivarObjeto(int index)
    {
        objetos[index].SetActive(true);
        //Debug.Log("Ativando objeto: " + objetos[index].name);
    }

    void DesativarObjeto(int index)
    {
        objetos[index].SetActive(false);
        //Debug.Log("Desativando objeto: " + objetos[index].name);
    }
    void DesbloquearCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
