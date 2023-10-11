using Ecommerce.Entities;
using ECommerce.API.Interface;
using ECommerce.API.Repository;
using ECommerce.Repository.UnitTests.Base;
using Xunit;

namespace ECommerce.Repository.UnitTests.WishLists;

public class WishListTests : BaseTests
{
    private readonly IWishListRepository _wishListRepository;

    public WishListTests()
    {
        _wishListRepository = new WishListRepository(DbContext);
    }

    [Fact]
    public async Task AddAsync_AddNewEntity_ReturnsSameEntity()
    {
        //Arrange
        int id = 1;
        int userId = 1;
        int priceId = 1;
        WishList wishList = new WishList
        {
            Id = id,
            UserId = userId,
            PriceId = priceId,
        };

        //Act
        WishList newWishList = await _wishListRepository.AddAsync(wishList, CancellationToken);

        //Assert
        Assert.Equal(id, newWishList.Id);
        Assert.Equal(userId, newWishList.UserId);
        Assert.Equal(priceId, newWishList.PriceId);
    }

    [Fact]
    public async Task DeleteAsync_DeleteOneEntity_ReturnDeletedEntity()
    {
        //Arrange
        int id = 1;
        int userId = 1;
        int priceId = 1;

        WishList wishList = new WishList
        {
            Id = id,
            UserId = userId,
            PriceId = priceId,
        };

        //Act
        await DbContext.WishLists.AddAsync(wishList, CancellationToken);
        await DbContext.SaveChangesAsync();
        var newWishList = _wishListRepository.DeleteAsync(wishList.Id, CancellationToken);

        //Assert
        Assert.Equal(id, newWishList.Id);
    }

    [Fact]
    public async Task DeleteAsync_DeleteNullEntity_ReturnFaulted()
    {
        //Arrange
        int falseId = 2;
        int id = 1;
        int userId = 1;
        int priceId = 1;

        WishList wishList = new WishList
        {
            Id = id,
            UserId = userId,
            PriceId = priceId,
        };

        //Act
        await DbContext.WishLists.AddAsync(wishList, CancellationToken);
        await DbContext.SaveChangesAsync();
        var newWishList = _wishListRepository.DeleteAsync(falseId, CancellationToken);

        //Assert
        Assert.Equal(TaskStatus.Faulted, newWishList.Status);
    }

    [Fact]
    public async Task EditAsync_EditOneEntity_ReturnUpdatedEntity()
    {
        //Arrange
        int id = 1;
        int userId = 1;
        int priceId = 1;
        int editUserId = 2;

        WishList wishList = new WishList
        {
            Id = id,
            UserId = userId,
            PriceId = priceId,
        };

        var editWishList = wishList;
        editWishList.UserId = editUserId;

        //Act
        await DbContext.WishLists.AddAsync(wishList, CancellationToken);
        await DbContext.SaveChangesAsync();
        var newWishList = _wishListRepository.UpdateAsync(editWishList, CancellationToken).Result;

        //Assert
        Assert.Equal(editUserId, newWishList.UserId);
        Assert.Equal(editWishList, newWishList);
    }

    [Fact]
    public async Task EditAsync_EditNotExistEntity_ThrowException()
    {
        //Arrange
        int falseId = 3;
        int id = 1;
        int userId = 1;
        int priceId = 1;

        WishList wishList = new WishList
        {
            Id = id,
            UserId = userId,
            PriceId = priceId,
        };

        var editWishList = new WishList() { Id = falseId };

        //Act
        await DbContext.WishLists.AddAsync(wishList, CancellationToken);
        await DbContext.SaveChangesAsync();
        Exception exception = null;
        try
        {
            await _wishListRepository.UpdateAsync(editWishList, CancellationToken.None, true);
        }
        catch (Exception ex)
        {
            exception = ex;
        }

        //Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public async Task GetAll_CountAllEntities_ReturnsTwoEntities()
    {
        //Arrange
        int firstId = 1;
        int secondId = 2;
        int userId = 1;
        int priceId = 1;

        WishList firstWishList = new WishList
        {
            Id = firstId,
            UserId = userId,
            PriceId = priceId,
        };

        WishList secondWishList = new WishList
        {
            Id = secondId,
            UserId = userId,
            PriceId = priceId,
        };

        await DbContext.WishLists.AddAsync(firstWishList, CancellationToken);
        await DbContext.WishLists.AddAsync(secondWishList, CancellationToken);
        await DbContext.SaveChangesAsync();


        //Act
        var newWishLists = await _wishListRepository.GetAll(CancellationToken);

        //Assert
        Assert.Equal(2, newWishLists.Count());
    }

    [Fact]
    public async Task GetByProductUser_AddNewEntity_ReturnsSameEntity()
    {
        //Arrange
        int id = 1;
        int userId = 1;
        int priceId = 1;
        int productId = 1;
        Price price = new Price() { Id = priceId, ProductId = productId };

        WishList wishList = new WishList
        {
            Id = id,
            UserId = userId,
            PriceId = priceId,
            Price = price
        };

        //Act
        await DbContext.WishLists.AddAsync(wishList, CancellationToken);
        await DbContext.SaveChangesAsync();
        var newWishList = await _wishListRepository.GetByProductUser(productId, userId, CancellationToken);

        //Assert
        Assert.Equal(id, newWishList.Id);
        Assert.Equal(price, newWishList.Price);
        Assert.Equal(productId, newWishList.Price!.ProductId);
    }

    [Fact]
    public async Task GetByIdWithInclude_AddTowEntity_ReturnsTwoViewModels()
    {
        //Arrange
        int firstId = 1;
        int secondId = 2;
        int userId = 1;
        var product = await GetProduct();

        WishList firstWishList = new WishList
        {
            Id = firstId,
            UserId = userId,
            PriceId = product.Prices!.FirstOrDefault()!.Id,
            Price = product.Prices!.FirstOrDefault()
        };

        WishList secondWishList = new WishList
        {
            Id = secondId,
            UserId = userId,
            PriceId = product.Prices!.FirstOrDefault()!.Id,
            Price = product.Prices!.FirstOrDefault()
        };

        await DbContext.WishLists.AddAsync(firstWishList, CancellationToken);
        await DbContext.WishLists.AddAsync(secondWishList, CancellationToken);
        await DbContext.SaveChangesAsync();

        //Act
        var wishListViewModels = await _wishListRepository.GetByIdWithInclude(userId, CancellationToken);

        //Assert
        Assert.Equal(2, wishListViewModels.Count);
    }
}
