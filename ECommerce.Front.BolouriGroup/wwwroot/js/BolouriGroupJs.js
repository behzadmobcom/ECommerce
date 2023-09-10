const formatter = new Intl.NumberFormat();

function RemoveWishList(id) {
  $.ajax({
    type: "Get",
    url: "/index?handler=RemoveWishList&id=" + id,
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    success: function (result) {
      swal(result.message);
      if (result.code === 0) {
        $("#" + id).remove();
      }
    },
    failure: function (response) {
      alert(response);
    },
  });
}

function InvertWishList(caller, ids) {
  var id = ids[0];
  if (ids[1] != null) {
    id = ids[1];
  }
  $.ajax({
    type: "Get",
    url: "/index?handler=InvertWishList&id=" + id,
    contentType: "application/json; charset=utf-8",
    dataType: "json",

    success: function (response) {
      $("#" + caller + "-" + id).toggleClass("active");
      swal(response);
    },
    failure: function (response) {
      swal(response);
    },
  });
}

function AddWishList(id) {
  $.ajax({
    type: "Get",
    url: "/index?handler=AddWishList&id=" + id,
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    success: function (response) {},
    failure: function (response) {
      swal(response);
    },
  });
}

function setCities() {
  var stateId = $("#state").val();
  var flag = true;
  $("#city option").each(function () {
    if ($(this).attr("stateId") == stateId) {
      $(this).show();
      if (flag) {
        $("#city").val($(this).val());
        flag = false;
      }
    } else {
      $(this).hide();
    }
  });
}

$(".menu_hover").mouseover(function () {
  var section = $(this).find("a").attr("Id");
  var panelId = "#" + section + "Panel";
  $(".tab-pane").removeClass(" active in ");
  $(panelId).addClass(" active in");
});

function OpenProductModal(id) {
    $.ajax({
        type: "Get",
        url: "/index?handler=QuickView&id=" + id,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.code == 0) {
                console.log(result.returnData);
                var item =
                    '<div class="row">' +
                    '<div class="col-md-6 col-lg-6">' +
                    '<div class="view-gallery">' +
                    '<img style="width: 250px" src="' +
                    result.returnData.imagePath +
                    '" alt="' +
                    result.returnData.alt +
                    '">' +
                    '<div class="view-list-group mt-4">' +
                    '<label class="view-list-title">اشتراک گذاری:</label>' +
                    '<ul class="view-share-list">' +
                    '<li>' +
                    '<a class="fab fa-pinterest" href="https://pin.it/4PLGQh2" title="پینترست"></a>' +
                    '</li>' +
                    '<li>' +
                    '<a href="https://www.linkedin.com/in/boloorico-%D8%AA%D8%AC%D9%87%DB%8C%D8%B2%D8%A7%D8%AA-%D8%B5%D9%86%D8%B9%D8%AA%DB%8C-%D8%A8%D9%84%D9%88%D8%B1%DB%8C-506aa6217/" class="fab fa-linkedin-in" title="لینکدین"></a>' +
                    '</li>' +
                    '<li>' +
                    '<a href="https://www.instagram.com/boloorico/" class="fab fa-instagram" title="اینستاگرام"></a>' +
                    '</li>' +
                    '</ul>' +
                    '</div>' +
                    '</div>' +
                    '</div>' +
                    '<div class="col-md-6 col-lg-6">' +
                    '<div class="view-details">' +
                    '<h3 class="view-name">' +
                    '<a asp-area="" asp-page="/Productdetails">' +
                    result.returnData.name +
                    '</a>' +
                    '</h3>' +
                    '<div class="view-meta">' +
                    '<p><a>مارک تجاری:</a>' +
                    result.returnData.brand +
                    '</p>' +
                    '</div>';

                if (result.returnData.discountAmount != null) {                  
                   item = item + '<h4> <del>' +
                        result.returnData.price +
                        '</del>' +
                        '</br> <h5>' +
                        result.returnData.discountAmount +
                        '</h5>' +
                        '</h4>';
                }
                if (result.returnData.discountPercent != null) {
                    item = item + '<h4> <del>' +
                        result.returnData.price +
                        '</del>' +
                        '</br> <h5>' +
                        result.returnData.discountPercent +
                        '</h5>' +
                        '</h4>';
                }
                item = item + '<h3>' +
                              '<span>' +
                              result.returnData.payableAmount +
                              '</span>' +
                              '</h3>' +
                    '</div>'+                    

                    '<div class="view-list-group">' +
                    '<label class="view-list-title">در انبار:</label>' +
                    ' <div id="exist">';
                if (result.returnData.exist > 0) {
                    item = item + '<div>در دسترس</div>';
                } else {
                    item = item + '<div>عدم موجودی</div>';
                }
                item = item + '</div>' +
                    '</div>' +
                    '<div class="view-list-group">' +
                    '<label class="view-list-title">برچسب ها:</label>' +
                    '<ul class="view-tag-list">' +
                    '</ul>' +
                    '</div>' +
                    '</div>' +
                    '</div>' +
                    '</div>' +
                    '<div class="row">' +
                    '<div class="col-md-10 col-md-offset-1">' +
                    '<p class="view-desc">' +
                    result.returnData.description +
                    '</p>' +
                    '</div>' +
                    '</div>';
                $('#modal_body').html(item);
                $('#product-view').modal('show');
            }
        },
        failure: function (response) {
            alert(response);
        }
    });
}

function createCartItem(product) {
  return `<li class='cart-item' id='CartDrop-${product.id}'> 
  <div class='cart-media'> 
    <a href='/product/${product.url}'>
      <img src='/${product.imagePath}' alt='${product.alt}'>
    </a>
    <button class='cart-delete' onclick='DeleteCart(${product.id},${product.productId},${product.priceId})'>
      <i class='far fa-times'></i>
    </button>
  </div>
  <div class='cart-info-group'>
    <div class='cart-info'>
      <h5><a href="/product/${product.url}">${product.name}</a></h5>
      <h6> برند : ${product.brand}</h6> 
      <h6> رنگ : ${product.colorName}</h6>
      <p id='cart-item-price-amount-${product.id}'>${formatter.format(product.priceAmount)}</p>
    </div>
    <div class='cart-action-group'>
      <div class='product-action'>
        <button class='action-minus' onclick='DecreaseCart(${product.productId},${product.priceId},${product.id})' title='مقدار منهای'>
          <i class='far fa-minus'></i>
        </button>
        <input class='action-input' title='تعداد' type='text' name='quantity' id='cart-item-quantity-${product.id}' value='${product.quantity}' disabled> 
        <button class='action-plus' onclick='AddCart(${product.productId},${product.priceId},${product.id})' title='مقدار به علاوه'>
          <i class='far fa-plus'></i>
        </button>
      </div>
      <h6 id='cart-item-sum-price-${product.id}'>${formatter.format(product.sumPrice)}</h6>
      <h6>تومان</h6>
      <input hidden='hidden' value='${product.sumPrice}' id='SumPrice-${product.id}'/>
    </div>
  </div>
</li>`;
}

const toggleCheckout = () => {
  const checkoutBtn = $(".cart-checkout-btn");
  if (cartList.length === 0) checkoutBtn.addClass("disable-a");
  else checkoutBtn.removeClass("disable-a");
};

async function loadCart() {
  const data = await $.get("/index?handler=LoadCart");
  cartList = data.returnData;

  let allPrice = 0;
  const listToRender = cartList.map((product) => {
    const item = createCartItem(product);
    allPrice += product.sumPrice;
    return item;
  });

  $("#Cart-List").text("");
  $("#Cart-List").append(listToRender.join(""));

  $("#Cart-Count").text(cartList.length);
  $("#Cart-Count1").text(cartList.length);
  $("#Cart-Count-Value").val(cartList.length);
  $("#Cart-Count-Value-Icon").val(cartList.length);
  $("#Cart-Count-Value-Icon1").val(cartList.length);
  $("#Cart-Count2").text("کل مورد (" + cartList.length + ")");
  $("#All-Price").text(formatter.format(allPrice));
  $("#AllPrice-Value").val(allPrice);

  toggleCheckout();
}

/**
 *
 * @param {number} id
 * @param {"increment" | "decrement" | "newItem" | "remove"} action
 * @param {number} productId
 */
async function updateCartItem(id, action, productId) {
  if (!id) {
    const data = await $.get("/index?handler=LoadCart");
    const newCartList = data.returnData;
    const p = newCartList.filter((v1) => !cartList.some((v2) => v1.id === v2.id))[0];
    if (p) {
      cartList.push(p);
      id = p.id;
      action = "newItem";
    } else {
      action = "increment";
      const prod = cartList.filter((v) => v.productId === productId)[0];
      id = prod.id;
    }
  }
  let index;
  const product = cartList.filter((v, i) => {
    const res = v.id === id;
    if (res) index = i;
    return res;
  })[0];

  const cartItem = $(`#CartDrop-${product.id}`);

  if (action === "increment") {
    product.quantity++;
  } else if (action === "decrement") {
    product.quantity--;
  }
  product.sumPrice = product.quantity * product.priceAmount;

  if (product.quantity === 0) {
    cartItem.remove();
    cartList.splice(index, 1);
  } else if (action === "newItem") {
    $("#Cart-List").append(createCartItem(product));
  } else if (action === "remove") {
    cartList.splice(index, 1);
    cartItem.remove();
  } else {
    cartItem.replaceWith(createCartItem(product));
  }

  const allPrice = cartList.reduce((prev, current) => prev + current.sumPrice, 0);

  $("#Cart-Count").text(cartList.length);
  $("#Cart-Count1").text(cartList.length);
  $("#Cart-Count-Value").val(cartList.length);
  $("#Cart-Count-Value-Icon").val(cartList.length);
  $("#Cart-Count-Value-Icon1").val(cartList.length);
  $("#Cart-Count2").text("کل مورد (" + cartList.length + ")");
  $("#All-Price").text(formatter.format(allPrice));
  $("#AllPrice-Value").val(allPrice);

  toggleCheckout();
}

function AddCart(productId, priceId, id, showMessage = false) {
  $(`#CartDrop-${id}`).append(`<div id='loading-${id}' class='loading-indicator'><progress class='pure-material-progress-circular'/></div>`);
  $.ajax({
    type: "Get",
    url: "/index?handler=AddCart&id=" + productId + "&priceId=" + priceId,
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    success: function (result) {
      $(`#loading-${id}`).remove();
      if (showMessage) {
        swal(result.message);
      }
      if ($("#Cart-List").text() === "") {
        loadCart();
      } else {
        if (result.code === 2 || result.code === 0) {
          updateCartItem(id, "increment", productId);
        } else {
          swal(result.message);
        }
      }
    },
    failure: function (response) {
      $(`#loading-${id}`).remove();
      swal(response);
    },
  });
}

function DeleteCompare(id) {
  $.ajax({
    type: "Get",
    url: "/index?handler=DeleteCompare&id=" + id,
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    success: function (result) {
      if (result.code === 0) {
        location.reload();
      }
    },
    failure: function (response) {
      alert(response);
    },
  });
}

function DecreaseCart(productId, priceId, id) {
  const decrease = () =>
    $.ajax({
      type: "Get",
      url: "/index?handler=DecreaseCart&id=" + id + "&productId=" + productId + "&priceId=" + priceId,
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (result) {
        $(`#loading-${id}`).remove();
        if ($("#Cart-List").text() === "") {
          loadCart();
        } else {
          updateCartItem(id, "decrement");
        }
      },
      failure: function (response) {
        $(`#loading-${id}`).remove();
        swal(response);
      },
    });

  $(`#CartDrop-${id}`).append(`<div id='loading-${id}' class='loading-indicator'><progress class='pure-material-progress-circular'/></div>`);
  const quantity = $(`#cart-item-quantity-${id}`).val();
  if (+quantity === 1) {
    swal({
      icon: "warning",
      title: "حذف محصول",
      text: "از حذف این محصول اطمینان دارید؟",
      buttons: ["خیر", "بله"],
    }).then((isConfirmed) => {
      if (isConfirmed) {
        decrease();
      } else {
        $(`#loading-${id}`).remove();
      }
    });
  } else {
    decrease();
  }
}

function DeleteCart(id, productId, priceId) {
  $(`#CartDrop-${id}`).append(`<div id='loading-${id}' class='loading-indicator'><progress class='pure-material-progress-circular'/></div>`);
  swal({
    icon: "warning",
    title: "حذف محصول",
    text: "از حذف این محصول اطمینان دارید؟",
    buttons: ["خیر", "بله"],
  }).then((isConfirmed) => {
    if (isConfirmed) {
      $.ajax({
        type: "Get",
        url: "/index?handler=DeleteCart&id=" + id + "&productId=" + productId + "&priceId=" + priceId,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
          $(`#loading-${id}`).remove();
          swal(result.message);
          if (result.code === 0) {
            updateCartItem(id, "remove", productId);
          }
        },
        failure: function (response) {
          $(`#loading-${id}`).remove();
          swal(response);
        },
      });
    } else {
      $(`#loading-${id}`).remove();
    }
  });
}

function SendConfirmSms() {
  username = document.getElementById("userName").value;
  if (username == "") {
    return;
  }
  var timer = "";
  var newNumber = false;
  $.ajax({
    type: "Get",
    url: "/Newlogin?handler=SecondsLeft&username=" + username,
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    async: false,
    success: function (result) {
      if (result.code == 2) {
        timer = result.returnData;
      }
      if (result.code == 1) {
        newNumber = true;
      }
    },
  });
  if (newNumber) {
    swal("این شماره در سایت ثبت نام نکرده است");
    return;
  }
  if (timer == "") {
    swal("رمز یکبار مصرف برای شما ارسال شد.");
    $.ajax({
      type: "Get",
      url: "/Newlogin?handler=SendRegisterSms&username=" + username,
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      async: false,
    });
    timer = "130";
  }

  $("#sendSmsButton").disabled = true;
  var interval = setInterval(function () {
    var minutes = Math.trunc(parseInt(timer) / 60);
    var seconds = parseInt(timer) - minutes * 60;

    --seconds;
    minutes = seconds < 0 ? --minutes : minutes;
    if (minutes < 0) clearInterval(interval);
    seconds = seconds < 0 ? 59 : seconds;
    $("#countdown").html(minutes + ":" + seconds);
    timer = minutes * 60 + seconds;
    if (timer == 0) {
      clearInterval(interval);
      $("#countdown").html("");
      btnSendActive.addEventListener("click", buttonClicked);
    }
  }, 1000);
  $("#sendSmsButton").disabled = false;
}

function SaveStars(id, starNumber) {
  $.ajax({
    type: "Get",
    url: "/index?handler=SaveStars&id=" + id + "&starNumber=" + starNumber,
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    success: async function (result) {
      if (result.code === 0) {
        swal(result.message);
        const stars = await (await fetch(`/index?handler=stars&id=${id}`)).json();
        const styles = ["star-quarter", "star-half", "star-3-quarter", "star-full"];
        for (let i = 1; i <= 5; i++) {
          let cls = "";
          if (Math.ceil(stars) === i) {
            cls += styles[Math.floor((stars % 1) / 0.33)];
          }
          if (i <= stars) {
            cls += " rankChecked";
          }
          const label = $(`#starLable-${i}[for='${id}-${i}']`);
          label.removeClass();
          label.addClass(cls);
        }
        $(`#starLable-1[for='${id}-1']`)
          .parent()
          .parent()
          .find("a")
          .text((_, v) => v.replace(/(\d+(?:[\/\.]\d+)?)/, (+stars.toFixed(2)).toString().replace(".", "/")));
      } else {
        swal(result.message);
      }
    },
    failure: function (response) {
      swal(response);
    },
  });
}

function CheckPassword() {
  {
    inputtxt = $("#RegisterViewModel_Password");
    var decimal = /^(?=.*[0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]{8,30}$/;
    if (decimal.test(inputtxt.val())) {
      $("#passwordHintId").hide();
    } else {
      $("#passwordHintId").show();
    }
  }
}

function closeZoom() {
  if (isOpenModel) {
    $("#zoomModal").css("display", "none");
    $(".img-zoom-lens").css("display", "none");
    isOpenModel = false;
  }
}

function openZoom() {
  if (isOpenModel == false) {
    $("#zoomModal").css("display", "block");
    $(".img-zoom-lens").css("display", "block");
    isOpenModel = true;
  }
}

function ChangeZoomImage(imageId) {
  ReImageZoom(imageId, "myresult");
  $("#zoomModal").css("display", "none");
}

/**
 *
 * @param {string} searchText
 */
const searchChangeHandler = async (searchText) => {
  if (searchText.length < 3) {
    $(".search-result").hide("fast");
    $(".search-result-items").html("");
    $(".search-result-all-btn").html("");
    return;
  }

  const searchResult = await $.ajax("/shop?handler=search", {
    url: "/",
    data: {
      page: 1,
      quantityPerPage: 10,
      searchText,
    },
    method: "GET",
  });

  const searchResultCategory = await $.ajax("/shop?handler=searchCategory", {
    url: "/",
    data: {
      page: 1,
      quantityPerPage: 10,
      searchText,
    },
    method: "GET",
  });

  if (searchResult.length === 0 && searchResultCategory.length === 0) {
    $(".search-result-items").html("<p style='text-align:center;height:100%;padding-top:10px;'>نتیجه ای یافت نشد.</p>");
  } else {
    const categoriesResults = searchResultCategory.map((value, index) => {
      return createSearchCategoryResultItem(value, index);
    });
    const productsResults = searchResult.map((value, index) => {
      return createSearchResultItem(value, index);
    });
    const resutls = [...categoriesResults, ...productsResults];
    $(".search-result-items").html(resutls);
    $(".search-result-all-btn").html(`<a role="button" class="btn btn-primary search-all-btn" href="/shop?search=${searchText}">همه نتایج</a>`);
  }

  $(".search-result").show("fast");
};

const createSearchCategoryResultItem = (value, index) => {
  return `<a href="/Shop/${value.path}" id="search-category-result-${value.id}">
      <img src="${value.imagePath}" alt="${value.name}" width="80px">
      <div>${value.name}</div>
    </a>`;
};

const createSearchResultItem = (value, index) => {
  return `<a href="/product/${encodeURIComponent(value.url)}" id="search-result-${value.id}">
      <img src="/${value.imagePath}" alt="${value.alt}" width="80px">
      <div>${value.name}</div>
    </a>`;
};

$(() => {
  let timer = null;
  $("#searchBox").on("input", (event) => {
    if (timer) {
      clearTimeout(timer);
    }
    timer = setTimeout(() => {
      timer = null;
      searchChangeHandler(event.target.value);
    }, 500);
  });
  $(document).on("click", (e) => {
    if (!$(e.target).closest(".search-result").length) $(".search-result").hide();
    if ($("#searchBox").is(":focus")) {
      const searchResult = $(".search-result-items");
      if (searchResult.children().length > 0) searchResult.parent().show("fast");
    }
  });
});

//////***fa-eye-slash***/////
$(function () {
  $("#show_hide_password a").on("click", function (event) {
    event.preventDefault();
    if ($("#show_hide_password input").attr("type") == "text") {
      $("#show_hide_password input").attr("type", "password");
      $("#show_hide_password i").addClass("fa-eye-slash");
      $("#show_hide_password i").removeClass("fa-eye");
    } else if ($("#show_hide_password input").attr("type") == "password") {
      $("#show_hide_password input").attr("type", "text");
      $("#show_hide_password i").removeClass("fa-eye-slash");
      $("#show_hide_password i").addClass("fa-eye");
    }
  });
});

$(function () {
  $("#show_hide_password1 a").on("click", function (event) {
    event.preventDefault();
    if ($("#show_hide_password1 input").attr("type") == "text") {
      $("#show_hide_password1 input").attr("type", "password");
      $("#show_hide_password1 i").addClass("fa-eye-slash");
      $("#show_hide_password1 i").removeClass("fa-eye");
    } else if ($("#show_hide_password1 input").attr("type") == "password") {
      $("#show_hide_password1 input").attr("type", "text");
      $("#show_hide_password1 i").removeClass("fa-eye-slash");
      $("#show_hide_password1 i").addClass("fa-eye");
    }
  });
});

window.RemoveWishList = RemoveWishList;
window.InvertWishList = InvertWishList;
window.AddWishList = AddWishList;
window.setCities = setCities;
window.OpenProductModal = OpenProductModal;
window.createCartItem = createCartItem;
window.toggleCheckout = toggleCheckout;
window.loadCart = loadCart;
window.updateCartItem = updateCartItem;
window.AddCart = AddCart;
window.DeleteCart = DeleteCart;
window.DeleteCompare = DeleteCompare;
window.DecreaseCart = DecreaseCart;
window.SendConfirmSms = SendConfirmSms;
window.SaveStars = SaveStars;
window.CheckPassword = CheckPassword;
window.closeZoom = closeZoom;
window.openZoom = openZoom;
window.ChangeZoomImage = ChangeZoomImage;
window.searchChangeHandler = searchChangeHandler;
