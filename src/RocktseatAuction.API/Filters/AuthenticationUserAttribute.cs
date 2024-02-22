using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RocketseatAuction.API.Contracts;

namespace RocketseatAuction.API.Filters;

public class AuthenticationUserAttribute : AuthorizeAttribute, IAuthorizationFilter
{
  private readonly IUserRepository _repository;

  public AuthenticationUserAttribute(IUserRepository repository) => _repository = repository;

  public void OnAuthorization(AuthorizationFilterContext context) {
    try {
      var token = this.TokenOnRequest(context.HttpContext);

      var email = this.FromBase64ToString(token);

      var exist = _repository.ExistUserWithEmail(email);

      if (exist == false) {
        context.Result = new UnauthorizedObjectResult("E-mail not valid");
      }
    }
    catch (Exception ex) {
      context.Result = new UnauthorizedObjectResult(ex.Message);
    }
  }

  private string TokenOnRequest(HttpContext context) {
    var token = context.Request.Headers.Authorization.ToString();

    if (string.IsNullOrEmpty(token)) {
      throw new Exception("Token is missing");
    }

    return token["Bearer ".Length..];
  }

  private string FromBase64ToString(string base64) {
    var data = Convert.FromBase64String(base64);

    return System.Text.Encoding.UTF8.GetString(data);
  }
}