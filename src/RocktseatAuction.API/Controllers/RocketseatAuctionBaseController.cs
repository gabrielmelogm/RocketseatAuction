using Microsoft.AspNetCore.Mvc;
using RocketseatAuction.API.Filters;

namespace RocketseatAuction.API.Controllers;

[Route("[controller]")]
[ApiController]

[ServiceFilter(typeof(AuthenticationUserAttribute))]
public class RocketseatAuctionBaseController : ControllerBase {}