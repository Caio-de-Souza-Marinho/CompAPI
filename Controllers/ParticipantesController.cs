using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Collections.Generic;
using CompAPI.Models;
using System.Data;
using CompAPI.DAL;
using System.Text;
using Dapper;
using System.Linq;
using System;

namespace CompAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ParticipantesController : ControllerBase
    {
       private readonly IConfiguration _config;

       public ParticipantesController(IConfiguration config)
       {
           _config = config;
       }  

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        using (IDbConnection conexao = ConnectionFactory.GetStringConexao(_config))
        {
            conexao.Open();

            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ID as Id, ID_TIPO_PARTICIPANTE as TipoId, TX_NOME as Nome, ");
            sql.Append("TX_CPF as Cpf, TX_EMAIL as Email ");
            sql.Append("FROM TB_PARTICIPANTE ");

            List<Participante> lista = (await conexao.QueryAsync<Participante>(sql.ToString())).ToList();

            return Ok(lista);
        }
    }
 




    }
}