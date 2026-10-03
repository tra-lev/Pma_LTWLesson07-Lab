namespace ProductCategory
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews(options =>
            {
                // Thông báo lỗi model binding bằng tiếng Việt (vd: để trống / nhập chữ vào ô số)
                var messages = options.ModelBindingMessageProvider;
                messages.SetValueMustNotBeNullAccessor(_ => "Vui lòng nhập giá trị cho trường này.");
                messages.SetValueMustBeANumberAccessor(field => $"{field} phải là một số.");
                messages.SetAttemptedValueIsInvalidAccessor((value, field) => $"Giá trị '{value}' không hợp lệ cho trường {field}.");
                messages.SetMissingBindRequiredValueAccessor(field => $"Vui lòng nhập {field}.");
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
