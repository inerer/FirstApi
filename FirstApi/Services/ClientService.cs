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
        return _userRepository.AddClient(client);
    }

    public IActionResult EditClient(Client client)
    {
        return _userRepository.EditClient(client);
    }

    public IActionResult DeleteClient(int id)
    {
        return _userRepository.DeleteClient(id);
    }
}