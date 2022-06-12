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

        [HttpGet("{id}")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            Participante p = null;
            using (IDbConnection conexao = ConnectionFactory.GetStringConexao(_config))
            {
                conexao.Open();

                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT ID as Id, ID_TIPO_PARTICIPANTE as TipoId, TX_NOME as Nome, TX_CPF as Cpf , ");
                sql.Append("TX_EMAIL as Email FROM TB_PARTICIPANTE WHERE ID = @Id ");

                p = await conexao.QueryFirstOrDefaultAsync<Participante>(sql.ToString(), new { Id = id });

                if (p != null)
                    return Ok(p);
                else
                    return NotFound("Participante não encontrado");
            }
        }

        [HttpPost]
        public async Task<ActionResult> InsertAsync(Participante p)
        {
            using (IDbConnection conexao = ConnectionFactory.GetStringConexao(_config))
            {
                conexao.Open();

                StringBuilder sql = new StringBuilder();
                sql.Append("INSERT INTO TB_PARTICIPANTE (ID_TIPO_PARTICIPANTE, TX_NOME, TX_CPF, TX_EMAIL) ");
                sql.Append("VALUES (@TipoId, @Nome, @Cpf, @Email) ");
                sql.Append("SELECT CAST(SCOPE_IDENTITY() AS INT) ");

                object o = await conexao.ExecuteScalarAsync(sql.ToString(), p);

                if (o != null)
                    p.Id = Convert.ToInt32(o);
            }
            return Ok(p);
        }


    }
}