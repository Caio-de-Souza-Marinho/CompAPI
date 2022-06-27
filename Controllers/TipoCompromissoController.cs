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

    public class TipoCompromissoController : Controller
    {
        private readonly IConfiguration _config;

        public TipoCompromissoController(IConfiguration config)
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
                sql.Append("SELECT ID as Id, TX_DESCRICAO as DescricaoCompromisso ");
                sql.Append("FROM TB_TIPO_COMPROMISSO ");

                List<TipoCompromisso> lista = (await conexao.QueryAsync<TipoCompromisso>(sql.ToString())).ToList();

                return Ok(lista);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            TipoCompromisso p = null;
            using (IDbConnection conexao = ConnectionFactory.GetStringConexao(_config))
            {
                conexao.Open();

                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT ID as Id, TX_DESCRICAO as DescricaoCompromisso ");
                sql.Append("FROM TB_TIPO_COMPROMISSO WHERE ID = @Id ");
                
                p = await conexao.QueryFirstOrDefaultAsync<TipoCompromisso>(sql.ToString(), new { Id = id });
                
                if (p != null)
                    return Ok(p);
                else
                    return NotFound("Tipo de Compromisso não encontrado.");
            }
        }

















    }
}