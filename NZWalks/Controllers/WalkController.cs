using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.CustomActionFilters;
using NZWalks.Models.Domain;
using NZWalks.Models.DTOs;
using NZWalks.Repository;

namespace NZWalks.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalkController : ControllerBase
    {
        private readonly IWalkRepository repository;
        private readonly IMapper mapper;

        public WalkController(IWalkRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllWalks()
        {
            var Walks = await repository.GetAllAsync();
            var WalksDto = mapper.Map<List<WalkDto>>(Walks);
            return Ok(WalksDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetWalkById([FromRoute] Guid id)
        {
            var getWalk = await repository.GetByIdAsync(id);
            if (getWalk == null)
            {
                return NotFound();
            }
            var walkDto = mapper.Map<WalkDto>(getWalk);
            return Ok(walkDto);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateWalk([FromBody] AddWalkRequestDto addRequestDto)
        {
            var walk = mapper.Map<Walk>(addRequestDto);
            walk = await repository.CreateAsync(walk);
            var addwalkDto = mapper.Map<WalkDto>(walk);
            return CreatedAtAction(nameof(GetWalkById), new { id = addwalkDto.Id }, addwalkDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateWalk([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateRequestDto)
        {
            var walk = mapper.Map<Walk>(updateRequestDto);
            walk = await repository.UpdateAsync(id, walk);
            if (walk == null)
            {
                return NotFound();
            }
            var walkDto = mapper.Map<WalkDto>(walk);
            return Ok(walkDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteWalk([FromRoute] Guid id)
        {
            var getWalk = await repository.DeleteAsync(id);
            if (getWalk == null)
            {
                return NotFound();
            }
            var walkDto = mapper.Map<WalkDto>(getWalk);
            return Ok(walkDto);
        }
    }
}