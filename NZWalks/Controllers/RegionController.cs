using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Models.Domain;
using NZWalks.Models.DTOs;
using NZWalks.Repository;

namespace NZWalks.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegionController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var getRegions = await regionRepository.GetAllRegionsAsync();
            var RegionDtos = mapper.Map<List<RegionDto>>(getRegions);
            return Ok(RegionDtos);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult?> GetById([FromRoute] Guid id)
        {
            var getRegion = regionRepository.GetByIdAsync(id);
            if (getRegion == null)
            {
                return NotFound();
            }
            var regionDto = mapper.Map<RegionDto>(getRegion);
            return Ok(regionDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRegion([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);
            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult?> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto requestDto)
        {
            var region = mapper.Map<Region>(regionRepository);
            region = await regionRepository.UpdateAsync(id, region);
            if (region == null)
            {
                return NotFound();
            }
            requestDto = mapper.Map<UpdateRegionRequestDto>(region);
            return Ok(requestDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult?> DeleteRegion([FromRoute] Guid id)
        {
            var getregion = regionRepository.DeleteAsync(id);
            if (getregion == null)
            {
                return NotFound();
            }
            var regionDto = mapper.Map<RegionDto>(getregion);
            return Ok(regionDto);
        }
    }
}