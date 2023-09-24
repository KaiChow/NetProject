using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MyNet6.Services;
using MyNet6.Interfaces;
using Net6.WebApi.Utility;
using Net6.WebApi.Utility.Route;
using Net6.WebApi.Utility.Swagger;
using System.Text.Encodings.Web;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Conventions.Insert(0, new RouteConvention(new RouteAttribute("oms")));
}).AddJsonOptions((
    options) =>
{
    options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
});
#region 添加版本信息
builder.Services.AddApiVersioning(o =>
{

    o.ReportApiVersions = true;
    o.DefaultApiVersion = new ApiVersion(1, 0);
    o.AssumeDefaultVersionWhenUnspecified = true;
});
builder.Services.AddVersionedApiExplorer(o =>
{
    o.GroupNameFormat = "'v'VVV";
    o.SubstituteApiVersionInUrl = true;
});

#endregion

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    #region  调用第三方的程序包支持版本控制

    {
        var provider = builder.Services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, new OpenApiInfo
            {
                Contact = new OpenApiContact
                {
                    Name = "Kevin老师",
                    Email = "12313132@QQ.com"
                },
                Description = "文档的描述",
                Title = "文档的Title",
                Version = description.ApiVersion.ToString()
            });
        };
        options.DocInclusionPredicate((version, apiDescription) =>
        {
            if (!version.Equals(apiDescription.GroupName))
            {
                return false;
            }
            IEnumerable<string>? values = apiDescription!.RelativePath.Split('/').Select(v => v.Replace("v{version}", apiDescription.GroupName));
            apiDescription.RelativePath = string.Join("/", values);
            return true;
        });
        // 参数使用驼峰命名方式
        options.DescribeAllParametersInCamelCase();

    }
    #endregion
    #region 配置分版本的swagger
    /* typeof(ApiVersions).GetEnumNames().ToList().ForEach(verson =>
     {

         options.SwaggerDoc(verson, new OpenApiInfo()
         {
             Title = $"{verson}:Api文档",
             Version = verson,
             Description = $"通用版本的CoreApi版本{verson}"
         });
     });*/
    #endregion

    #region 配置展示注释
    {
        //xml文档的绝对路径
        var file = Path.Combine(AppContext.BaseDirectory, "Net6.WebApi.xml");
        // true 显示控制器注释
        options.IncludeXmlComments(file, true);
        //对action的名称进行排序 如果有多个，就可以看见效果
        options.OrderActionsBy(o => o.RelativePath);

    }
    #endregion

    #region 传入Token
    //添加安全定义
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "请输入token,格式为Bearer xxxxxxxx(注意中间必须有空格)",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    // 添加安全要求
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new List<string>()
        }
    });
    #endregion

    #region 扩展文件上传按钮
    {
        options.OperationFilter<FileUploadFilter>();
    }
    #endregion
});

/*builder.Services.AddMvc(opt =>
{
    opt.Conventions.Insert(0, new RouteConvention(new RouteAttribute("oms")));
});
*/

#region  注册抽象和具体之间的关系
builder.Services.AddTransient<ITestServiceA,TestServiceA>();
#endregion
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(op =>
    {
        /* foreach (var version in typeof(ApiVersions).GetEnumNames())
         {
             op.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"版本：{version}");
         }*/

        #region 使用第三方程序包的支持
        {
            var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
            foreach (var description in provider.ApiVersionDescriptions.Reverse())
            {
                op.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", $"测试API {description.GroupName.ToLowerInvariant()}");
            }
        }
        #endregion
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
