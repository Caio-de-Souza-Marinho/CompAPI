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
    [Route("[Controller]")]

    public class CompromissosController : ControllerBase
    {
        private readonly IConfiguration _config;

        public CompromissosController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAsync()
        {
            using (IDbConnection conexao = ConnectionFactory.GetStringConexao(_config))
            {
                conexao.Open();

                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT ID as Id, ID_TIPO_COMPROMISSO as TipoCompromisso, TX_DESCRICAO as Descricao, TX_LOCALIZACAO as Localizacao, ");
                sql.Append("DT_INICIO as DataInicio, DT_TERMINO as DataTermino, FL_VISIVEL as Visivel ");
                sql.Append("FROM TB_COMPROMISSO");

                List<Compromisso> lista = (await conexao.QueryAsync<Compromisso>(sql.ToString())).ToList();

                return Ok(lista);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            Compromisso c = null;
            using (IDbConnection conexao = ConnectionFactory.GetStringConexao(_config))
            {
                conexao.Open();
                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT ID as Id, ID_TIPO_COMPROMISSO as TipoCompromissoId, TX_DESCRICAO as Descricao, TX_LOCALIZACAO as Localizacao, ");
                sql.Append("DT_INICIO as DataInicio, DT_TERMINO as DataTermino, FL_VISIVEL as Visivel ");
                sql.Append("FROM TB_COMPROMISSO WHERE ID = @Id ");

                c = await conexao.QueryFirstOrDefaultAsync<Compromisso>(sql.ToString(), new { Id = id });

                if (c != null)
                    return Ok(c);
                else
                    return NotFound("Compromisso não encontrado.");
            }
        }















    }
}