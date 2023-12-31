using Microsoft.AspNetCore.Mvc;
using recipes_api.Services;
using recipes_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace recipes_api.Controllers;

[ApiController]
[Route("user")]
public class UserController : ControllerBase
{    
    public readonly IUserService _service;
    
    public UserController(IUserService service)
    {
        this._service = service;        
    }

    [HttpGet("{email}", Name = "GetUser")]
    public IActionResult Get(string email)
    {                
        var user = this._service.GetUser(email);
        return user != null ? Ok(user) : NotFound("User not found");
    }

    [HttpPost]
    public IActionResult Create([FromBody]User user)
    {
        this._service.AddUser(user);
        return CreatedAtRoute("GetUser", new { email = user.Email }, user);
    }

    [HttpPut("{email}")]
    public IActionResult Update(string email, [FromBody]User user)
    {
        try 
        {
        var userToUpdate = this._service.GetUser(email);
        if (userToUpdate == null) return NotFound("User not found");
        this._service.UpdateUser(user);
        return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{email}")]
    public IActionResult Delete(string email)
    {
        try 
        {
        var userToDelete = this._service.GetUser(email);
        if (userToDelete == null) return NotFound("User not found");
        this._service.DeleteUser(email);
        return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    } 
}