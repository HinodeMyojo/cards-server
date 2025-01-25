namespace CardsServer.BLL.Dto.Module
{
    public class GetModulesRequest : BaseSortModel
    {
        public bool AddElements { get; set; }
        public bool UserModules { get; set; }
        public bool AddCreatorAvatar { get; set; }
        public bool AddCreatorUserName { get; set; }
        // TODO
        public bool AddTags { get; set; }
        // TODO
        public bool AddCommentCount { get; set; }
        // Ограничение по количеству возвращаемых модулей. В случае отсутствия = 0
        public int Limit { get; set; }  
    }
}
