using RocketseatAuction.API.Contracts;
using RocketseatAuction.API.Entities;

namespace RocketseatAuction.API.UseCases.Actions.GetCurrent;

public class GetCurrentAuctionUseCase {
  private readonly IAuctionRepository _repository;

  public GetCurrentAuctionUseCase(IAuctionRepository repository) => _repository = repository;
  
  public Auction? Execute() {
    return _repository.GetCurrent();
  }
}
