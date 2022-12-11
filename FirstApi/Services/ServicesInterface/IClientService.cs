using FirstApi.Models.Client;
using Microsoft.AspNetCore.Mvc;

namespace FirstApi.Services.ServicesInterface;

public interface IClientService
{
    public IActionResult GetClient(int id);
    public IActionResult AddClient(Client client);
    public IActionResult EditClient(Client client);
    public IActionResult DeleteClient(Client client);
}