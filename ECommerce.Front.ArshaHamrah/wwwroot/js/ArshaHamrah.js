function DeleteCompare(id) {
    $.ajax({
        type: "Get",
        url: "/index?handler=DeleteCompare&id=" + id,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            swal(result.message);
            if (result.code === 0) {
                location.reload();
            }
        },
        failure: function (response) {
            alert(response);
        }
    });
}

function SaveStars(id, starNumber) {

    $.ajax({
        type: "Get",
        url: "/index?handler=SaveStars&id=" + id + "&starNumber=" + starNumber,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            swal(result);
            location.reload();
        },
        failure: function (response) {
            swal(response);
        }
    });
}

function DeleteCart(id, productId, priceId) {
    var elementId = "CartDrop-" + id;
    var sumPriceId = "#SumPrice-" + id;
    var price = parseInt($(sumPriceId).val());
    var count = parseInt($("#Cart-Count-Value").val());
    var allPrice = parseInt($("#AllPrice-Value").val());
    $.ajax({
        type: "Get",
        url: "/index?handler=DeleteCart&id=" + id + "&productId=" + productId + "&priceId=" + priceId,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            swal(result.message);
            if (result.code === 0) {
                location.reload();
                //const element = document.getElementById(elementId);
                //element.remove();
                //count--;
                //allPrice = allPrice - price;
                //$('#Cart-Count').text(count);
                //$('#All-Price').text(allPrice + ' تومان');
                //$("#Cart-Count-Value").val(count);
                //$("#AllPrice-Value").val(allPrice);
            }
        },
        failure: function (response) {
            alert(response);
        }
    });
}

function DecreaseCart(id, productId, priceId) {
    $.ajax({
        type: "Get",
        url: "/index?handler=DecreaseCart&id=" + id + "&productId=" + productId + "&priceId=" + priceId,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            swal(result.message);
            if (result.code === 0) {
                LoadCard();
            }
        },
        failure: function (response) {
            swal(response);
        }
    });
}

function AddWishList(id) {
    $.ajax({
        type: "Get",
        url: "/index?handler=AddWishList&id=" + id,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            swal(result.message);
        },
        failure: function (result) {
            swal(result.message);
        }
    });
}

function AddCompareList(id) {
    $.ajax({
        type: "Get",
        url: "/index?handler=AddCompareList&id=" + id,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            swal(result.message);
            if (result.code === 0) {
                $('#Compare-Count').text(result.returnData);
                $('#Compare-Count-value').text(result.returnData);
            }
        },
        failure: function (response) {
            swal(response);
        }
    });
}

function OpenProductModal() {
    $.ajax({
        type: "Get",
        url: "/index?handler=wish",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            swal(response);
        },
        failure: function (response) {
            swal(response);
        }
    });
}

function AddCart(id, priceId, count) {
    $.ajax({
        type: "Get",
        url: "/index?handler=AddCart&id=" + id + "&priceId=" + priceId + "&count=" + count,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            swal(result.message);
            if (result.code === 0) {
                LoadCard();
            }
        },
        failure: function (response) {
            alert(response);
        }
    });

}

function OpenProductModal(id) {
    $.ajax({
        type: "Get",
        url: "/index?handler=QuickView&id=" + id,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.code == 0) {
                var item =
                    '<div class="modal-product clearfix">' +
                    '<div class="product-images">' +
                    ' <div class="main-image images">' +
                    ' <img alt="' +
                    result.returnData.alt +
                    '" src="/' +
                    result.returnData.imagePath +
                    '">' +
                    '   </div>' +
                    ' </div>' +
                    ' <div class="product-info">' +
                    '<h1>' + result.returnData.name + '</h1>' +
                    '<p>' + result.returnData.brand + '</p>' +
                    '<div class="price-box-3">' +
                    '<div class="s-price-box">' +
                    '<span class="new-price">' +
                    result.returnData.price +
                    ' تومان</span>' +
                    '</div>' +
                    '</div>' +
                    '<a  href="/product/' + result.returnData.url + '" class="see-all">مشاهده همه ویژگی ها</a>' +
                    ' <div class="quick-desc">' +
                    result.returnData.description +
                    '  </div>' +
                    ' <div class="social-sharing">' +
                    ' <div class="widget widget_socialsharing_widget">' +
                    '   </div>' +
                    '   </div>' +
                    ' </div>' +
                    '</div>' +
                    '</p></div></div>';
                $('#modal_body').html(item);
                $('#productModal').modal('show');
            }
        },
        failure: function (response) {
            alert(response);
        }
    });
}

function LoadCard() {
    var count = 0;
    var allPrice = 0;
    $('#Cart-List').text('');
    $.get('/index?handler=LoadCart').done(function (products) {
        $.each(products.returnData,
            function (i, product) {
                var item =
                    '<div class="single-cart clearfix" id="CartDrop-' +
                    product.id +
                    '>"' +
                    '<h6 class="text-capitalize"> <a href="' +
                    product.url +
                    '">' +
                    product.name +
                    '</a> </h6>' +
                    '<div class="cart-img f-left">' +
                    '<a href="' +
                    product.url +
                    '">' +
                    '<img width="100px" src="/' +
                    product.imagePath +
                    '" alt="' +
                    product.alt +
                    '"> </a>' +
                    '<div class="del-icon">' +
                    '<a href="javascript:void(0)" onclick="DeleteCart(' + product.id + ',' + product.productId + ',' + product.priceId + ')""> ' +
                    '<i class="zmdi zmdi-close"></i> </a>' +
                    '</div></div><div class="cart-info f-left"> <p>' +
                    '<span>برند <strong>:</strong></span>' +
                    product.brand +
                    '</p><p><span>قیمت <strong>:</strong></span>' +
                    product.priceAmount +
                    '</p><p><span>تخفیف <strong>:</strong></span>' +
                    product.discountAmount +
                    '</p><p><span>تعداد <strong>:</strong></span>' +
                    product.quantity +
                    '</p><p><span>رنگ <strong>:</strong></span>' +
                    product.colorName +
                    '</p><p> <input hidden="hidden" value="' +
                    product.sumPrice +
                    '" id="SumPrice-' +
                    product.productId +
                    '"/><span>جمع <strong>:</strong></span>' +
                    product.sumPrice +
                    '</p></div></div>';
                $('#Cart-List').append(item);
                count++;
                allPrice = allPrice + product.sumPrice;
            });
        $('#Cart-Count').text(count);
        $('#All-Price').text(allPrice + ' تومان');
    });
}
