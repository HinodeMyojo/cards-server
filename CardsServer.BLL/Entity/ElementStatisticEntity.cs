namespace CardsServer.BLL.Entity
{
    /// <summary>
    /// Модель статистики по элементу.
    /// Каждому пользователю, который добавил модуль с элементами - создается данный объект, куда собирается статистика
    /// </summary>
    public class ElementStatisticEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserEntity? User { get; set; }
        public int ElementId { get; set; }
        public ElementEntity? Element { get; set; }
        public int CorrectAnswers { get; set; }
        public int IncorrectAnswers { get; set; }
        // Последний ответ = 1 - правильный, 0 - неправильный
        public bool LastAnswer { get; set; }
    }
}
