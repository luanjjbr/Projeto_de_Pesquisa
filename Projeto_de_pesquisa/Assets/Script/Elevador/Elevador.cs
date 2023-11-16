using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO.Ports;
using UnityEngine.Rendering.Universal;
using TMPro;
using TMPro.Examples;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Elevador : MonoBehaviour
{
    private SerialPort serialPort;
    public string portName = "COM3"; // Nome da porta serial
    public int baudRate = 9600; // Taxa de transmissão
    private float updateInterval = 0.1f;

    public TextMeshProUGUI text;
    private float passo = 1;
    public float app = 1;
    int q = 0, e = 0;

    private int valorRecebido;
    private int buttonState;
    public Porta porta;

    public TMP_InputField Passos;
    public TMP_InputField PPR;
    public TMP_InputField Diametro;
    public Button E;
    public Button Q;
    //public Animator animadorDoOutroObjeto;
    //public string nomeDoParametro = "Porta";
    // Start is called before the first frame update
    void Start()
    {
        serialPort = new SerialPort(portName, baudRate);
        try
        {
            serialPort.Open();
            Debug.Log("Porta serial aberta com sucesso.");
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Erro ao abrir a porta serial: " + ex.Message);
        }
        porta = FindObjectOfType<Porta>();
        E.onClick.AddListener(OnBotaoClicado);
        Q.onClick.AddListener(OnBotaoClicado1);
        text.text = "0: " + q + "\n 9: " + e;
        Passos.text = "1";
        PPR.text = "200";
        Diametro.text = "0.5";
        InvokeRepeating("UpdateSerialData", 0, updateInterval);
    }
    private void OnBotaoClicado()
    {
        MoverObjeto(1);
    }
    private void OnBotaoClicado1()
    {
        MoverObjeto(-1);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //controle da porta com teclado
        if (Input.GetKey(KeyCode.Alpha1) || buttonState == 1) { porta.ani[0] = true; }
        else if (Input.GetKey(KeyCode.Alpha5) || buttonState == 5) { porta.ani[0] = false; }

        if (Input.GetKey(KeyCode.Alpha2) || buttonState == 2) { porta.ani[1] = true; }
        else if (Input.GetKey(KeyCode.Alpha6) || buttonState == 6) { porta.ani[1] = false; }

        if (Input.GetKey(KeyCode.Alpha3) || buttonState == 3) { porta.ani[2] = true; }
        else if (Input.GetKey(KeyCode.Alpha7) || buttonState == 7) { porta.ani[2] = false; }

        if (Input.GetKey(KeyCode.Alpha4) || buttonState == 4) { porta.ani[3] = true; }
        else if (Input.GetKey(KeyCode.Alpha8) || buttonState == 8) { porta.ani[3] = false; }


        // Verifica se a tecla Q está pressionada
        if (Input.GetKey(KeyCode.Alpha9) || buttonState == 9)
        {
            MoverObjeto(1);
        }
        else if (Input.GetKey(KeyCode.Alpha0) || buttonState == 0)
        {
            MoverObjeto(-1);
        }
        buttonState = -2;
    }
    private void UpdateSerialData()
    {
        if (serialPort.IsOpen && serialPort.BytesToRead > 0)
        {
            //ClearUnityConsole();
            string data = serialPort.ReadLine();
            //Debug.Log(data);
            if (int.TryParse(data, out int parsedValue))
            {
                //buttonState = int.Parse(data);
                buttonState = parsedValue;
                //Debug.Log("Botão Pressionado: " + buttonState);
            }
            else
            {
                buttonState = -1;
                //Debug.Log("Botão Pressionado: " + buttonState);
            }
        }
    }
    void MoverObjeto(int id)
    {
        if (id == 1)
        {
            e++;
        }
        else
        {
            q++;
        }
        float p3 = float.Parse(Passos.text);
        float p6 = float.Parse(Diametro.text);
        float p5 = float.Parse(PPR.text);
        passo = p3 * (3.1415926f * p6) / p5;
        //print(passo);
        Vector3 movimento = new Vector3(0.0f, id, 0.0f) * passo * app;
        transform.position += movimento;
        text.text = "0: " + q + "\n9: " + e;
    }

    public string tagAlvo = "SuaTagAqui";
    public GameObject objetoPai;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagAlvo))
        {
            // Ao entrar na área, tornar o objeto filho do objeto pai
            other.transform.parent = objetoPai.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(tagAlvo))
        {
            // Ao sair da área, remover o objeto como filho do objeto pai
            other.transform.parent = null;
        }
    }
}
