using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using MinimalAPI.Models;
using MinimalAPI.Services;
using System.Text;

namespace MinimalAPI.ApiEndPoints
{
    public static class AutenticationEndPoints
    {
        public static void MapAutenticationEndPoints(this WebApplication app)
        {
            app.MapPost("/login", [AllowAnonymous] (UserModel userModel, ITokenService tokenService) =>
            {
                if (userModel is null)
                {
                    return Results.BadRequest("Login nao encontrado");
                }

                if (userModel.UserName == "Kaue" && userModel.Password == "123@456#")
                {
                    var tokenString = tokenService.GerarToken(app.Configuration["Jwt:Key"],
                        app.Configuration["Jwt:Issuer"],
                        app.Configuration["Jwt: Audience"], userModel);

                    return Results.Ok(new { token = tokenString });
                }
                else return Results.BadRequest("Login Nao encontrado");
            });

        }
    }
}
