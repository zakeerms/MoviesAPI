using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.DTOs;
using MoviesAPI.Entities;
using MoviesAPI.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Controllers
{
    [Route("api/movietheaters")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
    public class MovieTheatersController : ControllerBase
    {
        private readonly ApplicationContext context;
        private readonly IMapper mapper;

        public MovieTheatersController(ApplicationContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<MovieTheaterDTO>>> Get([FromQuery] PaginationDTO paginationDTO)
        {
            var queryable = context.MovieTheaters.AsQueryable();
            await HttpContext.InsertParametersPaginationInHeader(queryable);

            var genres = await queryable.OrderBy(x => x.Name).Paginate(paginationDTO).ToListAsync();
            return mapper.Map<List<MovieTheaterDTO>>(genres);
        }
        [HttpGet("{Id:int}")]
        public async Task<ActionResult<MovieTheaterDTO>> Get(int Id)
        {
            var movieTheater = await context.MovieTheaters.FirstOrDefaultAsync(x => x.Id == Id);
            if (movieTheater == null)
                return NotFound();
            return mapper.Map<MovieTheaterDTO>(movieTheater);
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] MovieTheaterCreationDTO movieTheaterCreationDTO)
        {
            var movieTheater = mapper.Map<MovieTheater>(movieTheaterCreationDTO);
            context.MovieTheaters.Add(movieTheater);
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("{Id:int}")]
        public async Task<ActionResult> Put(int Id, [FromBody] MovieTheaterCreationDTO movieTheaterCreationDTO)
        {
            var movieTheater = await context.MovieTheaters.FirstOrDefaultAsync(x => x.Id == Id);
            if (movieTheater == null)
                return NotFound();
            movieTheater = mapper.Map(movieTheaterCreationDTO, movieTheater);
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{Id:int}")]
        public async Task<ActionResult> Delete(int Id)
        {
            var movieTheater = await context.MovieTheaters.FirstOrDefaultAsync(x => x.Id == Id);
            if (movieTheater == null)
                return NotFound();
            context.MovieTheaters.Remove(movieTheater);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
