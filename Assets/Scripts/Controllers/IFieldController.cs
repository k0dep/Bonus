using Models;

namespace Controllers
{
    /// <summary>
    /// Интерфейс контроллера игрового поля
    /// </summary>
    public interface IFieldController
    {
        /// <summary>
        /// Инициализация поля
        /// </summary>
        void Initialize();

        /// <summary>
        /// Обновление логики сгорания шаров(возможных)
        /// </summary>
        void UpdateField();

        /// <summary>
        /// Добавить новых сущностей
        /// </summary>
        void SpawnEntities();

        /// <summary>
        /// Попытка контролировать положение сущности(сдвинуть)
        /// </summary>
        /// <param name="IsRight">Определяет, является ли сторона сдвига вправо</param>
        void MoveEntity(bool IsRight);
    }
}