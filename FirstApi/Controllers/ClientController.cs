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
        private readonly ClientService _clientService;
        
        public ClientController(IConfiguration configuration)
        {
            _clientService = new ClientService(configuration);
        }

        [HttpGet("GetClient")]
        public IActionResult GetClient(int id)
        {
            return _clientService.GetClient(id);
        }
        [HttpPost("AddClient")]
        public IActionResult AddClient(Client client)
        {
            return _clientService.AddClient(client);
        }

        [HttpPost("EditClient")]
        public IActionResult EditClient(Client client)
        {
            return _clientService.EditClient(client);
        }
        [HttpPost("DeleteClient")]
        public IActionResult DeleteClient(int id)
        {
            return _clientService.DeleteClient(id);
        }
    }
}
