// using CardsServer.BLL.Entity;
//
// namespace CardsServer.BLL.Dto.User
// {
//     public class GetUserFullResponse : GetUserSimpleResponse
//     {
//         public DateTime CreatedAt {  get; set; }
//         public required string Email { get; set; }
//         public static explicit operator GetUserFullResponse(UserEntity user)
//         {
//             GetUserSimpleResponse simpleResponse = (GetUserSimpleResponse)user;
//             return new GetUserFullResponse
//             {
//                 Id = simpleResponse.Id,
//                 UserName = simpleResponse.UserName,
//                 IsEmailConfirmed = simpleResponse.IsEmailConfirmed,
//                 StatusId = simpleResponse.StatusId,
//                 RoleId = simpleResponse.RoleId,
//                 AvatarId = simpleResponse.AvatarId,
//                 Avatar = simpleResponse.Avatar,
//
//                 CreatedAt = user.CreatedAt,
//                 Email = user.Email
//             };
//         }
//     }
// }
