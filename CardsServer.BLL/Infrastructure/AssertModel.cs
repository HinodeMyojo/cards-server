namespace CardsServer.BLL.Infrastructure
{
    public class AssertModel
    {
        public static T CheckNull<T>(T? model) where T : class
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return model;
        }
    }
}
