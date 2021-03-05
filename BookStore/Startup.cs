using BookStore.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // �������� ������ ����������� �� ����� ������������
            string connection = Configuration.GetConnectionString("DefaultConnection");
            // ��������� �������� MobileContext � �������� ������� � ����������
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

            services.AddScoped<Logger>(s => LogManager.GetCurrentClassLogger());

            services.AddControllersWithViews();
            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "My API",
                    Description = "ASP.NET Core Web API"
                });
            });

            services.AddControllers(); // ���������� ����������� ��� �������������
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            #region DataBase inicialization
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationContext>();
                context.Database.EnsureCreated();

                Author tolstoy = new Author()
                {
                    Name = "���",
                    Surname = "�������",
                    LastName = "����������",
                    FullName = "������� ��� ����������",
                    DateBirth = new DateTime(1828, 09, 09)
                };
                Author turgenev = new Author()
                {
                    Name = "����",
                    Surname = "��������",
                    LastName = "���������",
                    FullName = "�������� ���� ���������",
                    DateBirth = new DateTime(1818, 10, 28)
                };

                Author pushkin = new Author()
                {
                    Name = "���������",
                    Surname = "������",
                    LastName = "���������",
                    FullName = "������ ��������� ���������",
                    DateBirth = new DateTime(1799, 05, 26)
                };
                Author chehov = new Author()
                {
                    Name = "�����",
                    Surname = "�����",
                    LastName = "���������",
                    FullName = "����� ����� ��������",
                    DateBirth = new DateTime(1860, 01, 17)
                };

                Author lermontov = new Author()
                {
                    Name = "������",
                    Surname = "���������",
                    LastName = "�������",
                    FullName = "��������� ������ �������",
                    DateBirth = new DateTime(1814, 10, 15)
                };
                BookStatus exist = new BookStatus()
                {
                    Name = "� �������"
                };
                BookStatus sold = new BookStatus()
                {
                    Name = "�������"
                };
                BookStatus borrowed = new BookStatus()
                {
                    Name = "����������"
                };
                context.Books.Add(new Book
                {
                    Name = "����� � ���",
                    ReleaseDate = new DateTime(1867),
                    CreatedOn = DateTime.Today,
                    Author = tolstoy,
                    BookStatus = sold
                });
                context.Books.Add(new Book
                {
                    Name = "���� � ����",
                    ReleaseDate = new DateTime(1862),
                    CreatedOn = DateTime.Today,
                    Author = turgenev,
                    BookStatus = exist
                });
                context.Books.Add(new Book
                {
                    Name = "����������� �����",
                    ReleaseDate = new DateTime(1836),
                    CreatedOn = DateTime.Today,
                    Author = pushkin,
                    BookStatus = borrowed
                });
                context.Books.Add(new Book
                {
                    Name = "������ �6",
                    ReleaseDate = new DateTime(1892),
                    CreatedOn = DateTime.Today,
                    Author = chehov,
                    BookStatus = exist
                });
                context.Books.Add(new Book
                {
                    Name = "����� ������ �������",
                    ReleaseDate = new DateTime(1840),
                    CreatedOn = DateTime.Today,
                    Author = lermontov,
                    BookStatus = exist
                });
                if (!context.Books.Any() || !context.BookStatuses.Any() || !context.Authors.Any())
                {
                    context.SaveChanges();
                }
            }
            #endregion
        }
    }
}
