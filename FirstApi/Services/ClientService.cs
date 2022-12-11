using FirstApi.DataBase;
using FirstApi.Models.Client;
using FirstApi.Services.ServicesInterface;
using Microsoft.AspNetCore.Mvc;

namespace FirstApi.Services;

public class ClientService : IClientService
{
    private readonly UserRepository _userRepository;

    public ClientService(IConfiguration configuration)
    {
        _userRepository = new UserRepository(configuration.GetConnectionString("ClientAppCon"));
    }

    public IActionResult GetClient(int id)
    {
        if (id < 1)
            return new BadRequestObjectResult("Id cannot be less than 1");
        return _userRepository.GetClient(id);
    }

    public IActionResult AddClient(Client client)
    {
        throw new NotImplementedException();
    }

    public IActionResult EditClient(Client client)
    {
        throw new NotImplementedException();
    }

    public IActionResult DeleteClient(Client client)
    {
        throw new NotImplementedException();
    }
}