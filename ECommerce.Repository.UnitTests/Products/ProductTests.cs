using Ecommerce.Entities;
using ECommerce.API.Interface;
using ECommerce.API.Repository;
using ECommerce.Repository.UnitTests.Base;
using Microsoft.EntityFrameworkCore;
using Xunit;
using FluentAssertions;
using System.Text.Json;

namespace ECommerce.Repository.UnitTests.Products;

public class ProductTests : BaseTests
{
    private readonly IProductRepository _productRepository;
    private readonly Dictionary<string, Dictionary<string, Product>> _testSets = new();

    // Add
    // AddAsync
    // AddAll
    // AddRange
    // AddRangeAsync
    // GetAll
    // GetById
    // Delete
    // DeleteAsync
    // DeleteRange
    // DeleteRangeAsync
    // GetByIdAsync
    // GetByName
    // GetByUrl
    // TODO: AddWithRelations
    // TODO: Attach
    // TODO: Detach
    // TODO: EditWithRelations
    // TODO: GetAllProducts
    // TODO: GetAllWithInclude
    // TODO: GetByCategoryId
    // TODO: GetByIdWithInclude
    // TODO: GetCategoryProductCount
    // TODO: GetProductById
    // TODO: GetProductByIdWithInclude
    // TODO: GetProductList
    // TODO: GetProductListWithAttributes
    // TODO: GetProducts
    // TODO: GetProductsIdWithCategories
    // TODO: GetWithInclude
    // TODO: LoadCollection
    // TODO: LoadCollectionAsync
    // TODO: LoadReference
    // TODO: LoadReferenceAsync
    // TODO: SaveChangesAsync
    // TODO: Search
    // TODO: TopChip
    // TODO: TopNew
    // TODO: TopPrices
    // TODO: TopRelatives
    // TODO: TopSells
    // TODO: TopStars
    // TODO: Update
    // TODO: UpdateAsync
    // TODO: UpdateRange
    // TODO: UpdateRangeAsync
    // TODO: Where
    public ProductTests()
    {
        _productRepository = new ProductRepository(DbContext, HolooDbContext);

        _testSets.Add("null_test", new() {
            {"null_object", null!}
        });
        _testSets.Add("required_fields", new() {
            {"required_Name", new Product {Id = 1, MinOrder = 10, Url = "some random-url/w.[]"}},
            {"required_MinOrder", new Product {Id = 2, Name = "this has name", Url = "some random-url/w.[/\\:D]"}},
            {"required_Url", new Product {Id = 3, Name = "this also has name", MinOrder = 2}},
        });
        _testSets.Add("duplicate_url", new() {
            {"test_1", new Product
                {
                    Id = 1,
                    Name = "this has name",
                    IsShowInIndexPage = true,
                    Description = new string('*', 500000),
                    Review = "I'll let this one be short :>",
                    Star = -2, // should it accept this?!
                    MinOrder = 10,
                    MaxOrder = 255,
                    MinInStore = -10, // should it accept this?!
                    ReorderingLevel = 20,
                    IsDiscontinued = true,
                    IsActive = true,
                    Url = "some random-url/w.[/\\:D]"
                }
            },
            {"test_2", new Product
                {
                    Id = 2,
                    Name = "this name",
                    IsShowInIndexPage = true,
                    Description = "This time I'll let this one be short :>",
                    Review = new string('*', 500000),
                    Star = 2,
                    MinOrder = 10,
                    MaxOrder = 255,
                    MinInStore = 3,
                    ReorderingLevel = 20,
                    IsDiscontinued = true,
                    IsActive = true,
                    Url = "some random-url/w.[/\\:D]" // should it accept the same url as test_1?!
                }
            },
        });
        _testSets.Add("unique_url", new() {
            {"test_1", new Product
                {
                    Id = 398,
                    Name = "this has name",
                    MinOrder = 10,
                    Url = "some random-url/w.[/\\:D]"
                }
            },
            {"test_2", new Product
                {
                    Id = -92764,
                    Name = "this name",
                    MinOrder = 10,
                    Url = "random-url"
                }
            },
            {"test_3", new Product
                {
                    Id = 420,
                    Name = "such product",
                    MinOrder = 69,
                    Url = "much wow"
                }
            },
        });
    }

    private static Product CopyProduct(Product product)
    {
        return (Product)JsonSerializer.Deserialize(JsonSerializer.Serialize(product), typeof(Product));
    }

    private static IEnumerable<Product> CopyProductsList(IEnumerable<Product?> products)
    {
        return (IEnumerable<Product>)JsonSerializer.Deserialize(JsonSerializer.Serialize(products), typeof(IEnumerable<Product>));
    }

    #region required fields

    [Fact(DisplayName = "Add: Null value for required Fields")]
    public void Add_RequiredFields_ThrowsException()
    {
        // Arrange
        Dictionary<string, Product> expected = _testSets["required_fields"];

        // Act
        Dictionary<string, Action> actual = new();
        foreach (KeyValuePair<string, Product> entry in expected)
        {
            actual.Add(entry.Key, () => _productRepository.Add(entry.Value));
        }

        // Assert
        foreach (var action in actual.Values)
        {
            Assert.Throws<DbUpdateException>(action);
        }
    }

    [Fact(DisplayName = "AddAsync: Null value for required Fields")]
    public void AddAsync_RequiredFields_ThrowsException()
    {
        // Arrange
        Dictionary<string, Product> expected = _testSets["required_fields"];

        // Act
        Dictionary<string, Func<Task<Product>>> actual = new();
        foreach (KeyValuePair<string, Product> entry in expected)
        {
            actual.Add(entry.Key, () => _productRepository.AddAsync(entry.Value, CancellationToken));
        }

        // Assert
        foreach (var action in actual.Values)
        {
            Assert.ThrowsAsync<DbUpdateException>(action);
        }
    }

    [Fact(DisplayName = "AddAll: Null value for required Fields")]
    public void AddAll_RequiredFields_ThrowsException()
    {
        // Arrange
        Dictionary<string, Product> expected = _testSets["required_fields"];

        // Act
        Task<int> actual() => _productRepository.AddAll(expected.Values, CancellationToken);

        // Assert
        Assert.ThrowsAsync<DbUpdateException>(actual);
    }

    [Fact(DisplayName = "AddRange: Null value for required Fields")]
    public void AddRange_RequiredFields_ThrowsException()
    {
        // Arrange
        Dictionary<string, Product> expected = _testSets["required_fields"];

        // Act
        void actual() => _productRepository.AddRange(expected.Values);

        // Assert
        Assert.Throws<DbUpdateException>(actual);
    }

    [Fact(DisplayName = "AddRangeAsync: Null value for required Fields")]
    public void AddRangeAsync_RequiredFields_ThrowsException()
    {
        // Arrange
        Dictionary<string, Product> expected = _testSets["required_fields"];

        // Act
        Task actual() => _productRepository.AddRangeAsync(expected.Values, CancellationToken);

        // Assert
        Assert.ThrowsAsync<DbUpdateException>(actual);
    }

    #endregion
    #region null object

    [Fact(DisplayName = "Add: Null product")]
    public void Add_NullProduct_ThrowsException()
    {
        // Arrange
        Dictionary<string, Product> expected = _testSets["null_test"];

        // Act
        Dictionary<string, Action> actual = new();
        foreach (KeyValuePair<string, Product> entry in expected)
        {
            actual.Add(entry.Key, () => _productRepository.Add(entry.Value));
        }

        // Assert
        foreach (var action in actual.Values)
        {
            Assert.Throws<ArgumentNullException>(action);
        }
    }

    [Fact(DisplayName = "AddAsync: Null product")]
    public void AddAsync_NullProduct_ThrowsException()
    {
        // Arrange
        Dictionary<string, Product> expected = _testSets["null_test"];

        // Act
        Dictionary<string, Func<Task<Product>>> actual = new();
        foreach (KeyValuePair<string, Product> entry in expected)
        {
            actual.Add(entry.Key, () => _productRepository.AddAsync(entry.Value, CancellationToken));
        }

        // Assert
        foreach (var action in actual.Values)
        {
            Assert.ThrowsAsync<ArgumentNullException>(action);
        }
    }

    [Fact(DisplayName = "AddAll: Null product")]
    public void AddAll_NullProduct_ThrowsException()
    {
        // Arrange
        Dictionary<string, Product> expected = _testSets["null_test"];

        // Act
        Task<int> actual() => _productRepository.AddAll(expected.Values, CancellationToken);

        // Assert
        Assert.ThrowsAsync<ArgumentNullException>(actual);
    }

    [Fact(DisplayName = "AddRange: Null product")]
    public void AddRange_NullProduct_ThrowsException()
    {
        // Arrange
        Dictionary<string, Product> expected = _testSets["null_test"];

        // Act
        void actual() => _productRepository.AddRange(expected.Values);

        // Assert
        Assert.Throws<NullReferenceException>(actual);
    }

    [Fact(DisplayName = "AddRangeAsync: Null product")]
    public void AddRangeAsync_NullProduct_ThrowsException()
    {
        // Arrange
        Dictionary<string, Product> expected = _testSets["null_test"];

        // Act
        Task actual() => _productRepository.AddRangeAsync(expected.Values, CancellationToken);

        // Assert
        Assert.ThrowsAsync<ArgumentNullException>(actual);
    }

    [Fact(DisplayName = "Delete: Null product")]
    public void Delete_NullProduct_ThrowsException()
    {
        // Arrange
        Dictionary<string, Product> expected = _testSets["null_test"];

        // Act
        Dictionary<string, Action> actual = new();
        foreach (KeyValuePair<string, Product> entry in expected)
        {
            actual.Add(entry.Key, () => _productRepository.Delete(entry.Value));
        }

        // Assert
        foreach (var action in actual.Values)
        {
            Assert.Throws<ArgumentNullException>(action);
        }
    }

    [Fact(DisplayName = "DeleteAsync: Null product")]
    public void DeleteAsync_NullProduct_ThrowsException()
    {
        // Arrange
        Dictionary<string, Product> expected = _testSets["null_test"];

        // Act
        Dictionary<string, Func<Task>> actual = new();
        foreach (KeyValuePair<string, Product> entry in expected)
        {
            actual.Add(entry.Key, () => _productRepository.DeleteAsync(entry.Value, CancellationToken));
        }

        // Assert
        foreach (var action in actual.Values)
        {
            Assert.ThrowsAsync<ArgumentNullException>(action);
        }
    }

    [Fact(DisplayName = "DeleteRange: Null product")]
    public void DeleteRange_NullProduct_ThrowsException()
    {
        // Arrange
        Dictionary<string, Product> expected = _testSets["null_test"];

        // Act
        void actual() => _productRepository.DeleteRange(expected.Values);

        // Assert
        Assert.Throws<NullReferenceException>(actual);
    }

    [Fact(DisplayName = "DeleteRangeAsync: Null product")]
    public void DeleteRangeAsync_NullProduct_ThrowsException()
    {
        // Arrange
        Dictionary<string, Product> expected = _testSets["null_test"];

        // Act
        Task actual() => _productRepository.DeleteRangeAsync(expected.Values, CancellationToken);

        // Assert
        Assert.ThrowsAsync<NullReferenceException>(actual);
    }

    [Fact(DisplayName = "DeleteRange: Null input")]
    public void DeleteRange_NullInput_ThrowsException()
    {
        // Arrange
        Dictionary<string, Product> expected = _testSets["null_test"];

        // Act
        void actual() => _productRepository.DeleteRange(null!);

        // Assert
        Assert.Throws<ArgumentNullException>(actual);
    }

    [Fact(DisplayName = "DeleteRangeAsync: Null input")]
    public void DeleteRangeAsync_NullInput_ThrowsException()
    {
        // Arrange
        Dictionary<string, Product> expected = _testSets["null_test"];

        // Act
        Task actual() => _productRepository.DeleteRangeAsync(null!, CancellationToken);

        // Assert
        Assert.ThrowsAsync<ArgumentNullException>(actual);
    }

    #endregion

    [Fact(DisplayName = "Add: Add product")]
    public void Add_AddEntity_EntityExistsInRepository()
    {
        // Arrange
        Dictionary<string, Product> expected = _testSets["duplicate_url"];

        // Act
        foreach (Product entry in expected.Values)
        {
            _productRepository.Add(CopyProduct(entry));
        }

        // Assert
        Dictionary<string, Product?> actual = new();
        foreach (KeyValuePair<string, Product> entry in expected)
        {
            actual.Add(entry.Key, DbContext.Products.FirstOrDefault(x => x.Id == entry.Value.Id));
        }

        actual.Values.Should().BeEquivalentTo(expected.Values);
    }

    [Fact(DisplayName = "AddAsync: Add product async")]
    public void AddAsync_AddEntity_ReturnsAddedEntities()
    {
        // Arrange
        Dictionary<string, Product> expected = _testSets["duplicate_url"];

        // Act
        Dictionary<string, Product> actual = new();
        foreach (KeyValuePair<string, Product> entry in expected)
        {
            actual.Add(entry.Key, _productRepository.AddAsync(CopyProduct(entry.Value), CancellationToken).Result);
        }

        // Assert
        actual.Values.Should().BeEquivalentTo(expected.Values);
    }

    [Fact(DisplayName = "AddAll: Add products all together")]
    public async void AddAll_AddEntities_ReturnsAddedEntitiesCount()
    {
        // Arrange
        Dictionary<string, Product> expected = _testSets["duplicate_url"];

        // Act
        int actual = await _productRepository.AddAll(CopyProductsList(expected.Values), CancellationToken);

        // Assert
        Assert.Equal(expected.Count, actual);
    }

    [Fact(DisplayName = "AddRange: Add products all together")]
    public void AddRange_AddEntities_EntityExistsInRepository()
    {
        // Arrange
        Dictionary<string, Product> expected = _testSets["duplicate_url"];

        // Act
        _productRepository.AddRange(CopyProductsList(expected.Values));

        // Assert
        Dictionary<string, Product?> actual = new();
        foreach (KeyValuePair<string, Product> entry in expected)
        {
            actual.Add(entry.Key, DbContext.Products.FirstOrDefault(x => x.Id == entry.Value.Id));
        }

        actual.Values.Should().BeEquivalentTo(expected.Values);
    }

    [Fact(DisplayName = "AddRange: No save")]
    public void AddRange_NoSave_EntityNotInRepository()
    {
        // Arrange
        Dictionary<string, Product> expected = _testSets["duplicate_url"];

        // Act
        _productRepository.AddRange(CopyProductsList(expected.Values), false);

        // Assert
        Dictionary<string, Product?> actual = new();
        foreach (KeyValuePair<string, Product> entry in expected)
        {
            actual.Add(entry.Key, DbContext.Products.FirstOrDefault(x => x.Id == entry.Value.Id));
        }

        foreach (var item in actual.Values)
        {
            Assert.Null(item);
        }

        DbContext.SaveChanges();
        actual.Clear();
        foreach (KeyValuePair<string, Product> entry in expected)
        {
            actual.Add(entry.Key, DbContext.Products.FirstOrDefault(x => x.Id == entry.Value.Id));
        }

        actual.Values.Should().BeEquivalentTo(expected.Values);
    }

    [Fact(DisplayName = "AddRangeAsync: Add products all together")]
    public async void AddRangeAsync_AddEntities_EntityExistsInRepository()
    {
        // Arrange
        Dictionary<string, Product> expected = _testSets["duplicate_url"];

        // Act
        await _productRepository.AddRangeAsync(CopyProductsList(expected.Values), CancellationToken);

        // Assert
        Dictionary<string, Product?> actual = new();
        foreach (KeyValuePair<string, Product> entry in expected)
        {
            actual.Add(entry.Key, DbContext.Products.FirstOrDefault(x => x.Id == entry.Value.Id));
        }

        actual.Values.Should().BeEquivalentTo(expected.Values);
    }

    [Fact(DisplayName = "AddRangeAsync: No save")]
    public async void AddRangeAsync_NoSave_EntityExistsInRepository()
    {
        // Arrange
        Dictionary<string, Product> expected = _testSets["duplicate_url"];

        // Act
        await _productRepository.AddRangeAsync(CopyProductsList(expected.Values), CancellationToken, false);

        // Assert
        Dictionary<string, Product?> actual = new();
        foreach (KeyValuePair<string, Product> entry in expected)
        {
            actual.Add(entry.Key, DbContext.Products.FirstOrDefault(x => x.Id == entry.Value.Id));
        }

        foreach (var item in actual.Values)
        {
            Assert.Null(item);
        }

        DbContext.SaveChanges();
        actual.Clear();
        foreach (KeyValuePair<string, Product> entry in expected)
        {
            actual.Add(entry.Key, DbContext.Products.FirstOrDefault(x => x.Id == entry.Value.Id));
        }

        actual.Values.Should().BeEquivalentTo(expected.Values);
    }

    [Fact(DisplayName = "GetAll: Get all products")]
    public void GetAll_GetAllAddedEntities_EntityExistsInRepository()
    {
        // Arrange
        Dictionary<string, Product> expected = _testSets["duplicate_url"];
        foreach (var product in expected.Values)
        {
            DbContext.Products.Add(CopyProduct(product));
        }
        DbContext.SaveChanges();

        // Act
        var actuals = _productRepository.GetAll(CancellationToken).Result;

        // Assert
        actuals.Should().BeEquivalentTo(expected.Values);
    }

    [Fact(DisplayName = "GetById: Get products by Id")]
    public void GetById_GetAddedEntityById_EntityExistsInRepository()
    {
        // Arrange
        Dictionary<string, Product> expected = _testSets["duplicate_url"];
        foreach (var product in expected.Values)
        {
            DbContext.Products.Add(CopyProduct(product));
        }
        DbContext.SaveChanges();

        // Act
        Dictionary<string, Product?> actual = new();
        foreach (KeyValuePair<string, Product> entry in expected)
        {
            actual.Add(entry.Key, _productRepository.GetById(entry.Value.Id));
        }

        // Assert
        actual.Values.Should().BeEquivalentTo(expected.Values);
    }

    [Fact(DisplayName = "GetByName: Get products by Name")]
    public async void GetByName_GetAddedEntityByName_EntityExistsInRepository()
    {
        // Arrange
        Dictionary<string, Product> expected = _testSets["duplicate_url"];
        foreach (var product in expected.Values)
        {
            DbContext.Products.Add(CopyProduct(product));
        }
        DbContext.SaveChanges();

        // Act
        Dictionary<string, Product?> actual = new();
        foreach (KeyValuePair<string, Product> entry in expected)
        {
            actual.Add(entry.Key, await _productRepository.GetByName(entry.Value.Name, CancellationToken));
        }

        // Assert
        actual.Values.Should().BeEquivalentTo(expected.Values);
    }

    [Fact(DisplayName = "GetByName: Non existing name")]
    public async void GetByName_GetAddedEntityByNonExistingName_ReturnsNull()
    {
        // Arrange
        Dictionary<string, Product> expected = _testSets["duplicate_url"];
        foreach (var product in expected.Values)
        {
            DbContext.Products.Add(CopyProduct(product));
        }
        DbContext.SaveChanges();

        // Act
        Dictionary<string, Product?> actual = new();
        foreach (KeyValuePair<string, Product> entry in expected)
        {
            actual.Add(entry.Key, await _productRepository.GetByName(new Guid().ToString(), CancellationToken));
        }

        // Assert
        actual.Values.Should().AllSatisfy(x => x.Should().BeNull());
    }

    [Fact(DisplayName = "GetByUrl: Get products by url")]
    public async void GetByUrl_GetAddedEntityByUrl_EntityExistsInRepository()
    {
        // Arrange
        Dictionary<string, Product> expected = _testSets["unique_url"];
        foreach (var product in expected.Values)
        {
            DbContext.Products.Add(CopyProduct(product));
        }
        DbContext.SaveChanges();

        // Act
        Dictionary<string, Product?> actual = new();
        foreach (KeyValuePair<string, Product> entry in expected)
        {
            actual.Add(entry.Key, await _productRepository.GetByUrl(entry.Value.Url, CancellationToken));
        }

        // Assert
        actual.Values.Should().BeEquivalentTo(expected.Values, options => options.Excluding(x => x.Images)
        .Excluding(x => x.Keywords)
        .Excluding(x => x.Tags));
    }

    [Fact(DisplayName = "GetByName: Non existing url")]
    public async void GetByUrl_GetAddedEntityByNonExistingUrl_ReturnsNull()
    {
        // Arrange
        Dictionary<string, Product> expected = _testSets["duplicate_url"];
        foreach (var product in expected.Values)
        {
            DbContext.Products.Add(CopyProduct(product));
        }
        DbContext.SaveChanges();

        // Act
        Dictionary<string, Product?> actual = new();
        foreach (KeyValuePair<string, Product> entry in expected)
        {
            actual.Add(entry.Key, await _productRepository.GetByUrl(new Guid().ToString(), CancellationToken));
        }

        // Assert
        actual.Values.Should().AllSatisfy(x => x.Should().BeNull());
    }

    [Fact(DisplayName = "GetByIdAsync: Get products by Id")]
    public async void GetByIdAsync_GetAddedEntityById_EntityExistsInRepository()
    {
        // Arrange
        Dictionary<string, Product> expected = _testSets["duplicate_url"];
        foreach (var product in expected.Values)
        {
            DbContext.Products.Add(CopyProduct(product));
        }
        DbContext.SaveChanges();

        // Act
        Dictionary<string, Product> actual = new();
        foreach (KeyValuePair<string, Product> entry in expected)
        {
            actual.Add(entry.Key, await _productRepository.GetByIdAsync(CancellationToken, entry.Value.Id));
        }

        // Assert
        actual.Values.Should().BeEquivalentTo(expected.Values);
    }

    [Fact(DisplayName = "Delete: Delete product from repository")]
    public void Delete_DeleteProduct_EntityNotInRepository()
    {
        // Arrange
        Dictionary<string, Product> expected = _testSets["duplicate_url"];
        DbContext.Products.AddRange(CopyProductsList(expected.Values));
        DbContext.SaveChanges();
        DbContext.ChangeTracker.Clear();

        Product productToDelete = expected.Values.ToArray()[0];

        // Act
        _productRepository.Delete(productToDelete);

        // Assert
        Product? actual = DbContext.Products.FirstOrDefault(x => x.Id == productToDelete.Id);

        Assert.Null(actual);
        Assert.Equal(expected.Count - 1, DbContext.Products.Count());
    }

    [Fact(DisplayName = "DeleteAsync: Delete product from repository")]
    public async void DeleteAsync_DeleteProduct_EntityNotInRepository()
    {
        // Arrange
        Dictionary<string, Product> expected = _testSets["duplicate_url"];
        DbContext.Products.AddRange(CopyProductsList(expected.Values));
        DbContext.SaveChanges();
        DbContext.ChangeTracker.Clear();

        Product productToDelete = expected.Values.ToArray()[0];

        // Act
        await _productRepository.DeleteAsync(productToDelete, CancellationToken);

        // Assert
        Product? actual = DbContext.Products.FirstOrDefault(x => x.Id == productToDelete.Id);

        Assert.Null(actual);
        Assert.Equal(expected.Count - 1, DbContext.Products.Count());
    }

    [Fact(DisplayName = "Delete: (No Save) Entity is in repository and is deleted after SaveChanges is called")]
    public void Delete_NoSave_EntityIsInRepository()
    {
        // Arrange
        Dictionary<string, Product> expected = _testSets["duplicate_url"];
        DbContext.Products.AddRange(CopyProductsList(expected.Values));
        DbContext.SaveChanges();
        DbContext.ChangeTracker.Clear();

        Product productToDelete = expected.Values.ToArray()[0];

        // Act
        _productRepository.Delete(productToDelete, false);

        // Assert
        Product? actual = DbContext.Products.FirstOrDefault(x => x.Id == productToDelete.Id);

        Assert.NotNull(actual);
        Assert.Equal(expected.Count, DbContext.Products.Count());

        DbContext.SaveChanges();
        actual = DbContext.Products.FirstOrDefault(x => x.Id == productToDelete.Id);
        Assert.Null(actual);
        Assert.Equal(expected.Count - 1, DbContext.Products.Count());
    }

    [Fact(DisplayName = "DeleteAsync: (No Save) Entity is in repository and is deleted after SaveChanges is called")]
    public async void DeleteAsync_NoSave_EntityIsInRepository()
    {
        // Arrange
        Dictionary<string, Product> expected = _testSets["duplicate_url"];
        DbContext.Products.AddRange(CopyProductsList(expected.Values));
        DbContext.SaveChanges();
        DbContext.ChangeTracker.Clear();

        Product productToDelete = expected.Values.ToArray()[0];

        // Act
        await _productRepository.DeleteAsync(productToDelete, CancellationToken, false);

        // Assert
        Product? actual = DbContext.Products.FirstOrDefault(x => x.Id == productToDelete.Id);

        Assert.NotNull(actual);
        Assert.Equal(expected.Count, DbContext.Products.Count());

        DbContext.SaveChanges();
        actual = DbContext.Products.FirstOrDefault(x => x.Id == productToDelete.Id);
        Assert.Null(actual);
        Assert.Equal(expected.Count - 1, DbContext.Products.Count());
    }

    [Fact(DisplayName = "DeleteRange: Delete range of products from repository")]
    public void DeleteRange_DeleteProducts_EntityNotInRepository()
    {
        // Arrange
        Dictionary<string, Product> expected = _testSets["duplicate_url"];
        DbContext.Products.AddRange(CopyProductsList(expected.Values));
        DbContext.SaveChanges();
        DbContext.ChangeTracker.Clear();

        string productNotToDeleteSetKey = expected.Keys.ToArray()[0];
        Product productNotToDelete = expected[productNotToDeleteSetKey];
        IEnumerable<Product> productsToDelete = expected.Values.Where(x => x.Id != productNotToDelete.Id);

        // Act
        _productRepository.DeleteRange(productsToDelete);

        // Assert
        List<Product?> actual = new();
        foreach (var product in productsToDelete)
        {
            actual.Add(DbContext.Products.FirstOrDefault(x => x.Id == product.Id));
        }

        Assert.Equal(1, DbContext.Products.Count());
        foreach (var product in actual)
        {
            Assert.Null(product);
        }
    }

    [Fact(DisplayName = "DeleteRangeAsync: Delete range of products from repository")]
    public async void DeleteRangeAsync_DeleteProducts_EntityNotInRepository()
    {
        // Arrange
        Dictionary<string, Product> expected = _testSets["duplicate_url"];
        DbContext.Products.AddRange(CopyProductsList(expected.Values));
        DbContext.SaveChanges();
        DbContext.ChangeTracker.Clear();

        string productNotToDeleteSetKey = expected.Keys.ToArray()[0];
        Product productNotToDelete = expected[productNotToDeleteSetKey];
        IEnumerable<Product> productsToDelete = expected.Values.Where(x => x.Id != productNotToDelete.Id);

        // Act
        await _productRepository.DeleteRangeAsync(productsToDelete, CancellationToken);

        // Assert
        List<Product?> actual = new();
        foreach (var product in productsToDelete)
        {
            actual.Add(DbContext.Products.FirstOrDefault(x => x.Id == product.Id));
        }

        Assert.Equal(1, DbContext.Products.Count());
        foreach (var product in actual)
        {
            Assert.Null(product);
        }
    }

    [Fact(DisplayName = "DeleteRange: (No Save) Entites are in repository and are deleted after SaveChanges is called")]
    public void DeleteRange_NoSave_EntitiesAreInRepository()
    {
        // Arrange
        Dictionary<string, Product> expected = _testSets["duplicate_url"];
        DbContext.Products.AddRange(CopyProductsList(expected.Values));
        DbContext.SaveChanges();
        DbContext.ChangeTracker.Clear();

        string productNotToDeleteSetKey = expected.Keys.ToArray()[0];
        Product productNotToDelete = expected[productNotToDeleteSetKey];
        IEnumerable<Product> productsToDelete = expected.Values.Where(x => x.Id != productNotToDelete.Id);

        // Act
        _productRepository.DeleteRange(productsToDelete, false);

        // Assert
        List<Product?> actual = new();
        foreach (var product in productsToDelete)
        {
            actual.Add(DbContext.Products.FirstOrDefault(x => x.Id == product.Id));
        }

        Assert.Equal(expected.Count, DbContext.Products.Count());
        foreach (var product in actual)
        {
            Assert.NotNull(product);
        }

        DbContext.SaveChanges();
        actual.Clear();
        foreach (var product in productsToDelete)
        {
            actual.Add(DbContext.Products.FirstOrDefault(x => x.Id == product.Id));
        }

        Assert.Equal(1, DbContext.Products.Count());
        foreach (var product in actual)
        {
            Assert.Null(product);
        }
    }

    [Fact(DisplayName = "DeleteRangeAsync: (No Save) Entites are in repository and are deleted after SaveChanges is called")]
    public async void DeleteRangeAsync_NoSave_EntitiesAreInRepository()
    {
        // Arrange
        Dictionary<string, Product> expected = _testSets["duplicate_url"];
        DbContext.Products.AddRange(CopyProductsList(expected.Values));
        DbContext.SaveChanges();
        DbContext.ChangeTracker.Clear();

        string productNotToDeleteSetKey = expected.Keys.ToArray()[0];
        Product productNotToDelete = expected[productNotToDeleteSetKey];
        IEnumerable<Product> productsToDelete = expected.Values.Where(x => x.Id != productNotToDelete.Id);

        // Act
        await _productRepository.DeleteRangeAsync(productsToDelete, CancellationToken, false);

        // Assert
        List<Product?> actual = new();
        foreach (var product in productsToDelete)
        {
            actual.Add(DbContext.Products.FirstOrDefault(x => x.Id == product.Id));
        }

        Assert.Equal(expected.Count, DbContext.Products.Count());
        foreach (var product in actual)
        {
            Assert.NotNull(product);
        }

        DbContext.SaveChanges();
        actual.Clear();
        foreach (var product in productsToDelete)
        {
            actual.Add(DbContext.Products.FirstOrDefault(x => x.Id == product.Id));
        }

        Assert.Equal(1, DbContext.Products.Count());
        foreach (var product in actual)
        {
            Assert.Null(product);
        }
    }
}
