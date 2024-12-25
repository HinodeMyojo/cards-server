using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Dto.Image;
using CardsServer.BLL.Entity;
using CardsServer.BLL.Infrastructure.Result;
using CardsServer.DAL.Repository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardsServer.BLL.Services
{
    public sealed class ImageService : IImageService
    {
        private IImageRepository _repository;

        public ImageService(IImageRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> UploadImage(CreateImage model, CancellationToken cancellationToken)
        {
            if (model.Image == null)
            {
                Result.Failure("Не заданы изображения для загрузки");
            }
            
            try
            {
                byte[] byteImage = Convert.FromBase64String(model.Image);

                //ElementImageEntity image = new()
                //{
                //    Data = byteImage,
                //    UserId = model.UserId,
                //};
                //await _repository.AddImage(image);

                return Result.Success();

                //var bytes = GetBytes(files.FirstOrDefault)
                //ImageEntity image = new(info);
                //await _repository.AddImage()
            }
            catch(Exception ex)
            {
                return Result.Failure(ex.Message);
            }

        }

        public async Task<Result<string>> GetImage(int id, CancellationToken cancellationToken)
        {
            byte[]? image = await _repository.GetImage(id, cancellationToken);
            if (image != null)
            {
                string result = Convert.ToBase64String(image);
                return Result<string>.Success(result);
            }
            return Result<string>.Failure("Не удалось найти изображение по id");

        }

        //        foreach (var file in files)
        //{
        //  if (file.Length > 0)
        //  {
        //    using (var ms = new MemoryStream())
        //    {
        //      file.CopyTo(ms);
        //        var fileBytes = ms.ToArray();
        //        string s = Convert.ToBase64String(fileBytes);
        //        // act on the Base64 data
        //    }
        //}
        //}

        //public static class FormFileExtensions
        //{
        //    public static async Task<byte[]> GetBytes(this IFormFile formFile)
        //    {
        //        await using var memoryStream = new MemoryStream();
        //        await formFile.CopyToAsync(memoryStream);
        //        return memoryStream.ToArray();
        //    }
        //}


        //public async Task<Result<ImageEntity>> GetImage(int id, CancellationToken cancellationToken)
        //{

        //}

        public static async Task<byte[]> GetBytes(IFormFile file)
        {
            if (file != null) 
            { 
                await using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
            throw new NotImplementedException();
        }
    }
}
