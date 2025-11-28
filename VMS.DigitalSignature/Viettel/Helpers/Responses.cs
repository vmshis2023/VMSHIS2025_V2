using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace VMS.ChuKySo.Api.Helpers
{
    public interface IResponse
    {
        string Message { get; set; }

        bool Success { get; set; }

        string ErrorMessage { get; set; }
    }

    public interface ISingleResponse<TModel> : IResponse
    {
        TModel Data { get; set; }
    }

    public interface IListResponse<TModel> : IResponse
    {
        IEnumerable<TModel> Data { get; set; }
    }

    public interface IPagedResponse<TModel> : IListResponse<TModel>
    {
        int ItemsCount { get; set; }

        double PageCount { get; }
    }

    public class Response : IResponse
    {
        public string Message { get; set; }

        public bool Success { get; set; }

        public string ErrorMessage { get; set; }
    }

    public class SingleResponse<TModel> : ISingleResponse<TModel>
    {
        public string Message { get; set; }
        public string SAD { get; set; }

        public bool Success { get; set; }

        public string ErrorMessage { get; set; }

        public TModel Data { get; set; }
    }

    public class ListResponse<TModel> : IListResponse<TModel>
    {
        public string Message { get; set; }

        public bool Success { get; set; }

        public string ErrorMessage { get; set; }

        public IEnumerable<TModel> Data { get; set; }
    }

    public class PagedResponse<TModel> : IPagedResponse<TModel>
    {
        public string Message { get; set; }

        public bool Success { get; set; }

        public string ErrorMessage { get; set; }

        public IEnumerable<TModel> Data { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public int ItemsCount { get; set; }

        public double PageCount
            => ItemsCount < PageSize ? 1 : (int)(((double)ItemsCount / PageSize) + 1);
    }

    public static class ResponseExtensions
    {
        public static IActionResult ToHttpResponse(this IResponse response)
        {
            var status = HttpStatusCode.OK;
            if (string.IsNullOrEmpty(response.Message))
            {
                response.Message = response.Success ? "Successfully" : "There was an internal error, please contact to technical support.";
            }

            return new ObjectResult(response)
            {
                StatusCode = (int)status
            };
        }

        public static IActionResult ToHttpResponse<TModel>(this ISingleResponse<TModel> response)
        {
            var status = HttpStatusCode.OK;

            //if (!response.Success)
            //    status = HttpStatusCode.InternalServerError;
            //else 
            //if (response.Data == null)
            //    status = HttpStatusCode.NotFound;

            return new ObjectResult(response)
            {
                StatusCode = (int)status
            };
        }

        public static IActionResult ToHttpResponse<TModel>(this IListResponse<TModel> response)
        {
            var status = HttpStatusCode.OK;

            //if (!response.Success)
            //    status = HttpStatusCode.InternalServerError;
            //else 
            //if (response.Data == null)
            //    status = HttpStatusCode.NoContent;

            return new ObjectResult(response)
            {
                StatusCode = (int)status
            };
        }
    }
}
