using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Windows.Speech;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using TwitchLib.Unity;
using UnityEngine;

public class TwitchClient : MonoBehaviour
{
    public Client client;
    private string channel_name = "El_Largosstia";
    private SoundManager soundmanager;
    private int contador = 0;
    [SerializeField]
    private GameObject[] efectos;

    private void Awake()
    {
        soundmanager = FindObjectOfType<SoundManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Application.runInBackground = true;

        ConnectionCredentials credentials = new ConnectionCredentials(Secrets.USERNAME_FROM_OAUTH_TOKEN, Secrets.OAUTH_TOKEN);
        client = new Client();
        client.Initialize(credentials, channel_name);

        client.OnConnected += connect;
        client.OnChatCommandReceived += chatcommandreceived;
        client.OnGiftedSubscription += giftsus;
        client.OnNewSubscriber += newsus;
        client.OnRaidNotification += raid;
        client.OnReSubscriber += resuscriber;
        client.OnMessageReceived += messagereceived;


        client.Connect();
    }

    private void messagereceived(object sender, OnMessageReceivedArgs e)
    {
        //ID de recompensas
        // Hidratacion: ea7647b2-9a8e-4577-ab2d-77a336a5640f
        //Preguntame algo: ad30c6f4-a83b-4192-addd-e199501378a0
        //recomienda una recompensa: 640e1a2b-0a79-4d46-9709-0646c309f449
        //Groseria: a751eef9-51a7-4da9-8aa0-6f5542e3b5b8
        //Cambiar juego: 69c44119-1617-4a7b-9570-010661b3c538

        //client.SendMessage(channel_name, $"veamos que imprime {e.ChatMessage.CustomRewardId.ToString()}");
        switch (e.ChatMessage.CustomRewardId.ToString())
        {
            case Variables.Hidratacion:
                soundmanager.Seleccionaaudio(9, 1);
                break;

            case Variables.Pregunta:
                client.SendMessage(channel_name, $"!say {e.ChatMessage.DisplayName} pregunta, {e.ChatMessage.Message}");
                break;

            case Variables.Recomienda:
                client.SendMessage(channel_name, $"!say {e.ChatMessage.DisplayName} Recomienda crear la recompensa:, {e.ChatMessage.Message}");
                break;

            case Variables.groseria:
                client.SendMessage(channel_name, $"!say Tienes que decir, {e.ChatMessage.Message}");
                break;

            case Variables.cambiar_juego:
                client.SendMessage(channel_name, $"!say {e.ChatMessage.DisplayName} quiere que juegues, {e.ChatMessage.Message}");
                break;
        }
    }

    private void resuscriber(object sender, OnReSubscriberArgs e)
    {
        client.SendMessage(channel_name, $"!say Gracias {e.ReSubscriber.DisplayName} por seguir apoyando");
    }

    private void newsus(object sender, OnNewSubscriberArgs e)
    {
        client.SendMessage(channel_name, $"!say Gracias {e.Subscriber.DisplayName} se te agradece el apoyo uwu");
        soundmanager.Seleccionaaudio(7, 1);
    }

    private void raid(object sender, OnRaidNotificationArgs e)
    {
        client.SendMessage(channel_name, $"!say {e.RaidNotification.DisplayName} trajo a sus compas UwU");
        soundmanager.Seleccionaaudio(2, 1);
    }

    private void giftsus(object sender, OnGiftedSubscriptionArgs e)
    {
        client.SendMessage(channel_name, $"!say A {e.GiftedSubscription.DisplayName} le sobra el dineros");
        soundmanager.Seleccionaaudio(3, 0.5f);
    }

    private void chatcommandreceived(object sender, OnChatCommandReceivedArgs e)
    {
        switch (e.Command.CommandText)
        {
            case "comandos":
                client.SendMessage(channel_name, "Lista de comandos\n !bonk, !honk, !yasuo, !headshot");
                break;
            case "honk":
                soundmanager.Seleccionaaudio(5, 1);
                StartCoroutine("esperar_honk");
                efectos[1].SetActive(true);
                break;
            case "bonk":
                soundmanager.Seleccionaaudio(0, 1);
                StartCoroutine("esperar_bonk");
                efectos[0].SetActive(true);
                break;
            case "yasuo":
                soundmanager.Seleccionaaudio(6, 1);
                break;
            case "headshot":
                soundmanager.Seleccionaaudio(4, 1);
                break;
            case "say":
                break;
            default:
                client.SendMessage(e.Command.ChatMessage.Channel, $"Unknown chat command: {e.Command.CommandIdentifier}{e.Command.CommandText}");
                break;
        }
    }


    //Accion que el bot realizara al conectarse a la sala del chat
    private void connect(object sender, OnConnectedArgs e)
    {
        client.SendMessage(channel_name, "I'm here");
    }

    IEnumerator esperar_honk()
    {
        yield return new WaitForSeconds(0.3f);
        efectos[1].SetActive(false);
    }

    IEnumerator esperar_bonk()
    {
        yield return new WaitForSeconds(0.5f);
        efectos[0].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
