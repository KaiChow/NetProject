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
#region ��Ӱ汾��Ϣ
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
    #region  ���õ������ĳ����֧�ְ汾����

    {
        var provider = builder.Services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, new OpenApiInfo
            {
                Contact = new OpenApiContact
                {
                    Name = "Kevin��ʦ",
                    Email = "12313132@QQ.com"
                },
                Description = "�ĵ�������",
                Title = "�ĵ���Title",
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
        // ����ʹ���շ�������ʽ
        options.DescribeAllParametersInCamelCase();

    }
    #endregion
    #region ���÷ְ汾��swagger
    /* typeof(ApiVersions).GetEnumNames().ToList().ForEach(verson =>
     {

         options.SwaggerDoc(verson, new OpenApiInfo()
         {
             Title = $"{verson}:Api�ĵ�",
             Version = verson,
             Description = $"ͨ�ð汾��CoreApi�汾{verson}"
         });
     });*/
    #endregion

    #region ����չʾע��
    {
        //xml�ĵ��ľ���·��
        var file = Path.Combine(AppContext.BaseDirectory, "Net6.WebApi.xml");
        // true ��ʾ������ע��
        options.IncludeXmlComments(file, true);
        //��action�����ƽ������� ����ж�����Ϳ��Կ���Ч��
        options.OrderActionsBy(o => o.RelativePath);

    }
    #endregion

    #region ����Token
    //��Ӱ�ȫ����
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "������token,��ʽΪBearer xxxxxxxx(ע���м�����пո�)",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    // ��Ӱ�ȫҪ��
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

    #region ��չ�ļ��ϴ���ť
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

#region  ע�����;���֮��Ĺ�ϵ
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
             op.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"�汾��{version}");
         }*/

        #region ʹ�õ������������֧��
        {
            var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
            foreach (var description in provider.ApiVersionDescriptions.Reverse())
            {
                op.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", $"����API {description.GroupName.ToLowerInvariant()}");
            }
        }
        #endregion
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
