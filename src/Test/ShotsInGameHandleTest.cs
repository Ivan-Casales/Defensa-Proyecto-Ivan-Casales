using NUnit.Framework;
using Battleship;
using Telegram.Bot.Types;
using System;

namespace Library.Tests
{
    /// <summary>
    /// Testea el Handler que está encarga de retornar el número de disparos de un tipo
    /// específico realizado durante una partida
    /// </summary>
    [TestFixture]
    public class ShotsWaterHandleTest
    {
        private ShotsInGameHandler waterShotsHandle;
        private AttackHandle attackHandler;
        private Message message;
        private Battleship.User user1;
        private Battleship.User user2;
        private Telegram.Bot.Types.User userTelegram1;
        private Telegram.Bot.Types.User userTelegram2;

        private SearchGameHandler sgameh;

        private PositionShipsHandle pshiph;

        private IPrinter Printer;

        [SetUp]
        public void Setup()
        {
            Printer = new ConsolePrinter();

            waterShotsHandle = new ShotsInGameHandler(null);
            
            attackHandler = new AttackHandle(null, Printer);

            sgameh = new SearchGameHandler(null, Printer);

            pshiph = new PositionShipsHandle(null, Printer);

            message = new Message();

            UserRegister.CreateUser(7);
            UserRegister.CreateUser(8);
            
            user1 = UserRegister.GetUser(7);
            user2 = UserRegister.GetUser(8);

            string response;
            IHandler result;

            userTelegram1 = new Telegram.Bot.Types.User();
            userTelegram1.Id = 7;

            userTelegram2 = new Telegram.Bot.Types.User();
            userTelegram2.Id = 8;

            message.From = userTelegram1;
            message.Text = "buscar partida";
            
            sgameh.Handle(message, out response);

            message.From = userTelegram2;
            message.Text = "buscar partida";

            sgameh.Handle(message, out response);

            for (int i = 1; i < 5; i++)
            {
                message.Text = $"posicionar barco a{i} down";
                result = pshiph.Handle(message, out response);
            }  

            message.From = userTelegram1;

            for (int i = 1; i < 5; i++)
            {
                message.Text = $"posicionar barco a{i} down";
                result = pshiph.Handle(message, out response);
            }  
        }


        /// <summary>
        /// Testeamos disparos en agua
        /// </summary>
        [Test]
        public void TestWaterShots()
        {
            message.Text = "atacar j9"; // Agua intencionado
            message.From = userTelegram2;

            if (user2.GetPlayer().GetTurn() == false)
            {
                user2.GetPlayer().ChangeTurn();
            }

            string response;

            IHandler result = attackHandler.Handle(message, out response);

            // Confirmamos que el disparo fue Agua
            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("Agua\n\n\n\n------Turno cambiado------\n\n"));

            message.Text = "disparos agua";

            result = waterShotsHandle.Handle(message, out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("Hubieron 1 disparos al agua"));


            // Volvemos a disparar agua con el otro usuario para comprobar nuevamente que funcionó
            message.Text = "atacar j8"; // Agua intencionado
            message.From = userTelegram1;

            result = attackHandler.Handle(message, out response);


            if (user1.GetPlayer().GetTurn() == false)
            {
                user1.GetPlayer().ChangeTurn();
            }

            Assert.That(response, Is.EqualTo("Agua\n\n\n\n------Turno cambiado------\n\n"));

            message.Text = "disparos agua";

            result = waterShotsHandle.Handle(message, out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("Hubieron 2 disparos al agua"));

        }

        /// <summary>
        /// Testeamos el disparo en barcos
        /// </summary>
        [Test]
        public void TestShipsShots()
        {
            message.Text = "atacar a1"; // Agua intencionado
            message.From = userTelegram2;

            if (user2.GetPlayer().GetTurn() == false)
            {
                user2.GetPlayer().ChangeTurn();
            }

            string response;

            IHandler result = attackHandler.Handle(message, out response);

            // Confirmamos que el disparo fue Tocado
            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("Tocado\n\n\n\n------Turno cambiado------\n\n"));

            message.Text = "disparos barcos";

            result = waterShotsHandle.Handle(message, out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("Hubieron 1 disparos en barcos"));


            // Volvemos a disparar Tocado con el otro usuario para comprobar nuevamente que funcionó
            message.Text = "atacar a2"; // Tocado intencionado
            message.From = userTelegram1;

            result = attackHandler.Handle(message, out response);

            Assert.That(response, Is.EqualTo("Tocado\n\n\n\n------Turno cambiado------\n\n"));

            message.Text = "disparos barcos";

            result = waterShotsHandle.Handle(message, out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("Hubieron 2 disparos en barcos"));

        }


        /// <summary>
        /// Test realizado para cuando el usuario no indica correctamente el tipo de disparo
        /// </summary>
         [Test]
        public void TestInvalidShot()
        {
            string response;

            message.Text = "disparos blabla"; // Ponemos una segunda palabra incorrecta

            IHandler result = waterShotsHandle.Handle(message, out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("Por favor, ingrese un tipo de disparo válido luego de la palabra disparo('disparo agua' o 'disparo barcos')"));
        }

    }
}