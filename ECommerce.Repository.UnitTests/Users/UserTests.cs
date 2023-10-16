using Ecommerce.Entities;
using ECommerce.API.Interface;
using ECommerce.API.Repository;
using ECommerce.Repository.UnitTests.Base;
using Xunit;

namespace ECommerce.Repository.UnitTests.Users;

public class UserTests : BaseTests
{
    private readonly IUserRepository _userRepository;
    public UserTests()
    {
        _userRepository = new UserRepository(DbContext);
    }

    [Fact]
    public async Task AddAsync_AddNewEntity_ReturnsSameEntity()
    {
        //Arrange
        int id = 2;
        string userName = Guid.NewGuid().ToString();
        string firstName = Guid.NewGuid().ToString();
        string lastName = Guid.NewGuid().ToString();
        User user = new User()
        {
            Id = id,
            UserName = userName,
            FirstName = firstName,
            LastName = lastName
        };

        //Act
        User newUser = await _userRepository.AddAsync(user, CancellationToken);

        //Assert
        Assert.Equal(id, newUser.Id);
        Assert.Equal(userName, newUser.UserName);
        Assert.Equal(firstName, newUser.FirstName);
    }

    [Fact]
    public async Task GetAll_CountAllEntities_ReturnsTwoEntities()
    {
        //Arrange
        int id = 3;
        string userName = Guid.NewGuid().ToString();
        string firstName = Guid.NewGuid().ToString();
        string lastName = Guid.NewGuid().ToString();
        User user = new User()
        {
            Id = id,
            UserName = userName,
            FirstName = firstName,
            LastName = lastName
        };
        await DbContext.AddAsync(user, CancellationToken);
        await DbContext.SaveChangesAsync(CancellationToken);

        //Act
        var newUsers = await _userRepository.GetAll(CancellationToken);

        //Assert
        Assert.Equal(2, newUsers.Count());
    }

    [Fact]
    public async Task? GetByEmailOrUserName_GetEntityByInputParam_ReturnsSameEntity()
    {
        //Arrange
        int id = 2;
        string userName = Guid.NewGuid().ToString();
        string email = Guid.NewGuid().ToString();
        User user = new User()
        {
            Id = id,
            UserName = userName,
            Email = email
        };

        await DbContext.AddAsync(user,CancellationToken);
        await DbContext.SaveChangesAsync(CancellationToken);

        //Act
        var newUserByUserName = await _userRepository.GetByEmailOrUserName(userName, CancellationToken);
        var newUserByEmail = await _userRepository.GetByEmailOrUserName(email, CancellationToken);

        //Assert
        Assert.Equal(id, newUserByUserName!.Id);
        Assert.Equal(id, newUserByEmail!.Id);
    }

    [Fact]
    public async Task? GetByPhoneNumber_GetEntityByInputParam_ReturnsSameEntity()
    {
        //Arrange
        int id = 2;
        string userName = Guid.NewGuid().ToString();
        string phoneNumber = Guid.NewGuid().ToString();
        User user = new User()
        {
            Id = id,
            UserName = userName,
            PhoneNumber = phoneNumber
        };

        await DbContext.AddAsync(user, CancellationToken);
        await DbContext.SaveChangesAsync(CancellationToken);

        //Act
        var newUser = await _userRepository.GetByPhoneNumber(phoneNumber, CancellationToken);

        //Assert
        Assert.Equal(id, newUser.Id);
    }

    [Fact]
    public async Task? Exists_GetExistEntityByParam_ReturnTrue()
    {
        //Arrange
        int newId = 3;
        int id = 2;
        string userName = Guid.NewGuid().ToString();
        string phoneNumber = Guid.NewGuid().ToString();
        string email = Guid.NewGuid().ToString();
        User user = new User()
        {
            Id = id,
            UserName = userName,
            PhoneNumber = phoneNumber,
            Email = email
        };

        await DbContext.AddAsync(user, CancellationToken);
        await DbContext.SaveChangesAsync(CancellationToken);

        //Act
        var result = await _userRepository.Exists(newId, email, phoneNumber, CancellationToken);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public async Task? GetUserRoles_GetExistEntityByParam_ReturnTrue()
    {
        //Arrange
        int id = 3;
        int roleId = 3;
        string userName = Guid.NewGuid().ToString();
        UserRole role = new UserRole()
        {
            Id = roleId,
        };
        User user = new User()
        {
            Id = id,
            UserName = userName,
            UserRole = role
        };
        
        await DbContext.AddAsync(role, CancellationToken);
        await DbContext.AddAsync(user, CancellationToken);
        await DbContext.SaveChangesAsync(CancellationToken);

        //Act
        var userRoles = await _userRepository.GetUserRoles(id, CancellationToken);

        //Assert
        Assert.Null(userRoles);
    }
}
