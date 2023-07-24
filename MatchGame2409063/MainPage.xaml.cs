using System.Threading;
using System.Timers;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using System;


namespace MatchGame2409063;

public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage()
    {
        InitializeComponent();
        SetUpGame();
        reiniciarControles();
        Grid1.IsEnabled = false;
    }

    private void SetUpGame()
    {
        List<string> animaEmoji = new List<string>()
        {
            "🐶","🐶",
            "🙈","🙈",
            "🦑","🦑",
            "🐘","🐘",
            "🦓","🦓",
            "🦒","🦒",
            "🐍","🐍",
            "🐬","🐬",
        };

        Random random = new Random();

        foreach (Button view in Grid1.Children)
        {
            int index = random.Next(animaEmoji.Count);

            string nextEmoji = animaEmoji[index];
            view.Text = nextEmoji;
            animaEmoji.RemoveAt(index);
        }
    }

    Button ultimoButtonClicked;

    bool encontrandoMatch = false;
    private void Button_Clicked(object sender, EventArgs e)
    {
        Button button = sender as Button;

        if (encontrandoMatch == false)
        {
            button.IsVisible = false;
            ultimoButtonClicked = button;
            encontrandoMatch = true;
        }
        else if (button.Text == ultimoButtonClicked.Text)
        {
            button.IsVisible = false;
            encontrandoMatch = false;
        }
        else
        {
            ultimoButtonClicked.IsVisible = true;
            encontrandoMatch = false;
        }

    }

    //modificaciones
    private System.Timers.Timer gameTimer;
    private int segTranscurridos;
    private void TimerInicio()
    {
        gameTimer = new System.Timers.Timer(1000); 
        gameTimer.Elapsed += cuentaTiempo;
        gameTimer.AutoReset = true;
        gameTimer.Enabled = true;
    }

    private void cuentaTiempo(object sender, ElapsedEventArgs e)
    {
        segTranscurridos++;
        Device.BeginInvokeOnMainThread(() =>
        {
            tiempoLabel();
        });
    }
    private void tiempoLabel()
    {
        // Mostrar el tiempo jugando en el Label llamado "timeLabel"
        lblTiempo.Text = $"Tiempo: {segTranscurridos} milisegundos";
    }


    private void startBtn_Clicked(object sender, EventArgs e)
    {
        
        TimerInicio();
        startBtn.Text = "Iniciar juego";
        stopBtn.IsEnabled = true;
        restartBtn.IsEnabled = false;
        Grid1.IsEnabled = true;


    }

    private void stopBtn_Clicked(object sender, EventArgs e)
    {
        gameTimer.Enabled = false;
        startBtn.Text = "Reanudar juego";
        restartBtn.IsEnabled = true;
    }

    private void restartBtn_Clicked(object sender, EventArgs e)
    {
        reiniciarControles();
        SetUpGame();

        foreach (Button view in Grid1.Children)
        {
            view.IsVisible = true;
        }

        segTranscurridos = 0;
        tiempoLabel();
        gameTimer.Enabled = false;
        Grid1.IsEnabled = false;
        startBtn.IsEnabled = true;

    }

    private void wintBtn_Clicked(object sender, EventArgs e)
    {

        
        
            SetUpGame();

            foreach (Button view in Grid1.Children)
            {
                view.IsVisible = true;
            }

            gameTimer.Enabled = false;
            tiempoLabel();
            reiniciarControles();
            restartBtn.IsEnabled = true;
            startBtn.IsEnabled = false;
            double segundos = segTranscurridos / 60;
        Grid1.IsEnabled = false;
        DisplayAlert("FELICIDADES", $"TERMINASTE EL JUEGO\nTU TIEMPO: {segundos} SEGUNDOS", "ACEPTAR");
    }

    private void reiniciarControles()
    {
       
        stopBtn.IsEnabled = false;
        startBtn.Text = "Iniciar juego";
        restartBtn.IsEnabled = false;
    }

}

