namespace CardsServer.BLL.Dto.Module
{
    public class GetModules : BaseSortModel
    {
        public bool AddElements { get; set; }
        public bool UserModules { get; set; }
        // Ограничение по количеству возвращаемых модулей. В случае отсутствия = 0
        public int Limit { get; set; }  
    }
}
