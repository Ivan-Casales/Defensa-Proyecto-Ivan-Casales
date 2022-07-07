using System.Collections.Generic;

namespace Battleship
{
    /// <summary>
    /// Una clase abstract que se encarga de contar los disparos
    /// realizados durante la partida.
    /// 
    /// De esta forma podemos reutilizar código, ya que está clase va a heredar clases que
    /// cuentes distintos tipos de disparos. Si un futuro queremos agregar nuevos contadores, 
    /// podemos hacerlo perfectamente.
    /// 
    /// Las clases de tipo ShotsInGame, son clases expertas en tener conocimiento de la cantidad
    /// de disparos
    /// 
    /// Las clases de este tipo van a estar contenidas dentro de ShotsCountHolder
    /// 
    /// No se crea una clase que contenga listas con todos los disparos directamente, por que 
    /// en el futuro se puede crear distintos métodos de obtención de disparos o agregación de disparos.
    /// 
    /// También se respeta el OCP, ya que la clase está abierta a extensiones y cerrada a modificaciones.
    /// </summary>
    public abstract class ShotsInGame
    {
        /// <summary>
        /// Contiene el número de disparos realizados
        /// </summary>
        protected int ShotsNumber = 0;

        /// <summary>
        /// Método que incrementa en 1 la cantidad de disparos realizados
        /// </summary>
        public void AddShot()
        {
            this.ShotsNumber ++;
        }

        /// <summary>
        /// Método que retorna la cantidad de disparos realizados
        /// </summary>
        /// <returns>Cantidad de disparos realizados</returns>
        public int GetShotsNumber()
        {
            return this.ShotsNumber;
        }
    }
}