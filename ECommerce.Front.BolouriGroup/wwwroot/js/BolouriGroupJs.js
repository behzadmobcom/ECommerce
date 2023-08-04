const formatter = new Intl.NumberFormat();

// global variables
let cartList = [];

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
  if (ids[1] != null) { id = ids[1]; }
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
    success: function (response) { },
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
          "<li>" +
          '<a class="fab fa-pinterest" href="https://pin.it/4PLGQh2" title="پینترست"></a>' +
          "</li>" +
          "<li>" +
          '<a href="https://www.linkedin.com/in/boloorico-%D8%AA%D8%AC%D9%87%DB%8C%D8%B2%D8%A7%D8%AA-%D8%B5%D9%86%D8%B9%D8%AA%DB%8C-%D8%A8%D9%84%D9%88%D8%B1%DB%8C-506aa6217/" class="fab fa-linkedin-in" title="لینکدین"></a>' +
          "</li>" +
          "<li>" +
          '<a href="https://www.instagram.com/boloorico/" class="fab fa-instagram" title="اینستاگرام"></a>' +
          "</li>" +
          "</ul>" +
          "</div>" +
          "</div>" +
          "</div>" +
          '<div class="col-md-6 col-lg-6">' +
          '<div class="view-details">' +
          '<h3 class="view-name">' +
          '<a asp-area="" asp-page="/Productdetails">' +
          result.returnData.name +
          "</a>" +
          "</h3>" +
          '<div class="view-meta">' +
          "<p><a>مارک تجاری:</a>" +
          result.returnData.brand +
          "</p>" +
          "</div>" +
          '<h3 class="view-price">' +
          "<span>" +
          result.returnData.price +
          "</span>" +
          "</h3>" +
          '<div class="view-list-group">' +
          '<label class="view-list-title">در انبار:</label>' +
          ' <div id="exist">';
        if (result.returnData.exist > 0) {
          item = item + "<div>در دسترس</div>";
        } else {
          item = item + "<div>عدم موجودی</div>";
        }
        item =
          item +
          "</div>" +
          "</div>" +
          '<div class="view-list-group">' +
          '<label class="view-list-title">برچسب ها:</label>' +
          '<ul class="view-tag-list">' +
          "</ul>" +
          "</div>" +
          "</div>" +
          "</div>" +
          "</div>" +
          '<div class="row">' +
          '<div class="col-md-10 col-md-offset-1">' +
          '<p class="view-desc">' +
          result.returnData.description +
          "</p>" +
          "</div>" +
          "</div>";
        $("#modal_body").html(item);
        $("#product-view").modal("show");
      }
    },
    failure: function (response) {
      alert(response);
    },
  });
}

function createCartItem(product) {
  return `<li class='cart-item' id='CartDrop-${product.id}'> 
            <div class='cart-media'> 
              <a asp-page='Product' asp-route-productUrl='${product.url}'>
                <img src='/${product.imagePath}' alt='${product.alt}'>
              </a>
              <button class='cart-delete' onclick='DeleteCart(${product.id},${product.productId},${product.priceId})'>
                <i class='far fa-times'></i>
              </button>
            </div>
            <div class='cart-info-group'>
              <div class='cart-info'>
                <h5><a asp-page='Product' asp-route-productUrl=${product.url}'>${product.name}</a></h5>
                <h6> برند : ${product.brand}</h6> 
                <h6> رنگ : ${product.colorName}</h6>
                <p id='cart-item-price-amount-${product.id}'>${formatter.format(product.priceAmount)}</p>
              </div>
              <div class='cart-action-group'>
                <div class='product-action'>
                  <button class='action-minus' onclick='DecreaseCart(${product.productId},${product.priceId},${product.id})' title='مقدار منهای'>
                    <i class='far fa-minus'></i>
                  </button>
                  <input class='action-input' title='تعداد' type='text' name='quantity' id='cart-item-quantity-${product.id}' value='${product.quantity}'> 
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

async function loadCart() {
  $("#Cart-List").text("");

  const data = await $.get("/index?handler=LoadCart");
  cartList = data.returnData;

  let allPrice = 0;
  const listToRender = cartList.map((product) => {
    const item = createCartItem(product);
    allPrice += product.sumPrice;
    return item;
  });

  $("#Cart-List").append(listToRender.join(""));

  $("#Cart-Count").text(cartList.length);
  $("#Cart-Count1").text(cartList.length);
  $("#Cart-Count-Value").val(cartList.length);
  $("#Cart-Count-Value-Icon").val(cartList.length);
  $("#Cart-Count-Value-Icon1").val(cartList.length);
  $("#Cart-Count2").text("کل مورد (" + cartList.length + ")");
  $("#All-Price").text(formatter.format(allPrice));
  $("#AllPrice-Value").val(allPrice);
}

/**
 *
 * @param {number} id
 * @param {"increment" | "decrement" | "newItem"} action
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

  if (action === "increment") {
    product.quantity++;
  } else if (action === "decrement") {
    product.quantity--;
  }
  product.sumPrice = product.quantity * product.priceAmount;

  if (product.quantity === 0) {
    $(`#CartDrop-${product.id}`).remove();
    cartList.splice(index, 1);
  } else if (action === "newItem") {
    $("#Cart-List").append(createCartItem(product));
  } else {
    $(`#CartDrop-${product.id}`).replaceWith(createCartItem(product));
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
}

function AddCart(productId, priceId, id, showMessage = false) {
  $.ajax({
    type: "Get",
    url: "/index?handler=AddCart&id=" + productId + "&priceId=" + priceId,
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    success: function (result) {
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

function AddCompareList(id) {
  $.ajax({
    type: "Get",
    url: "/index?handler=AddCompareList&id=" + id,
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    success: function (response) {
      $("#Compare-Count").html(response.returnData);
      swal(response.message);
    },
    failure: function (response) {
      swal(response);
    },
  });
}

function DecreaseCart(productId, priceId, id) {
  $.ajax({
    type: "Get",
    url: "/index?handler=DecreaseCart&id=" + id + "&productId=" + productId + "&priceId=" + priceId,
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    success: function (result) {
      if ($("#Cart-List").text() === "") {
        loadCart();
      } else {
        updateCartItem(id, "decrement");
      }
    },
    failure: function (response) {
      swal(response);
    },
  });
}

function DeleteCart(id, productId, priceId) {
  var elementId = "#CartDrop-" + id;
  var sumPriceId = "#SumPrice-" + id;
  var price = parseInt($(sumPriceId).val());

  var count = parseInt($("#Cart-Count-Value-Icon").val());
  var count = parseInt($("#Cart-Count-Value-Icon1").val());
  var allPrice = parseInt($("#AllPrice-Value").val());
  $.ajax({
    type: "Get",
    url: "/index?handler=DeleteCart&id=" + id + "&productId=" + productId + "&priceId=" + priceId,
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    success: function (result) {
      swal(result.message);
      if (result.code === 0) {
        $(elementId).remove();
        count--;
        allPrice = allPrice - price;
        $("#Cart-Count").text(count);
        $("#Cart-Count1").text(count);
        $("#Cart-Count2").text("کل مورد (" + count + ")");
        $("#All-Price").text(formatter.format(allPrice));
        $("#Cart-Count-Value").val(count);
        $("#Cart-Count-Value-Icon").val(count);
        $("#Cart-Count-Value-Icon1").val(count);
        $("#AllPrice-Value").val(allPrice);
      }
    },
    failure: function (response) {
      swal(response);
    },
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
    success: function (result) {
      swal(result);
      location.reload();
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

//////***fa-eye-slash***/////
$(document).ready(function () {
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

$(document).ready(function () {
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
