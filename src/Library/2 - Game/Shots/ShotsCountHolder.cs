using System.Collections.Generic;

namespace Battleship
{
    /// <summary>
    /// La clase ShotsCountHolder, es la encargada de contener las clases del tipo
    /// ShotsInGame, de está forma se respeta el SRP, ya que si agregaramos está responsabilidad
    /// al Game, el mismo tendría una causa más de cambio.
    /// 
    /// La clase está contenida dentro de Game
    /// </summary>
    public class ShotsCountHolder
    {
        /// <summary>
        /// Contiene los disparos en el agua
        /// </summary>
        private ShotsInGame WaterShots = new ShotsWaterInGame();

        /// <summary>
        /// Contiene los disparos a barcos
        /// </summary>
        private ShotsInGame ShipsShots = new ShotsWaterInGame();

        /// <summary>
        /// Retorna los disparos en el agua
        /// </summary>
        /// <returns>ShotsInGame</returns>
        public ShotsInGame GetWaterShots()
        {
            return this.WaterShots;
        }

        /// <summary>
        /// Retorna los disparos en barcos
        /// </summary>
        /// <returns>ShotsInGame</returns>
        public ShotsInGame GetShipsShots()
        {
            return this.ShipsShots;
        }
    }
}