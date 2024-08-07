namespace CardsServer.BLL.Infrastructure
{
    public class AssertModel
    {
        public async T? CheckNull(T? model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
        }
    }
}
