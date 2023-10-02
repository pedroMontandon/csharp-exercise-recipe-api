using Microsoft.AspNetCore.Mvc;
using recipes_api.Services;
using recipes_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Security.Cryptography.X509Certificates;

namespace recipes_api.Controllers;

[ApiController]
[Route("recipe")]
public class RecipesController : ControllerBase
{    
    public readonly IRecipeService _service;
    
    public RecipesController(IRecipeService service)
    {
        this._service = service;        
    }

    [HttpGet]
    public IActionResult Get()
    {
        var recipes = this._service.GetRecipes();
        return Ok(recipes);
    }

    [HttpGet("{name}", Name = "GetRecipe")]
    public IActionResult Get(string name)
    {                
        var recipe = this._service.GetRecipe(name);
        return recipe != null ? Ok(recipe) : NotFound("Recipe not found");
    }


    [HttpPost]
    public IActionResult Create([FromBody]Recipe recipe)
    {
        this._service.AddRecipe(recipe);
        return CreatedAtRoute("GetRecipe", new { name = recipe.Name }, recipe);
    }

    [HttpPut("{name}")]
    public IActionResult Update(string name, [FromBody]Recipe recipe)
    {
        try 
        {
        var recipeToUpdate = this._service.GetRecipe(name);
        if (recipeToUpdate == null) return NotFound("Recipe not found");
        this._service.UpdateRecipe(recipe);
        return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // 5 - Sua aplicação deve ter o endpoint DEL /recipe
    [HttpDelete("{name}")]
    public IActionResult Delete(string name)
    {
        throw new NotImplementedException();
    }    
}
