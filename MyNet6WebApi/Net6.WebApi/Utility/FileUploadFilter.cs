using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace Net6.WebApi.Utility
{
    /// <summary>
    /// 扩展文件上传，显示上传按钮
    /// </summary>
    public class FileUploadFilter : IOperationFilter
    {
        /// <summary>
        /// 重新实现文件的接口
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            //判断上传文件的类型，只有上传的类型是IFormCollection的才进行重写。
            if (context.ApiDescription.ActionDescriptor.Parameters.Any(w => w.ParameterType == typeof(IFormCollection)))
            {
                Dictionary<string, OpenApiSchema> schema = new Dictionary<string, OpenApiSchema>();
                schema["fileName"] = new OpenApiSchema { Description = "Select file", Type = "string", Format = "binary" };
                Dictionary<string, OpenApiMediaType> content = new Dictionary<string, OpenApiMediaType>();
                content["multipart/form-data"] = new OpenApiMediaType { Schema = new OpenApiSchema { Type = "object", Properties = schema } };
                operation.RequestBody = new OpenApiRequestBody() { Content = content };
            }
        }
    }
}