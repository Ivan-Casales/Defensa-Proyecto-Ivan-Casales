using System.IO;
using System;
using System.Linq;
using Telegram.Bot.Types;

namespace Battleship
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "disparos".
    /// Se encarga de devolver el resúmen de los juegos jugados hasta el momento en forma de string
    /// </summary>
    public class ShotsInGameHandler : BaseHandler
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ShotsInGameHandler"/>. Esta clase procesa el mensaje "disparos".
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public ShotsInGameHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] {"disparos", "disparo"};
        }

        /// <summary>
        /// Procesa el mensaje "disparos" y retorna true; retorna false en caso contrario.
        /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        /// <returns>true si el mensaje fue procesado; false en caso contrario.</returns>
        protected override void InternalHandle(Message message, out string response)
        {
            try
            {
                User user = UserRegister.GetUser(message.From.Id);

                if (user.getStatus() != $"in {user.GetGameMode()} game")
                {
                    // Estado de user incorrecto
                    response = $"Comando incorrecto. Estado del usuario = {user.getStatus()}";
                    return;
                }
                else
                {
                    Game game = GamesRegister.GetGameByUserId(user.GetID());
                    int shotsNumber;
                    string shotType;

                    try
                    {
                        string[] text = message.Text.Split(" ");
                        shotType = text[1];
                    }
                    catch
                    {
                        response = "Por favor, ingrese un tipo de disparo válido luego de la palabra disparo('disparo agua' o 'disparo barcos')";
                        return;
                    }

                    /// Se verifica cual es la segunda palabra ingresada en el mensaje
                    if(shotType.ToLower() == "barcos")
                    {
                        shotsNumber = game.GetShotsCounter().GetShipsShots().GetShotsNumber();;
                        response = $"Hubieron {shotsNumber} disparos en barcos";
                    }
                    else if (shotType.ToLower() == "agua")
                    {
                        shotsNumber = game.GetShotsCounter().GetWaterShots().GetShotsNumber();;
                        response = $"Hubieron {shotsNumber} disparos al agua";
                    }
                    else
                    {
                        response = "Por favor, ingrese un tipo de disparo válido luego de la palabra disparo('disparo agua' o 'disparo barcos')";
                    }
                    
                }
            }
            catch(UserNotCreatedException)
            {
                response = "Debe crear un usuario\nIngrese 'Crear Usuario':\n";
            }
            catch
            {
                response = "Sucedió un error, vuelve a intentar";
            }
        }

        /// <summary>
        /// Modifico el Can Handle para que me acepte los mensajes que iniciar
        /// con la palabra disparos, luego en el internal Handle controlo la otra
        /// parte del mensaje
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected override bool CanHandle(Message message)
        {
            try
            {
                string[] words = message.Text.Split(' ');


                if (this.Keywords.Contains(words[0].ToLower()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            
        }
    }
}