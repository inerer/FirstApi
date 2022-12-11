using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstApi.Models.Client;
using FirstApi.Services;
using FirstApi.Services.ServicesInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase, IClientService
    {
        private ClientService _clientService;

        public ClientController(IConfiguration configuration)
        {
            _clientService = new ClientService(configuration);
        }

        public IActionResult GetClient(int id)
        {
            return _clientService.GetClient(id);
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
}
