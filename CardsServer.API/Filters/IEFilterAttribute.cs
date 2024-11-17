//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;
//using System.Text.RegularExpressions;

//namespace CardsServer.API.Filters
//{
//    public class IEFilterAttribute : Attribute, IAsyncResourceFilter
//    {
//        /// <summary>
//        /// Фильтр ресурсов - не дает совершить запрос со старого IE
//        /// </summary>
//        /// <param name="context"></param>
//        /// <param name="next"></param>
//        /// <returns></returns>
//        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
//        {
//            string userBrowser = context.HttpContext.Request.Headers["User_agent"].ToString();
//            if (Regex.IsMatch(userBrowser, "MSIE|Trident"))
//            {
//                context.Result = new ContentResult { Content = "Ваш браузер устарел" };
//            }
//            else
//            {
//                await next();
//            }
//        }
//    }
//}
